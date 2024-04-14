using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trader : InteractableObject
{
    [SerializeField]private Canvas playerUI;
    [SerializeField]private Canvas traderUI;
    [SerializeField]private TraderOptions traderOptions;
    private Animator traderAnimator;
    private Entity player;
    private Modifers[] traderInventory;

    private Modifers buff;
    private float buffStrength;
    private Modifers debuff;
    private float debuffStrength;

    private TMP_Text buffDesc;
    private TMP_Text debuffDesc;

    private void Awake()
    {
        traderAnimator = GetComponent<Animator>();
        traderInventory = traderOptions.ModiferList;
        debuffDesc = traderUI.gameObject.transform.Find("Debuff").gameObject.transform.Find("DebuffOffer").gameObject.GetComponent<TMP_Text>();
        buffDesc = traderUI.gameObject.transform.Find("Buff").gameObject.transform.Find("BuffOffer").gameObject.GetComponent<TMP_Text>();
        player = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").GetComponent<Player>();
    }

    private float genModStrengthPercent(bool isBuff)
    {
        if (isBuff) 
        {
            float ModStrength = (float)System.Math.Round(Random.Range(1, 1.5f), 2);
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
        Destroy(gameObject);
    }
    public void TradeRejecteded()
    {
        Debug.Log("Trade Rejected");
        Time.timeScale = 1;
        playerUI.enabled = true;
        traderUI.enabled = false;
        Destroy(gameObject);
    }

}
