using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttibutes : MonoBehaviour
{
	[Header("PLayer Stats")]
    [SerializeField]private float playerHealth;
	[SerializeField]private float playerDamage;
	[SerializeField]private float playerSpeed;
	[SerializeField]private float playerAttackRate;
	[SerializeField]private float attackCooldown;

    public float PlayerAttackRate
	{
		get { return playerAttackRate; }
		set { playerAttackRate = value; }
	}


	public float PlayerSpeed
	{
		get { return playerSpeed; }
		set { playerSpeed = value; }
	}

	public float PlayerDamage
	{
		get { return playerDamage; }
		set { playerDamage = value; }
	}

	public float PlayerHealth
	{
		get { return playerHealth; }
		set { playerHealth = value; }
	}







}
