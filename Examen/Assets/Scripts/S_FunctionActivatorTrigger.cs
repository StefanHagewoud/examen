using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_FunctionActivatorTrigger : MonoBehaviour
{
    //Dit script heeft Ruben gemaakt, omdat de artists het nodig hadden.
    public int timesCanActivate = -1;
    public bool disableWhenMaxActivated = true;
    public string tagToLookFor;
    public UnityEvent OnTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if(tagToLookFor != "")
        {
            if (other.transform.CompareTag(tagToLookFor))
            {
                ActivateTrigger();
            }
        }
        else
        {
            ActivateTrigger();
        }
    }
    private void ActivateTrigger()
    {
        //Checking if player allowed to activate
        if(timesCanActivate < 0)
        {
            OnTriggered.Invoke();
            return;
        }

        if (timesCanActivate > 0)
        {
            timesCanActivate--;
            OnTriggered.Invoke();
        }
        if (disableWhenMaxActivated && timesCanActivate == 0)
        {
            gameObject.SetActive(false);
        }

    }
}
