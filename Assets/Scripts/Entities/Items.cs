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

    [SerializeField] private string itemName;
    private bool chasePlayer;

    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    //once a player has gotten into range of the item and activates it the item will then constantly try to get to the players position
    void Update()
    {
        if((chasePlayer == true) && (player != null) )
        {
            agent.destination = player.transform.Find("Character_Male_Rouge_01").transform.position;
        }
    }

    //Used for object pooling to pass in remove function
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
