using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Progress;
using Debug = UnityEngine.Debug;

public class SpawnerShopkeeper : SpawnerBase
{
    [SerializeField] private GameObject trader;
    [SerializeField] private float traderSpawnChance;

    private ObjectPool<GameObject> chestPool;
    private float RNG;

    // Start is called before the first frame update
    void Start()
    {
        chestPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(trader, transform.position, transform.rotation);
        }, trader =>
        {
            trader.gameObject.SetActive(true);
            trader.transform.position = transform.position;
            trader.transform.rotation = transform.rotation;
        }, trader =>
        {
            trader.gameObject.SetActive(false);
        }, trader =>
        {
            Destroy(trader.gameObject);
        }, true, 1, 3);

        spawn();
    }

    private void destShopkeeper(GameObject thisTrader)
    {
        chestPool.Release(thisTrader);
    }

    public override void spawn()
    {
        RNG = Random.Range(0f, 1f);
        if (RNG > (1 - traderSpawnChance))
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
