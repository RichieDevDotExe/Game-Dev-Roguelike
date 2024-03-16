using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask mask;

    public void CanSeeInteractable()
    {
        Debug.Log("AttemptInteract");
        Ray lineOfSight = new Ray(transform.position, player.transform.forward);
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(lineOfSight, out hitInfo, 2,mask))
        {
            hitInfo.collider.GetComponent<InteractableObject>().Interact();
        }
    }
}
