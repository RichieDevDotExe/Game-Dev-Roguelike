using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

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

    private int genCost()
    {
        cost = Random.Range(40, 70);
        return cost;
    }

    private float genOfferPercent()
    {
        offerStrength = (float)System.Math.Round(Random.Range(1, 1.5f), 2);
        Debug.Log("generate buff strength " + offerStrength);
        return offerStrength;
    }

    public void InteractStart()
    {
        shopkeeperAnimator.SetTrigger("interact");
    }

    public override void Interact()
    {
        Debug.Log("Trade?");
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

    public void TradeAccepted()
    {
        Debug.Log("Trade Accepted");
        //Debug.Log("x = "+ x);
        Time.timeScale = 1; 
        //Debug.Log("player gold = "+ player.Gold);
        //Debug.Log("cost of trade = "+ cost);
        if (player.Gold >= cost)
        {
            //Debug.Log("trade accepted2");
            //Debug.Log("type " + offer.GetType());
            offer.applyMod(player, offerStrength);
            player.Gold = player.Gold - cost;
            playerUI.enabled = true;
            shopkeeperUI.enabled = false;
            Destroy(gameObject);
        }
        else 
        {
            playerUI.enabled = true;
            shopkeeperUI.enabled = false;
        }
    }
    public void TradeRejecteded()
    {
        Debug.Log("Trade Rejected");
        Time.timeScale = 1;
        playerUI.enabled = true;
        shopkeeperUI.enabled = false;
    }

}
