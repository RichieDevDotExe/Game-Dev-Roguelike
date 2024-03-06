using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField]
    private PlayerAttibutes player;
    private TMP_Text healthDisplay;
    private Canvas hud;


    // Start is called before the first frame update
    void Start()
    {
        hud = GetComponent<Canvas>();
        healthDisplay = hud.transform.Find("Health").gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = player.PlayerHealth.ToString();
    }
}
