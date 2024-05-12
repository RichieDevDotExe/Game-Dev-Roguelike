using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelUI : MonoBehaviour
{
    private GameObject[] chests;
    private GameObject[] enemies;
    private GameObject[] shopkeepers;
    private GameObject[] traders;

    private GameObject[] spawners;
    private GameObject player;
    private Canvas playerUI;
    private Canvas nextLevelUI;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextLevel()
    {
        chests = GameObject.FindGameObjectsWithTag("Chest");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        shopkeepers = GameObject.FindGameObjectsWithTag("Shopkeeper");
        traders = GameObject.FindGameObjectsWithTag("Trader");
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        playerUI = GameObject.Find("PlayerUI").GetComponent<Canvas>();
        nextLevelUI = GameObject.Find("NextLevelUI").GetComponent<Canvas>();

        foreach (GameObject chest in chests)
        {
            chest.GetComponent<Chest>().resetThis();
        }
        foreach (GameObject enemy in enemies)
        {
            Debug.Log(enemy.name);
            enemy.transform.Find("Character_BR_BigOrk_01").GetComponent<Enemy>().resetThis();
        }
        foreach (GameObject shopkeeper in shopkeepers)
        {
            shopkeeper.transform.Find("Character_Male_Wizard_01").GetComponent<Shopkeeper>().resetThis();
        }
        foreach (GameObject trader in traders)
        {
            trader.transform.Find("Character_Male_Wizard_01").GetComponent<Trader>().resetThis();
        }

        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<SpawnerBase>().spawn();
        }

        Debug.Log("Next Level");
        player.transform.position = new Vector3((float)23.2463226, (float)-21.7536564, (float)-15.1050415);
        playerUI.enabled = true;
        nextLevelUI.enabled = false;
        Time.timeScale = 1f;
        //SceneManager.LoadScene("TempLevel");
    }
}
