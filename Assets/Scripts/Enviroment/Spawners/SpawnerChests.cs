using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Progress;

public class SpawnerChests : SpawnerBase
{
    [SerializeField] private Chest chest;
    [SerializeField] private float chestSpawnChance;

    private ObjectPool<Chest> chestPool;
    private float RNG;

    // Start is called before the first frame update
    void Start()
    {
        chestPool = new ObjectPool<Chest>(() =>
        {
            return Instantiate(chest, transform.position, transform.rotation);
        }, chest =>
        {
            chest.gameObject.SetActive(true);
            chest.Animator.SetBool("ChestInteract", false);
            chest.transform.position = transform.position;
            chest.transform.rotation = transform.rotation;
        }, chest =>
        {
            chest.gameObject.SetActive(false);
        }, chest =>
        {
            Destroy(chest.gameObject);
        }, true, 7, 15);

        spawn();
    }

    private void destChest(Chest thisChest)
    {
        chestPool.Release(thisChest);
    }

    public override void spawn()
    {
        RNG = Random.Range(0f, 1f);
        if (RNG > (1 - chestSpawnChance))
        {
            Debug.Log("Spawning - chest");
            var itemSpawn = chestPool.Get();
            itemSpawn.giveDestroy(destChest);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
