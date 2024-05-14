using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Progress;

public class SpawnerBoss : SpawnerBase
{

    [SerializeField] private GameObject boss;

    private ObjectPool<GameObject> bossPool;

    //generates object pool for enemy objects 
    void Start()
    {
        bossPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(boss, transform.position, transform.rotation);
        }, boss =>
        {
            boss.gameObject.SetActive(true);
            boss.transform.Find("Character_BR_BigOrk_01").GetComponent<Enemy>().Init();
            boss.transform.position = transform.position;
            boss.transform.rotation = transform.rotation;
        }, boss =>
        {
            boss.gameObject.SetActive(false);
        }, boss =>
        {
            Destroy(boss.gameObject);
        }, true, 1, 1);

        spawn();
    }

    //releases enemy 
    private void destEnemy(GameObject thisEnemy)
    {
        bossPool.Release(thisEnemy);
    }

    //spawns enemy
    public override void spawn()
    {
        var itemSpawn = bossPool.Get();
        itemSpawn.transform.Find("Character_BR_BigOrk_01").GetComponent<Enemy>().giveDestroy(destEnemy);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
