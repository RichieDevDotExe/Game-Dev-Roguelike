using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Progress;
using Debug = UnityEngine.Debug;

public class SpawnerTrader : SpawnerBase
{
    [SerializeField] private GameObject trader;

    private ObjectPool<GameObject> chestPool;

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

    private void destTrader(GameObject thisTrader)
    {
        chestPool.Release(thisTrader);
    }

    public override void spawn()
    {
        Debug.Log("Spawning - trader");
        var itemSpawn = chestPool.Get();
        itemSpawn.transform.Find("Character_Male_Wizard_01").GetComponent<Trader>().giveDestroy(destTrader);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
