using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    private Player player;
    private TMP_Text healthDisplay;
    private TMP_Text goldDisplay;
    private TMP_Text potionDisplay;
    private Animator healthAni;
    private Canvas hud;
    private Animator potionAni;
    private Animator goldAni;

    public Animator PotionAni
    {
        get { return potionAni; }
        set { potionAni = value; }
    }

    public Animator GoldAni
    {
        get { return goldAni; }
        set { goldAni = value; }
    }




    // Start is called before the first frame update
    void Start()
    {
        hud = GetComponent<Canvas>();
        healthAni = hud.gameObject.transform.Find("RawImage").GetComponent<Animator>();
        player = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").GetComponent<Player>();  
        healthDisplay = hud.gameObject.transform.Find("RawImage").gameObject.transform.Find("Health").gameObject.GetComponent<TMP_Text>();
        goldDisplay = hud.gameObject.transform.Find("GoldCount").gameObject.GetComponent<TMP_Text>();
        potionDisplay = hud.gameObject.transform.Find("PotionCount").gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            healthDisplay.text = player.EntityHealth.ToString();
            if (player.EntityHealth / player.EntityMaxHealth <= 0.3)
            {
                healthAni.SetBool("lowHealth", true);
                healthAni.speed = 2f;
            }
            if (player.EntityHealth / player.EntityMaxHealth >= 0.3)
            {
                healthAni.SetBool("lowHealth", false);
                healthAni.speed = 1f;
            }
            goldDisplay.text = player.Gold.ToString();
            potionDisplay.text = player.Potions.ToString();
        }
    }
}
