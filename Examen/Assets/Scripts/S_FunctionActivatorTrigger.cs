using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.LowLevel;

public class S_FunctionActivatorTrigger : MonoBehaviour
{
    //Dit script heeft Ruben gemaakt, omdat de artists het nodig hadden.
    public int timesCanActivate = -1;
    public bool disableWhenMaxActivated = true;
    public List<string> tagsToLookFor = new List<string>(new string[1]);
    public UnityEvent OnTriggered;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < tagsToLookFor.Count; i++)
        {
            if (tagsToLookFor[i] != "")
            {
                if (other.transform.CompareTag(tagsToLookFor[i]))
                {
                    ActivateTrigger();
                }
            }
            else
            {
                ActivateTrigger();
            }
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
