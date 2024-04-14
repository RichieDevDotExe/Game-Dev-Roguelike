using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 lookPos;
    private float speed;
    Vector3 lookDir;
    private float idleTimer;
    private Animator animator;
    private int layerMask = 31;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessMove(Vector2 input,float playerSpeed)
    {
        if((input.x != 0) || (input.y != 0)) {
            idleTimer = 0f;
            animator.SetBool("idleTimer", false);
            speed = playerSpeed;
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;  
            //var targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            characterController.Move(moveDirection * speed * Time.deltaTime);

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit,layerMask)) 
            {
                lookPos = hit.point;
            }
            lookDir = lookPos - transform.position;
            lookDir.y = 0;

            if (moveDirection.magnitude > 1.0f)
            {
                moveDirection = moveDirection.normalized;
            }

            moveDirection = transform.InverseTransformDirection(moveDirection);


            animator.SetFloat("VelX",moveDirection.x*4);
            animator.SetFloat("VelZ", moveDirection.z*4);
            animator.SetBool("isRunning", true);
        }
        else if((input.x == 0) && (input.y == 0) && (idleTimer >= 15)){
            idleTimer = 0f;
            animator.SetBool("idleTimer", true);
        }
        else
        {
            idleTimer += Time.deltaTime;
            animator.SetFloat("VelX", 0);
            animator.SetFloat("VelZ", 0);
            animator.SetBool("isRunning", false);
            //Debug.Log(idleTimer);
        }
        transform.LookAt(transform.position + lookDir, Vector3.up);
    }


}
