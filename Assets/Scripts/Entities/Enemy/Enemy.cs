using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;
using Random = UnityEngine.Random;

public class Enemy : Entity
{
    [Header("Enemy Stats")]
    [SerializeField]private EnemySettings enemySettings;
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

    [Header("Enemy SFX Libary")]
    [SerializeField] public AudioClip enemyAttackSFX;
    [SerializeField] public AudioClip enemyReadyChargeSFX;
    [SerializeField] public AudioClip enemyHitSFX;
    [SerializeField] public AudioClip enemyDieSFX;


    [Header("Enemy Misc")]
    [SerializeField] private Player player;

    private EnemyStateMachine stateMachine;
    private Action<GameObject> destroyThis;
    private Animator enemyAnimator;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Canvas playerUI;
    private Canvas nextLevelUI;
    public NavMeshAgent Agent { get => agent; }
    public EnemyPath EnemyPath { get => enemyPath; }
    //debugging
    

    public void Init()
    {
        Debug.Log("Init" + name);
        maxHealth = enemySettings.MaxHealth *(1+(0.2f*player.Difficulty));
        health = maxHealth;
        speed = enemySettings.Speed;
        damage = enemySettings.Damage * (1 + (0.2f * player.Difficulty));
        detectionRange = enemySettings.detectionRange;
        fieldOfView = enemySettings.fieldOfView;
        enemyAttackRange = enemySettings.AttackRange;
        enemyCooldown = enemySettings.enemyCooldown;
        chargeStrength = enemySettings.chargeStrength;
        maxSpeed = enemySettings.maxSpeed;
        stateMachine.Init();
        agent = GetComponent<NavMeshAgent>();
        enemyPath = GetComponent<EnemyPath>();
    }

    void Awake()
    {
        playerUI = GameObject.Find("PlayerUI").GetComponent<Canvas>();
        nextLevelUI = GameObject.Find("NextLevelUI").GetComponent<Canvas>();
        player = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").GetComponent<Player>();
        enemyAnimator = GetComponent<Animator>();
        stateMachine =  GetComponent<EnemyStateMachine>();
        hitbox = transform.Find("HitBox").gameObject.GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        if (EntityHealth <= 0)
        {
            Debug.Log(transform.parent.name);
            //if its the boss enemy trigger game over screen
            if (transform.parent.name == "BossMelee(Clone)")
            {
                Time.timeScale = 0f;
                playerUI.enabled = false;
                nextLevelUI.enabled = true;
            }
            entityDie();
        }
        enemyAnimator.SetFloat("Velx", rb.velocity.x);
        enemyAnimator.SetFloat("Velx", rb.velocity.z);

        agent.speed = EntitySpeed;
    }

    public float EnemyAttackRange
    {
        get { return enemyAttackRange; }
        set { enemyAttackRange = value; }
    }

    public Player EnemyTarget
    {
        get { return player; }
        set { player = value; }
    }

    public override void entityTakeDamage(float damage)
    {
        if (Time.time - iFrameStart >= iFrames)
        {
            SoundFXManager.instance.playSoundEffect(enemyHitSFX,transform,1f);
            health -= damage;
            iFrameStart = Time.time;
        }
    }

    protected override void entityDie()
    {
        //Destroy(gameObject);
        SoundFXManager.instance.playSoundEffect(enemyDieSFX, transform, 1f);
        destroyThis(transform.parent.gameObject);
    }

    //enemy attack will be different for every single enemy so this shouldn't be defined. will be defined now however so it can be tested
    //public void enemyAttack;

    //convert to lamda?
    IEnumerator Charge()
    {
        //wind ups a charge attack. changes animation to represent this state
        float saveSpeed = speed;
        agent.ResetPath();
        EntitySpeed = 0;
        enemyAnimator.SetBool("IsCharging", true);
        //Debug.Log("charging");
        //transform.Translate(move * enemySpeed * Time.deltaTime);
        SoundFXManager.instance.playSoundEffect(enemyReadyChargeSFX, transform, 1f);
        yield return new WaitForSeconds(3);
        //lunges at enemy
        SoundFXManager.instance.playSoundEffect(enemyAttackSFX, transform, 1f);
        enemyAnimator.SetBool("IsCharging", false);
        enemyAnimator.SetBool("IsAttacking", true);
        enemyAnimator.speed = 3;
        //Debug.Log("Charged");
        hitbox.enabled = true;
        //transform.Find("Root").gameObject.

        //sudden change in velocity is due to a addforce function directed in the players direction. Before add force happens it needs it cap the distance the enemy can lunge
        Vector3 forceCal = (player.transform.position - transform.position) * chargeStrength;
        if (forceCal.magnitude > maxSpeed)
        {
            var direction = forceCal.normalized;
            forceCal = direction * maxSpeed;
        }
        rb.AddForce(forceCal, ForceMode.Impulse);
        Vector3 targetDirection = player.transform.position;
        transform.LookAt(targetDirection);
        yield return new WaitForSeconds(0.5f);
        //reset to default state
        enemyAnimator.SetBool("IsAttacking", false);
        enemyAnimator.speed = 1;
        //Debug.Log("attack done");

        hitbox.enabled = false;
        speed = saveSpeed;
        rb.velocity = Vector3.zero;
        //agent.acceleration = 8;
        //agent.stoppingDistance = 0;
        stateMachine.changeState(new EnemyIdleState());
    }

    //checks if player is in field of view
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


    //unsure if this way of using attack is good
    [ContextMenu("Attack")]
    public void enemyAttack()
    {
        entityAttack();
    }

    protected override void entityAttack()
    {
        StartCoroutine(Charge());
    }

    public void giveDestroy(Action<GameObject> destroyFunct)
    {
        destroyThis = destroyFunct;
    }

    //checks if enemy hits player
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("PlayerHIt");
            player.entityTakeDamage(damage);
        }
    }

    public void resetThis()
    {
        destroyThis(transform.parent.gameObject);
        Init();
    }

}
