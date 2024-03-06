using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    private PlayerActionMap playerInput; 
    private PlayerActionMap.PlayerActions playerActions;
    private PlayerAttack playerAttack;
    private PlayerAttibutes playerAtri;
    private PlayerMotor playerMotor;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerActionMap();
        playerMotor = GetComponent<PlayerMotor>();
        playerAttack = GetComponent<PlayerAttack>();
        playerAtri = GetComponent<PlayerAttibutes>();
        playerActions = playerInput.Player;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMotor.ProcessMove(playerActions.Movement.ReadValue<Vector2>(),playerAtri.PlayerSpeed);
        playerActions.Attack.performed += ctx => playerAttack.playerAttack();
        if (playerAtri.PlayerHealth <= 0)
        {
            playerDie();
        }
    }

    void playerDie()
    {
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }
}
