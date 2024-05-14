using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shopkeeper : InteractableObject
{
    private Canvas playerUI;
    private Canvas shopkeeperUI;
    [SerializeField]private ShopkeeperOptions shopkeeperOptions;
    private Animator shopkeeperAnimator;
    private Entity player;
    private Modifers[] shopkeeperInventory;

    private Modifers offer;
    private float offerStrength;
    private int cost;

    private Button acceptButton;
    private Button rejectButton;
    private int x = 10;

    private TMP_Text offerDesc;
    private TMP_Text costDesc;

    [SerializeField] private AudioClip shopkeeperGreetingsSFX;

    private Action<GameObject> destroyThis;
    private void Awake()
    {
        shopkeeperAnimator = GetComponent<Animator>();
        shopkeeperInventory = shopkeeperOptions.ModiferList;
        playerUI = GameObject.Find("PlayerUI").GetComponent<Canvas>();
        shopkeeperUI = GameObject.Find("TraderUI").GetComponent<Canvas>();
        acceptButton = shopkeeperUI.gameObject.transform.Find("Yes").GetComponent<Button>();
        rejectButton = shopkeeperUI.gameObject.transform.Find("No").GetComponent<Button>();

        costDesc = shopkeeperUI.transform.Find("Debuff").gameObject.transform.Find("DebuffOffer").gameObject.GetComponent<TMP_Text>();
        offerDesc = shopkeeperUI.transform.Find("Buff").gameObject.transform.Find("BuffOffer").gameObject.GetComponent<TMP_Text>();
        player = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").GetComponent<Player>();
    }

    private void Update()
    {
        if(player != null) {
            transform.LookAt(player.transform.position);
        }
    }

    //Randomly generates a price
    private int genCost()
    {
        cost = Random.Range(40, 70);
        return cost;
    }

    //Randomly generates a mod strength to pass into a mod
    private float genOfferPercent()
    {
        offerStrength = (float)System.Math.Round(Random.Range(1, 1.5f), 2);
        Debug.Log("generate buff strength " + offerStrength);
        return offerStrength;
    }

    //activates animation when interacted with
    public void InteractStart()
    {
        shopkeeperAnimator.SetTrigger("interact");
    }

    //Picks a random buff and generates a price
    public override void Interact()
    {
        Debug.Log("Trade?");
        InteractStart();
        SoundFXManager.instance.playSoundEffect(shopkeeperGreetingsSFX, transform, 1f);
        if (offer == null)
        {
            offer = shopkeeperInventory[Random.Range(0, shopkeeperInventory.Length)];
            //Debug.Log("buff = " + buff.GetType() + " debuff" + debuff.GetType());
            offerStrength = genOfferPercent();
            cost = genCost();
            offer.generateModDesc(offerStrength);
            offerDesc.text = offer.ModDescription;
            //Debug.Log("offer type = "+offer);
            //Debug.Log("cost of trade = " + cost);
            costDesc.text = "It will cost you " + cost.ToString() + " gold";
            Time.timeScale = 0;
        }
        //need to reset Listeners when trade is generated because the methods being passed only use the variable values when called
        //e.g cost is set as 0 when shopkeeper is generated. when a cost is later generated in the Interact() function the TradeAccepted() function would still 
        //only use the value it was originally. that being 0.
        acceptButton.onClick.RemoveAllListeners();
        rejectButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(() => TradeAccepted());
        rejectButton.onClick.AddListener(() => TradeRejecteded());
        playerUI.enabled = false;
        shopkeeperUI.enabled = true;
    }

    //Trade Accepted Logic
    public void TradeAccepted()
    {
        InteractStart();
        Debug.Log("Trade Accepted");
        //Debug.Log("x = "+ x);
        Time.timeScale = 1; 
        //Debug.Log("player gold = "+ player.Gold);
        //Debug.Log("cost of trade = "+ cost);
        
        //Needs to check if player has enough gold to buy the buff
        //If has enough apply mod and then destroy object
        if (player.Gold >= cost)
        {
            //Debug.Log("trade accepted2");
            //Debug.Log("type " + offer.GetType());
            offer.applyMod(player, offerStrength);
            player.Gold = player.Gold - cost;
            playerUI.enabled = true;
            shopkeeperUI.enabled = false;
            StartCoroutine(shopkeeperDest());
        }
        //if not enough money. do nothing
        else 
        {
            playerUI.enabled = true;
            shopkeeperUI.enabled = false;
        }
    }

    //Trade Reject Logic
    public void TradeRejecteded()
    {
        InteractStart();
        Debug.Log("Trade Rejected");
        Time.timeScale = 1;
        playerUI.enabled = true;
        shopkeeperUI.enabled = false;
    }

    //Used for object pooling to pass in remove function
    public void giveDestroy(Action<GameObject> destroyFunct)
    {
        destroyThis = destroyFunct;
    }


    IEnumerator shopkeeperDest()
    {
        yield return new WaitForSeconds(1f);
        destroyThis(transform.parent.gameObject);
    }

    //Used from the NextLevelUI class to reset the world and remove all traders from previous run.
    public void resetThis()
    {
        destroyThis(transform.parent.gameObject);
    }

}
