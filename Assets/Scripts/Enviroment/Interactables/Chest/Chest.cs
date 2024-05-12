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
    [SerializeField] private ChestLoot.ChestItem[] lootTable;
    private Animator animator;


    [SerializeField] private Items gold;
    private ObjectPool<Items> goldPool;

    [SerializeField] private Items potion;
    private ObjectPool<Items> potionPool;

    private int numbOfItems;
    private float dropChance;
    private float RNG;
    private Action<Chest> destroyThis;
    public Animator Animator { get => animator; }

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

    public override void Interact()
    {
        Debug.Log("Opening Chest");
        animator.SetBool("ChestInteract",true);
    }

    private void chestDone()
    {
        StartCoroutine(chestDoneWait());
    }

    IEnumerator chestDoneWait()
    {
        generateLoot();
        yield return new WaitForSeconds(1f);
        destroyThis(this);
    }

    public void generateLoot()
    {
        for (int i = 0; i < lootTable.Length; i++)
        {
            ChestLoot.ChestItem item = lootTable[i];
            numbOfItems = UnityEngine.Random.Range(item.min, item.max);
            for(int j = 0; j < numbOfItems; j++)
            {
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

    public void giveDestroy(Action<Chest> destroyFunct)
    {
        destroyThis = destroyFunct;
    }

    public void resetThis()
    {
        destroyThis(this);
    }
}
