using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{

    [SerializeField]private Transform playerLocation;
    private Enemy enemy;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
    }

    //
    void Update()
    {
        agent.destination = playerLocation.position;
        agent.speed = enemy.EntitySpeed;
        transform.LookAt(playerLocation.position);
    }
}
