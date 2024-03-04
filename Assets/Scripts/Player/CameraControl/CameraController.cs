using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followDistance;
    [SerializeField] private Quaternion rotation;


    private void Update()
    {
        Vector3 pos = Vector3.Lerp(transform.position, player.position + offset + -transform.forward *followDistance ,cameraSpeed*Time.deltaTime);
        transform.position = pos;
        transform.rotation = rotation;
    }
}
