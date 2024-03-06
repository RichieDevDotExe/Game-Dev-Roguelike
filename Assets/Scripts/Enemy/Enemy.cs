using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField]private float enemyHealth;
    [SerializeField]private float enemyMaxHealth;
    [SerializeField]private float enemySpeed;
    [SerializeField]private float enemyDamage;

    [Header("Attack")]
    [SerializeField] private Transform hitbox;
    [SerializeField] private Vector3 hitBoxSize;
    [SerializeField] private LayerMask playerLayer; 
    [SerializeField] private float enemyCooldown;

    [Header("Enemy Statemachine")]
    [SerializeField]private EnemyPath enemyPath;
    [SerializeField]private string currentState;

    [Header("Enemy Misc")]
    [SerializeField] private PlayerAttibutes player;

    private Rigidbody ridgeBody;
    private EnemyStateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    public EnemyPath EnemyPath { get => enemyPath; }
    //debugging
    

    void Start()
    {
        enemyHealth = enemyMaxHealth;
        stateMachine =  GetComponent<EnemyStateMachine>();
        agent = GetComponent<NavMeshAgent>();
        enemyPath = GetComponent<EnemyPath>();
        ridgeBody = GetComponent<Rigidbody>();
        stateMachine.Init();
    }

    private void Update()
    {
        if(enemyHealth <= 0)
        {
            enemyDie();
        }
    }

    public float EnemyHealth
	{
		get { return enemyHealth; }
		set { enemyHealth = value; }
	}
    public float EnemyMaxHealth
    {
        get { return enemyMaxHealth; }
        set { enemyMaxHealth = value; }
    }
    public float EnemySpeed
    {
        get { return enemySpeed; }
        set { enemySpeed = value; }
    }
    public float EnemyDamage
    {
        get { return enemyDamage; }
        set { enemyDamage = value; }
    }

    public void enemyTakeDamage(float damage)
    {
        enemyHealth -= damage;
    }

    private void enemyDie()
    {
        Destroy(gameObject);
    }

    //enemy attack will be different for every single enemy so this shouldn't be defined. will be defined now however so it can be tested
    //public void enemyAttack;
    IEnumerable charge(Vector3 playerloc)
    {
        Debug.Log("charge");
        enemySpeed = 40;
        //transform.Translate(move * enemySpeed * Time.deltaTime);
        agent.destination = playerloc;
        return null;
    }

    [ContextMenu("Attack")]
    public void enemyAttack()
    {
        //float saveSpeed = enemySpeed;
        //enemySpeed = 0;

        Collider[] hitPlayer = Physics.OverlapBox(hitbox.position, hitBoxSize, hitbox.rotation, playerLayer);
        Debug.Log("enemy Hit");
        foreach (Collider playerHit in hitPlayer)
        {
            Debug.Log("Player Hit");
            player.playerTakeDamage(enemyDamage);
        }
        //Debug.Log("activate");
        //Invoke("charge", 1.5f);
        //enemySpeed = saveSpeed;
        //transform.LookAt(playerPos);
        //Debug.Log("attack done");


        //Debug.Log("activate");
        //IEnumerator charge()
        //{
        //    Debug.Log("charge");
        //    ridgeBody.AddRelativeForce(transform.forward);
        //    yield return new WaitForSeconds(3);
        //}
        //StartCoroutine(charge());
        //transform.LookAt(playerPos);
        //Debug.Log("attack done");

        //Debug.Log("activate");
        //enemySpeed = 0;
        //float timeStart = Time.deltaTime;
        //float timeCheck = 0;
        //while (timeCheck < 3)
        //{
        //    Debug.Log("charge");
        //    transform.LookAt(playerPos);
        //    timeCheck = timeStart - Time.deltaTime;
        //}
        //Debug.Log("charge");

        //ridgeBody.AddRelativeForce(transform.forward);
    }
    void OnDrawGizmosSelected()
    {
        if (hitbox == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(hitbox.position, hitBoxSize);
    }
}
