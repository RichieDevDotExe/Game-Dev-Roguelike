using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableObject
{
    [SerializeField] private ChestLoot.ChestItem[] lootTable;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        Destroy(gameObject, 1f);
        generateLoot();
    }

    public void generateLoot()
    {
        for (int i = 0; i < lootTable.Length; i++)
        {
            ChestLoot.ChestItem item = lootTable[i];
            int numbOfItems = Random.Range(item.min, item.max);
            for(int j = 0; j < numbOfItems; j++)
            {
                float dropChance = item.dropChance;
                float RNG = Random.Range(0f, 1f);
                if(RNG <= dropChance)
                {
                    Debug.Log("Spawning- " + item.name);
                    Instantiate(item.model, transform.position, transform.rotation);
                }
            }
        }
    }
}
