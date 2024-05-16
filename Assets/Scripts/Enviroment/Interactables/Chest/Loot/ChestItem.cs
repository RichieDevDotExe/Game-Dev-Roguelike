using UnityEngine;

[CreateAssetMenu(menuName = "ItemDesc")]
//contains values for each item to handle chest spawning 

//was originally nested class in chest loot but there was some Missing mono script glitch happeneing that caused game to crash
public class ChestItem : ScriptableObject
{
    [Header("Item Stats")]
    [SerializeField] public string itemName;
    [SerializeField] public int min;
    [SerializeField] public int max;
    [SerializeField] public float dropChance;
    [SerializeField] public GameObject model;
}