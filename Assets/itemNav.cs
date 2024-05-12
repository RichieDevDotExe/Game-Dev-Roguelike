using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Items : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private Action<Items> destroyThis;
    public Action<Items> DestroyThis
    {
        get { return destroyThis;}
    }

    [SerializeField] private string itemName;
    private bool chasePlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if((chasePlayer == true) && (player != null) )
        {
            agent.destination = player.transform.Find("Character_Male_Rouge_01").transform.position;
        }
    }

    public void giveDestroy(Action<Items> destroyFunct)
    {
        destroyThis = destroyFunct;
    }

    public void activateItem()
    {
        chasePlayer = true;
    }

    public void destroyItem()
    {
        //Destroy(gameObject);
        destroyThis(this);
    }

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

}
