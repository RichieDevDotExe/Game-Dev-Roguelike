using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Entity
{
	[Header("Player Stats")]
	[SerializeField]private float playerAttackRate;
	[SerializeField]private float attackCooldown;

    [Header("Player Items")]
    [SerializeField] private int potions;
    [SerializeField] private int maxPotions;
    [SerializeField] private int gold;

    private PlayerAttack playerAttack;
    private Collider hitbox;
    private Animator animator;

    private void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        hitbox = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    public float PlayerAttackRate
	{
		get { return playerAttackRate; }
		set { playerAttackRate = value; }
	}

    public Animator PlayerAnimator
    {
        get { return animator; }
        set { animator = value; }
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

    public void playerPotionHeal()
    {
        if(potions > 0 ) 
        { 
            modList.health.applyMod(this,1.2f);
            potions -= 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject item = other.gameObject;
        Debug.Log("item");
        if (item.tag == "Item")
        {
            Items itemInfo = item.GetComponent<Items>();
            Debug.Log(itemInfo.ItemName);
            if (itemInfo.ItemName == "Gold")
            {
                Debug.Log("add gold");
                itemInfo.destroyItem();
                gold = gold + 1;
            }
            if (itemInfo.ItemName == "Potion")
            {
                Debug.Log("add potion");
                itemInfo.destroyItem();
                if(potions < maxPotions)
                {
                    potions = potions + 1;
                }
            }
        }
    }
}
