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
    private Player player;
    private PlayerMotor playerMotor;
    private PlayerInteraction playerInteraction;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerActionMap();
        playerMotor = GetComponent<PlayerMotor>();
        playerAttack = GetComponent<PlayerAttack>();
        player = GetComponent<Player>();
        playerInteraction = GetComponent<PlayerInteraction>(); 
        playerActions = playerInput.Player;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMotor.ProcessMove(playerActions.Movement.ReadValue<Vector2>(),player.EntitySpeed);
        playerActions.Attack.performed += ctx => playerAttack.playerAttack();
        playerActions.Interact.performed += ctx => playerInteraction.CanSeeInteractable();

        //move to game manager
        if (player.EntityHealth <= 0)
        {
            player.playerDie();
        }
        if (player.EntityHealth > player.EntityMaxHealth)
        {
            player.EntityHealth = player.EntityMaxHealth;
        }
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
