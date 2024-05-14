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

    [Header("Player Audio Libary")]
    public AudioClip swordSwingSFX;
    public AudioClip takeDamageSFX;
    public AudioClip dashSFX;
    public AudioClip potionSFX;
    public AudioClip itemCollectSFX;

    private PlayerAttack playerAttack;
    private Collider hitbox;
    private Animator animator;
    private int difficulty;
    
    

    private void Start()
    {
        playerAttack = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").transform.Find("Root").transform.Find("Hips").transform.Find("Spine_01").transform.Find("Spine_02").transform.Find("Spine_03").transform.Find("Clavicle_R").transform.Find("Shoulder_R").transform.Find("Elbow_R").transform.Find("Hand_R").transform.Find("SM_Prop_SwordOrnate_01").transform.Find("weaponHitBox").gameObject.GetComponent<PlayerAttack>();
        hitbox = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    //used to keep track of how many complete runs has the player done. The more runs the more health and damage enemies will spawn with
    public int Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
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
            SoundFXManager.instance.playSoundEffect(takeDamageSFX, transform, 1f);
            iFrameStart = Time.time;
        }
    }

    public void playerPotionHeal()
    {
        if(potions > 0 ) 
        { 
            modList.health.applyMod(this,1.2f);
            SoundFXManager.instance.playSoundEffect(potionSFX, transform, 1f);
            potions -= 1;
        }
    }

    //If a item collides with the collider it checks what item it is and then adds 1 to the counter
    private void OnTriggerEnter(Collider other)
    {
        GameObject item = other.gameObject;
        //Debug.Log("item");
        if (item.tag == "Item")
        {
            Items itemInfo = item.GetComponent<Items>();
            Debug.Log(itemInfo.ItemName);
            SoundFXManager.instance.playSoundEffect(itemCollectSFX, transform, 0.9f);
            if (itemInfo.ItemName == "Gold")
            {
                Debug.Log("add gold");
                itemInfo.destroyItem();
                gold = gold + 1;
            }
            //if its a potion it needs to check if it would go over the potion max. 
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
