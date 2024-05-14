using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    //called on game over screen. loads scene again
    public void resetGame()
    {
        Debug.Log("resetting Level");
        Time.timeScale = 1f;
        SceneManager.LoadScene("TempLevel");
    }
}
