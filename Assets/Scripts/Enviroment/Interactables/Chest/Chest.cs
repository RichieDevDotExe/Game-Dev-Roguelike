using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using static UnityEditor.Progress;

public class Chest : InteractableObject
{
    //All items chest can spawn
    [SerializeField] private ChestItem[] lootTable;
    private Animator animator;


    [SerializeField] private Items gold;
    private ObjectPool<Items> goldPool;

    [SerializeField] private Items potion;
    private ObjectPool<Items> potionPool;

    private ChestItem item;
    private int numbOfItems;
    private float dropChance;
    private float RNG;
    private Action<Chest> destroyThis;
    [SerializeField] private AudioClip openChestSFX;
    public Animator Animator { get => animator; }

    //Creates object pools for item drops from chest
    private void Awake()
    {
        animator = GetComponent<Animator>();
        goldPool = new ObjectPool<Items>(() =>
        {
            return Instantiate(gold, transform.position, transform.rotation);
        }, gold =>
        {
            gold.gameObject.SetActive(true);
            gold.transform.position = transform.position;
            gold.transform.rotation = transform.rotation;
        }, gold =>
        {
            gold.gameObject.SetActive(false);
        }, gold =>
        {
            Destroy(gold.gameObject);
        }, true, 25, 60);

        potionPool = new ObjectPool<Items>(() =>
        {
            return Instantiate(potion, transform.position, transform.rotation);
        }, potion =>
        {
            potion.gameObject.SetActive(true);
            potion.transform.position = transform.position;
            potion.transform.rotation = transform.rotation;
        }, potion =>
        {
            potion.gameObject.SetActive(false);
        }, potion =>
        {
            Destroy(potion.gameObject);
        }, true, 2, 9);
    }


    private void Update()
    {
        
    }

    //Interact Logic for chest
    public override void Interact()
    {
        Debug.Log("Opening Chest");
        SoundFXManager.instance.playSoundEffect(openChestSFX, transform, 1f);
        animator.SetBool("ChestInteract",true);
    }

    public void Init()
    {
        animator.SetBool("ChestInteract", false);
    }

    private void chestDone()
    {
        destroyThis(this);
    }

    IEnumerator chestDoneWait()
    {
        generateLoot();
        yield return new WaitForSeconds(1f);
        destroyThis(this);
    }

    //Generate and creates items in world
    public void generateLoot()
    {
        for (int i = 0; i < lootTable.Length; i++)
        {
            item = lootTable[i];
            numbOfItems = UnityEngine.Random.Range(item.min, item.max);
            for(int j = 0; j < numbOfItems; j++)
            {
                //each item instance has a drop chances for spawning. for each item check if RNG allows it to spawn
                dropChance = item.dropChance;
                RNG = UnityEngine.Random.Range(0f, 1f);
                if(RNG <= dropChance)
                {
                    Debug.Log("Spawning- " + item.name);
                    if(item.name == "gold")
                    {
                        var itemSpawn = goldPool.Get();
                        itemSpawn.giveDestroy(destItem);
                    }
                    else if(item.name == "potion")
                    {
                        var itemSpawn = potionPool.Get();
                        itemSpawn.giveDestroy(destItem);
                    }
                    //Instantiate(item.model, transform.position, transform.rotation);
                }
            }
        }
    }

    //Used for object pooling to remove Objects
    private void destItem(Items item)
    {
        if (item.ItemName == "Gold")
        {
            goldPool.Release(item);
        }
        else if (item.ItemName == "Potion")
        {
            potionPool.Release(item);
        }
    }

    //Used for object pooling to pass in remove function
    public void giveDestroy(Action<Chest> destroyFunct)
    {
        destroyThis = destroyFunct;
    }

    //Used from the NextLevelUI class to reset the world and remove all traders from previous run.
    public void resetThis()
    {
        destroyThis(this);
    }
}
