using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_InteractManager : MonoBehaviour
{
    public UnityEvent OnInteracted;//Add Diagram
    public Transform playerTransform;//Add Diagram
    public float raycastRange = 1.25f;//Add Diagram
    public List<string> tagsToCheck = new List<string>(new string[1]);//Add Diagram

    [Header("Debug")]
    public bool allowDebug;//Add Diagram

    public void ActivateInteract()//Add Diagram
    {
        RaycastHit hit = new();
        if(Physics.Raycast(playerTransform.position, playerTransform.forward, out hit, raycastRange))
        {
            for (int i = 0; i < tagsToCheck.Count; i++)
            {
                if (tagsToCheck[i] == "")
                {
                    Interacts(hit);
                    return;
                }
                else
                {
                    if (hit.transform.CompareTag(tagsToCheck[i]))
                    {
                        Interacts(hit);
                        return;
                    }
                }
            }
        }
    }
    public void Interacts(RaycastHit hit)//Add Diagram
    {
        bool interactedActivated = true;

        if (hit.transform.GetComponent<S_Interactable>())
        {
            hit.transform.GetComponent<S_Interactable>().Interacted(gameObject);
        }
        else if (hit.transform.GetComponentInChildren<S_Interactable>())
        {
            hit.transform.GetComponentInChildren<S_Interactable>().Interacted(gameObject);
        }
        else if (hit.transform.GetComponentInParent<S_Interactable>())
        {
            hit.transform.GetComponentInParent<S_Interactable>().Interacted(gameObject);
        }
        else
        {
            interactedActivated = false;
        }

        OnInteracted.Invoke();

        if (allowDebug)
        {
            if (interactedActivated)
            {
                print("Player raycasted towards and interacted with: " + hit.transform.name);
            }
            else
            {
                print("Player raycasted towards: " + hit.transform.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (allowDebug && playerTransform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(playerTransform.position, playerTransform.position + (playerTransform.forward * raycastRange));
        }
    }

}
