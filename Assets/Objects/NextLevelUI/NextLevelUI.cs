using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NextLevelUI : MonoBehaviour
{
    private GameObject[] chests;
    private GameObject[] enemies;
    private GameObject[] shopkeepers;
    private GameObject[] traders;

    private GameObject[] spawners;
    private GameObject player;
    private Player playerAtri;
    private CharacterController characterController;
    private Canvas playerUI;
    private Canvas nextLevelUI;
    private GameObject spawnPoint;

    void Start()
    {
        player = GameObject.Find("Player");
        spawnPoint = GameObject.Find("SpawnPoint");
        playerAtri = player.transform.Find("Character_Male_Rouge_01").gameObject.GetComponent<Player>();
        characterController = player.transform.Find("Character_Male_Rouge_01").gameObject.GetComponent<CharacterController>();
        playerUI = GameObject.Find("PlayerUI").GetComponent<Canvas>();
        nextLevelUI = GameObject.Find("NextLevelUI").GetComponent<Canvas>();
    }

    //called if player hits button in canvas
    public void nextLevel()
    {
        playerAtri.Difficulty += 1;
        //Checks for any spawnable entities still left in scene
        chests = GameObject.FindGameObjectsWithTag("Chest");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        shopkeepers = GameObject.FindGameObjectsWithTag("Shopkeeper");
        traders = GameObject.FindGameObjectsWithTag("Trader");
        spawners = GameObject.FindGameObjectsWithTag("Spawner");

        //Sends any left over entities to the object pool
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

        //uses spawn() function at each spawner in the map
        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<SpawnerBase>().spawn();
        }

        Debug.Log("Next Level");
        playerUI.enabled = true;
        nextLevelUI.enabled = false;

        //resets player position to the start
        player.SetActive(false);
        characterController.enabled = false;
        Debug.Log(player.transform.position);
        player.transform.Find("Character_Male_Rouge_01").transform.position = spawnPoint.transform.position;
        Debug.Log(player.transform.position);
        characterController.enabled = true;
        player.SetActive(true);
        Time.timeScale = 1f;
        //SceneManager.LoadScene("TempLevel");
    }
}
