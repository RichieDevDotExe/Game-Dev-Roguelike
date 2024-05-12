using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Progress;

public class SpawnerEnemy : SpawnerBase
{

    [SerializeField] private GameObject enemy;
    [SerializeField] private float enemySpawnChance;

    private GameObject boss;

    private ObjectPool<GameObject> enemyPool;
    private ObjectPool<GameObject> bossPool;
    private float RNG;

    // Start is called before the first frame update
    void Start()
    {
        enemyPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(enemy, transform.position, transform.rotation);
        }, enemy =>
        {
            enemy.gameObject.SetActive(true);
            enemy.transform.Find("Character_BR_BigOrk_01").GetComponent<Enemy>().Init();
            enemy.transform.position = transform.position;
            enemy.transform.rotation = transform.rotation;
        }, enemy =>
        {
            enemy.gameObject.SetActive(false);
        }, enemy =>
        {
            Destroy(enemy.gameObject);
        }, true, 15, 25);

        spawn();
    }

    private void destEnemy(GameObject thisEnemy)
    {
        enemyPool.Release(thisEnemy);
    }

    public override void spawn()
    {
        RNG = Random.Range(0f, 1f);
        if (RNG > (1 - enemySpawnChance))
        {
            Debug.Log("Spawning - enemy");
            var itemSpawn = enemyPool.Get();
            itemSpawn.transform.Find("Character_BR_BigOrk_01").GetComponent<Enemy>().giveDestroy(destEnemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
