using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Hitbox")]
    private BoxCollider hitbox;
    [SerializeField] private LayerMask enemyLayers;
    private Enemy enemy;
    private Player player;
    private Animator animator;

    void Update()
    {

    }

    void Start()
    {
        hitbox = GetComponent<BoxCollider>();
        player = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").GetComponent<Player>();
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

    public void playerAttack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        { 
            Debug.Log("attack");
            StartCoroutine(activateHitBox());
        }
    }

    private IEnumerator activateHitBox()
    {
        if (Time.time - player.LastPlayerAttack >= player.AttackCooldown)
        {
            player.LastPlayerAttack = Time.time;
            hitbox.enabled = true;
            animator.SetTrigger("isAttacking");
            yield return new WaitForSeconds(1.4f);
            hitbox.enabled = false;
        }
    }

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