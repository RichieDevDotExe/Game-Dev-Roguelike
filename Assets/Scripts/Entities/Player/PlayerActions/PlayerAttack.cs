using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Hitbox")]
    [SerializeField]private Transform hitbox;
    [SerializeField]private Vector3 hitBoxSize;
    [SerializeField]private LayerMask enemyLayers;
    private Enemy enemy;
    private Player player;
    

    void Update()
    {
    }

    void Start()
    {
        player = GetComponent<Player>();
    }

    public void playerAttack()
    {

        Debug.Log("attack");
        Collider[] hitEnemies = Physics.OverlapBox(hitbox.position, hitBoxSize, hitbox.rotation, enemyLayers);
        
        foreach(Collider targetEnemy in hitEnemies)
        {
            Debug.Log("Hit");
            enemy = targetEnemy.gameObject.GetComponent<Enemy>();
            Debug.Log(enemy.name);
            enemy.entityTakeDamage(player.EntityDamage);
        }
    }

    

    void OnDrawGizmosSelected()
    {
        if (hitbox == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(hitbox.position,hitBoxSize);
    }
}
