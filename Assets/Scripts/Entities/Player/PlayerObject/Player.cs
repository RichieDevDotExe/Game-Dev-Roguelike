using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Entity
{
	[Header("Player Stats")]
	[SerializeField]private float attackCooldown;
    private float lastPlayerAttack;

    [Header("Player Items")]
    [SerializeField] private int potions;
    [SerializeField] private int maxPotions;
    [SerializeField] private int gold;

    private PlayerAttack playerAttack;
    private Collider hitbox;
    private Animator animator;

    private void Start()
    {
        playerAttack = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").transform.Find("Root").transform.Find("Hips").transform.Find("Spine_01").transform.Find("Spine_02").transform.Find("Spine_03").transform.Find("Clavicle_R").transform.Find("Shoulder_R").transform.Find("Elbow_R").transform.Find("Hand_R").transform.Find("SM_Prop_SwordOrnate_01").transform.Find("weaponHitBox").gameObject.GetComponent<PlayerAttack>();
        hitbox = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    public float LastPlayerAttack
    {
        get { return lastPlayerAttack; }
        set { lastPlayerAttack = value; }
    }

    public float AttackCooldown
    {
        get { return attackCooldown; }
        set { attackCooldown = value; }
    }


    public override int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

    public int Potions
    {
        get { return potions; }
        set { potions = value; }
    }

    public Collider Hitbox
    {
        get { return hitbox; }
        set { hitbox = value; }
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
        if (Time.time - iFrameStart >= iFrames)
        {
            health -= damage;
            iFrameStart = Time.time;
        }
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
        //Debug.Log("item");
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
