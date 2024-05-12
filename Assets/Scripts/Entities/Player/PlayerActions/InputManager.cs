using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
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

    private Canvas playerUI;
    private Canvas gameOverUI;


    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerActionMap();
        playerMotor = GetComponent<PlayerMotor>();
        playerAttack = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").transform.Find("Root").transform.Find("Hips").transform.Find("Spine_01").transform.Find("Spine_02").transform.Find("Spine_03").transform.Find("Clavicle_R").transform.Find("Shoulder_R").transform.Find("Elbow_R").transform.Find("Hand_R").transform.Find("SM_Prop_SwordOrnate_01").transform.Find("weaponHitBox").gameObject.GetComponent<PlayerAttack>();
        player = GetComponent<Player>();
        playerInteraction = GetComponent<PlayerInteraction>();
        playerActions = playerInput.Player;
        playerUI = GameObject.Find("PlayerUI").GetComponent<Canvas>();
        gameOverUI = GameObject.Find("GameOver").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMotor.ProcessMove(playerActions.Movement.ReadValue<Vector2>(),player.EntitySpeed);
        playerInteraction.activateDrops();
        playerActions.Attack.performed += ctx => StartCoroutine(inputAttack());
        playerActions.Dash.performed += ctx => StartCoroutine(activateDodge());
        playerActions.Interact.performed += ctx => playerInteraction.CanSeeInteractable();
        playerActions.Potion.performed += ctx => player.playerPotionHeal(); ;
        //move to game manager
        playerMotor.playerFaceTowards();
        if (player != null)
        {
            if (player.EntityHealth <= 0)
            {
                playerUI.enabled = false;
                gameOverUI.enabled = true;
                Time.timeScale = 0f;
                player.playerDie();
            }
            if (player.EntityHealth > player.EntityMaxHealth)
            {
                player.EntityHealth = player.EntityMaxHealth;
            }
        }
    }

    public IEnumerator activateDodge()
    {
        if ((Time.time - playerMotor.DodgeTimer) >= playerMotor.DashCooldown)
        {
            playerMotor.dodgeMovement();
            //player.Hitbox.enabled = false;
            player.Hitbox.excludeLayers = 1<<6;
            yield return new WaitForSeconds(0.5f);

            player.Hitbox.excludeLayers = 0;
            //player.Hitbox.enabled = true;
            playerMotor.RB.angularVelocity = Vector3.zero;
            playerMotor.RB.velocity = Vector3.zero;
            playerMotor.DodgeTimer = Time.time;
        }
    }

    private IEnumerator inputAttack()
    {
        player.EntitySpeed = 3f;
        playerAttack.playerAttack();
        yield return new WaitForSeconds(1);
        player.EntitySpeed = 7;
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
