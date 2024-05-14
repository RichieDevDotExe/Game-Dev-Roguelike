using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ChestLoot")]
public class ChestLoot : ScriptableObject
{
    [CreateAssetMenu(menuName = "Item")]
    //contains values for each item to handle chest spawning 
    public class ChestItem : ScriptableObject
    {
        [Header("Item Stats")]
        [SerializeField] public string itemName;
        [SerializeField] public int min;
        [SerializeField] public int max;
        [SerializeField] public float dropChance;
        [SerializeField] public GameObject model;
    }

    //chest spawn inventory
    public ChestItem[] chestLoot;

}
