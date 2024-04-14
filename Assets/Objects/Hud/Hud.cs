using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    private Player player;
    private TMP_Text healthDisplay;
    private Canvas hud;


    // Start is called before the first frame update
    void Start()
    {
        hud = GetComponent<Canvas>();
        player = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").GetComponent<Player>();  
        healthDisplay = hud.gameObject.transform.Find("RawImage").gameObject.transform.Find("Health").gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = player.EntityHealth.ToString();
    }
}
