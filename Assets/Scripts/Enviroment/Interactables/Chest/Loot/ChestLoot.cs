using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ChestLoot")]
public class ChestLoot : ScriptableObject
{
    [CreateAssetMenu(menuName = "Item")]
    public class ChestItem : ScriptableObject
    {
        [Header("Item Stats")]
        [SerializeField] public string itemName;
        [SerializeField] public int min;
        [SerializeField] public int max;
        [SerializeField] public float dropChance;
        [SerializeField] public GameObject model;
    }
    public ChestItem[] chestLoot;

}
