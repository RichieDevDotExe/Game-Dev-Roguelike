using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform hitbox;
    [SerializeField] private float itemHitBoxSize;
    [SerializeField] private LayerMask itemLayers;
    [SerializeField] private float hitBoxOffsetY;
    private Items itemNav;

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

    public void activateDrops()
    {
        //Debug.Log("collect item");
        Collider[] itemsHitBox = Physics.OverlapSphere(hitbox.position + (Vector3.up * hitBoxOffsetY), itemHitBoxSize, itemLayers);
        foreach (Collider item in itemsHitBox)
        {
            //Debug.Log("item detected");
            itemNav = item.GetComponent<Items>();
            itemNav.activateItem();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (hitbox == null)
        {
            return;
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(hitbox.position + (Vector3.up * hitBoxOffsetY), itemHitBoxSize);
    }
}
