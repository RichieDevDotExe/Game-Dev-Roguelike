using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Entity
{
	[Header("Player Stats")]
	[SerializeField]private float playerAttackRate;
	[SerializeField]private float attackCooldown;

    private PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }

    public float PlayerAttackRate
	{
		get { return playerAttackRate; }
		set { playerAttackRate = value; }
	}

	protected override void entityAttack()
	{
        playerAttack.playerAttack();
    }

	protected override void entityDie()
	{
        Destroy(gameObject);
    }

    //unsure if this is normal
    public void playerDie()
    {
        entityDie();
    }

    public override void entityTakeDamage(float damage)
    {
        health -= damage;
    }
}
