using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField]private float enemyHealth;
    [SerializeField]private float enemyMaxHealth;
    [SerializeField]private float enemySpeed;
    [SerializeField]private float enemyDamage;
    [SerializeField]private float enemyAttackRange;




    [Header("Attack")]
    private BoxCollider hitbox;
    [SerializeField] private float detectionRange;
    [SerializeField] private float fieldOfView;
    [SerializeField] private float enemyCooldown;
    [SerializeField] private float chargeStrength;
    [SerializeField] private float maxSpeed;

    [Header("Enemy Statemachine")]
    [SerializeField]private EnemyPath enemyPath;
    [SerializeField]private string currentState;

    [Header("Enemy Misc")]
    [SerializeField] private PlayerAttibutes player;

    private EnemyStateMachine stateMachine;
    private NavMeshAgent agent;
    private Rigidbody rb;
    public NavMeshAgent Agent { get => agent; }
    public EnemyPath EnemyPath { get => enemyPath; }
    //debugging
    

    void Start()
    {
        enemyHealth = enemyMaxHealth;
        stateMachine =  GetComponent<EnemyStateMachine>();
        agent = GetComponent<NavMeshAgent>();
        enemyPath = GetComponent<EnemyPath>();
        hitbox = GetComponentInChildren<BoxCollider>();
        rb = GetComponent<Rigidbody>(); 
        stateMachine.Init();
    }

    private void Update()
    {
        if (enemyHealth <= 0)
        {
            enemyDie();
        }
        agent.speed = enemySpeed;
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
    public float EnemyAttackRange
    {
        get { return enemyAttackRange; }
        set { enemyAttackRange = value; }
    }

    public PlayerAttibutes EnemyTarget
    {
        get { return player; }
        set { player = value; }
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

    //convert to lamda?
    IEnumerator Charge()
    {
        float saveSpeed = enemySpeed;
        agent.ResetPath();
        enemySpeed = 0;
        //Debug.Log("charging");
        //transform.Translate(move * enemySpeed * Time.deltaTime);
        yield return new WaitForSeconds(3);
        //Debug.Log("Charged");
        hitbox.enabled = true;
        transform.LookAt((player.transform.position - transform.position) * chargeStrength);
        rb.AddForce((player.transform.position - transform.position) * chargeStrength, ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        yield return new WaitForSeconds(0.5f);
        //Debug.Log("attack done");

        hitbox.enabled = false;
        enemySpeed = saveSpeed;
        rb.velocity = Vector3.zero;
        //agent.acceleration = 8;
        //agent.stoppingDistance = 0;
        stateMachine.changeState(new EnemyIdleState());
    }

    public bool CanSeePlayer()
    {
        if(player != null)
        {
            if(Vector3.Distance(transform.position, player.transform.position) < detectionRange)
            {
                Vector3 targetDirection = player.transform.position - transform.position;
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if(angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray lineOfSight = new Ray(transform.position, targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if(Physics.Raycast(lineOfSight,out hitInfo, detectionRange))
                    {
                        if(hitInfo.transform.gameObject == player.gameObject)
                        {
                            //Debug.Log("I SEE YOU");
                            return true;
                        }
                    }
                    Debug.DrawRay(lineOfSight.origin, lineOfSight.direction*detectionRange);
                }
            }
        }
        //Debug.Log("missing");
        return false;
    }

    [ContextMenu("Attack")]
    public bool enemyAttack()
    {
        StartCoroutine(Charge());
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerAttibutes>();
        if (player != null)
        {
            Debug.Log("PlayerHIt");
            player.PlayerHealth = player.PlayerHealth - enemyDamage;
        }
    }


}
