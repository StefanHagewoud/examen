using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_InteractManager : MonoBehaviour
{
    public Transform playerTransform;
    public float raycastRange;
    public RaycastHit raycastHit;
    public string tagToCheck = "Interact";
    public void ActivateInteract()
    {
        Physics.Raycast(playerTransform.position, playerTransform.eulerAngles, out raycastHit, raycastRange);

        if (raycastHit.transform.CompareTag(tagToCheck))
        {

        }
    }
}
