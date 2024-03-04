using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_FunctionActivatorTrigger : MonoBehaviour
{
    public string tagToLookFor;
    public UnityEvent OnTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if(tagToLookFor != "")
        {
            if (other.transform.CompareTag(tagToLookFor))
            {
                OnTriggered.Invoke();
            }
        }
        else
        {
            OnTriggered.Invoke();
        }
    }
}
