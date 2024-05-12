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

    public void InteractStart()
    {
        traderAnimator.SetTrigger("interact");
    }

    public override void Interact()
    {
        Debug.Log("Trade?");
        buff = traderInventory[Random.Range(0, traderInventory.Length)];
        debuff = traderInventory[Random.Range(0, traderInventory.Length)];
        while (buff.GetType() == debuff.GetType())
        {
            debuff = traderInventory[Random.Range(0, traderInventory.Length)];
        }
        //Debug.Log("buff = " + buff.GetType() + " debuff" + debuff.GetType());

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

    public void TradeAccepted()
    {
        Debug.Log("Trade Accepted");
        Time.timeScale = 1;
        buff.applyMod(player,buffStrength);
        debuff.applyMod(player, debuffStrength);
        playerUI.enabled = true;
        traderUI.enabled = false;
        StartCoroutine(traderDest());
    }
    public void TradeRejecteded()
    {
        Debug.Log("Trade Rejected");
        Time.timeScale = 1;
        playerUI.enabled = true;
        traderUI.enabled = false;
        StartCoroutine(traderDest());
    }

    public void giveDestroy(Action<GameObject> destroyFunct)
    {
        destroyThis = destroyFunct;
    }

    IEnumerator traderDest()
    {
        yield return new WaitForSeconds(1f);
        destroyThis(transform.parent.gameObject);
    }

    public void resetThis()
    {
        destroyThis(transform.parent.gameObject);
    }

}
