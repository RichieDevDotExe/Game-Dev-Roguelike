using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Hitbox")]
    private BoxCollider hitbox;
    [SerializeField] private LayerMask enemyLayers;
    private AudioClip swordSwingFX;
    private Enemy enemy;
    private Player player;
    private Animator animator;

    void Update()
    {

    }

    void Start()
    {
        //script is attached to weapon in player prephab not player
        hitbox = GetComponent<BoxCollider>();
        player = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").GetComponent<Player>();
        swordSwingFX = player.swordSwingSFX;
        animator = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").GetComponent<Animator>();
    }

    //public void playerAttack()
    //{
    //    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
    //    {
    //        player.EntitySpeed = 0;
    //        Debug.Log("attack");
    //        Collider[] hitEnemies = Physics.OverlapBox(hitbox.position + (Vector3.up * hitBoxOffsetY) + (Vector3.forward * hitBoxOffsetX), hitBoxSize, hitbox.rotation, enemyLayers);
    //        animator.SetTrigger("isAttacking");
    //        foreach (Collider targetEnemy in hitEnemies)
    //        {
    //            Debug.Log("Hit");
    //            enemy = targetEnemy.gameObject.GetComponent<Enemy>();
    //            Debug.Log(enemy.name);
    //            enemy.entityTakeDamage(player.EntityDamage);
    //        }
    //    }
    //}

    //sets animator to attack and plays attack sound
    public void playerAttack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("RightHand@Attack01"))
        {
            animator.SetTrigger("isAttacking");
            Debug.Log("Attack");
            SoundFXManager.instance.playSoundEffect(swordSwingFX, transform, 1f);
        }
    }

    //called in animator
    public void activateAttackHitBox()
    {
        if (Time.time - player.LastPlayerAttack >= player.AttackCooldown)
        {
            player.EntitySpeed = 0f;
            player.LastPlayerAttack = Time.time;
            hitbox.enabled = true;
        }
    }

    public void deactivateAttackHitBox()
    {
        hitbox.enabled = false;
        player.EntitySpeed = 7;
    }

    //checks if enemy hits collider and deals damage
    //Should be noted collider and script is found attached to the weapon the player prephab
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.entityTakeDamage(player.EntityDamage);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawCube(hitbox.position, hitBoxSize);
    //}
}