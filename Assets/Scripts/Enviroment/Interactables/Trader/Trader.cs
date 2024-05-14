using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Trader : InteractableObject
{
    private Canvas playerUI;
    private Canvas traderUI;
    [SerializeField]private TraderOptions traderOptions;
    private Animator traderAnimator;
    private Entity player;
    private Modifers[] traderInventory;

    private Modifers buff;
    private float buffStrength;
    private Modifers debuff;
    private float debuffStrength;

    private Button acceptButton;
    private Button rejectButton;

    private TMP_Text buffDesc;
    private TMP_Text debuffDesc;

    [SerializeField] private AudioClip traderGreetingsSFX;

    private Action<GameObject> destroyThis;

    private void Awake()
    {
        traderAnimator = GetComponent<Animator>();
        traderInventory = traderOptions.ModiferList;
        playerUI = GameObject.Find("PlayerUI").GetComponent<Canvas>();
        traderUI = GameObject.Find("TraderUI").GetComponent<Canvas>();
        acceptButton = traderUI.gameObject.transform.Find("Yes").GetComponent<Button>();
        rejectButton = traderUI.gameObject.transform.Find("No").GetComponent<Button>();
        acceptButton.onClick.AddListener(TradeAccepted);
        rejectButton.onClick.AddListener(TradeRejecteded);

        debuffDesc = traderUI.gameObject.transform.Find("Debuff").gameObject.transform.Find("DebuffOffer").gameObject.transform.GetComponent<TMP_Text>();
        buffDesc = traderUI.gameObject.transform.Find("Buff").gameObject.transform.Find("BuffOffer").gameObject.transform.GetComponent<TMP_Text>();
        player = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").GetComponent<Player>();
    }

    private void Update()
    {
        if ((player != null))
        {
            transform.LookAt(player.transform.position);
        }
        
    }

    //Randomly generates a mod strength to pass into a mod
    private float genModStrengthPercent(bool isBuff)
    {
        if (isBuff) 
        {
            float ModStrength = (float)System.Math.Round(Random.Range(1, 2f), 2);
            Debug.Log("generate buff strength " + ModStrength);
            return ModStrength;
        }
        else
        {
            float ModStrength = (float)System.Math.Round(Random.Range(0.5f, 1), 2);
            Debug.Log("generate debuff strength "+ + ModStrength);
            return ModStrength;
        }
    }


    //activates animation when interacted with
    public void InteractStart()
    {
        traderAnimator.SetTrigger("interact");
    }

    //Picks a random buff and debuff from it's inventory
    public override void Interact()
    {
        Debug.Log("Trade?");
        InteractStart();
        SoundFXManager.instance.playSoundEffect(traderGreetingsSFX, transform, 1f);
        buff = traderInventory[Random.Range(0, traderInventory.Length)];
        debuff = traderInventory[Random.Range(0, traderInventory.Length)];
        while (buff.GetType() == debuff.GetType())
        {
            debuff = traderInventory[Random.Range(0, traderInventory.Length)];
        }
        //Debug.Log("buff = " + buff.GetType() + " debuff" + debuff.GetType());

        //generates values used for both the debuffs and buffs
        debuffStrength = genModStrengthPercent(false);
        buffStrength = genModStrengthPercent(true);

        buff.generateModDesc(buffStrength);
        debuff.generateModDesc(debuffStrength);

        debuffDesc.text = debuff.ModDescription;
        buffDesc.text = buff.ModDescription;
        //need to reset Listeners when trade is generated because the methods being passed only use the variable values when called
        //e.g cost is set as 0 when shopkeeper is generated. when a cost is later generated in the Interact() function the TradeAccepted() function would still 
        //only use the value it was originally. that being 0.
        acceptButton.onClick.RemoveAllListeners();
        rejectButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(() => TradeAccepted());
        rejectButton.onClick.AddListener(() => TradeRejecteded());

        Time.timeScale = 0;
        playerUI.enabled = false;
        traderUI.enabled = true;
    }

    //Trade Accepted Logic
    public void TradeAccepted()
    {
        Debug.Log("Trade Accepted");
        Time.timeScale = 1;
        InteractStart();
        buff.applyMod(player,buffStrength);
        debuff.applyMod(player, debuffStrength);
        playerUI.enabled = true;
        traderUI.enabled = false;
        StartCoroutine(traderDest());
    }
    //Trade Rejected Logic
    public void TradeRejecteded()
    {
        Debug.Log("Trade Rejected");
        Time.timeScale = 1;
        InteractStart();
        playerUI.enabled = true;
        traderUI.enabled = false;
        StartCoroutine(traderDest());
    }

    //Used for object pooling to pass in remove function
    public void giveDestroy(Action<GameObject> destroyFunct)
    {
        destroyThis = destroyFunct;
    }

    IEnumerator traderDest()
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
