using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{

    [SerializeField]private Transform playerLocation;
    private Enemy enemyAtri;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAtri = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = playerLocation.position;
        agent.speed = enemyAtri.EnemySpeed;
    }
}
