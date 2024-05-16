using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Progress;
using Debug = UnityEngine.Debug;

public class SpawnerShopkeeper : SpawnerBase
{
    [SerializeField] private GameObject shopKeeper;
    [SerializeField] private float shopKeeperSpawnChance;

    private ObjectPool<GameObject> chestPool;
    private float RNG;

    //generates object pool for trader objects 
    void Start()
    {
        chestPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(shopKeeper, transform.position, transform.rotation);
        }, shopKeeper =>
        {
            shopKeeper.gameObject.SetActive(true);
            shopKeeper.transform.Find("Character_Male_Wizard_01").GetComponent<Shopkeeper>().Init();
            shopKeeper.transform.position = transform.position;
            shopKeeper.transform.rotation = transform.rotation;
        }, shopKeeper =>
        {
            shopKeeper.gameObject.SetActive(false);
        }, shopKeeper =>
        {
            Destroy(shopKeeper.gameObject);
        }, true, 1, 3);

        spawn();
    }

    //releases trader 
    private void destShopkeeper(GameObject thisTrader)
    {
        chestPool.Release(thisTrader);
    }

    //spawns shopkeeper. however the shopkeeper has a chance to spawn in 
    public override void spawn()
    {
        RNG = Random.Range(0f, 1f);
        if (RNG > (1 - shopKeeperSpawnChance))
        {
            Debug.Log("Spawning - trader");
            var itemSpawn = chestPool.Get();
            itemSpawn.transform.Find("Character_Male_Wizard_01").GetComponent<Shopkeeper>().giveDestroy(destShopkeeper);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
