using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform player;
    private Vector3 pos;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followDistance;
    [SerializeField] private Quaternion rotation;


    private void Start()
    {
        player = GameObject.Find("Player").transform.Find("Character_Male_Rouge_01").Find("Root").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            pos = Vector3.Lerp(transform.position, player.position + offset + -transform.forward * followDistance, cameraSpeed * Time.deltaTime);
            transform.position = pos;
            transform.rotation = rotation;
        }
        else 
        {
            transform.position = pos;
            transform.rotation = rotation;
        }
    }
}
