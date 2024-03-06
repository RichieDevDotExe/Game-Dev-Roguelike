using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController characterController;
    private Vector3 playerVel;
    private Vector3 lookPos;
    private float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessMove(Vector2 input,float playerSpeed)
    {
        speed = playerSpeed;
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;  
        //var targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        characterController.Move(moveDirection * speed * Time.deltaTime);

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(mouseRay, out hit) ) 
        {
            lookPos = hit.point;
        }
        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);
    }


}
