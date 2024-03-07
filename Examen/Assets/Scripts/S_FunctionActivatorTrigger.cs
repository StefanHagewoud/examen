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

    [Header("EnterTrigger")]
    public bool allowTriggerEnter = true;
    public UnityEvent OnEnterTriggered;

    [Header("ExitTrigger")]
    public bool allowExitTrigger = false;
    public UnityEvent OnExitTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (!allowTriggerEnter)
        {
            return;
        }

        for (int i = 0; i < tagsToLookFor.Count; i++)
        {
            if (tagsToLookFor[i] != "")
            {
                if (other.transform.CompareTag(tagsToLookFor[i]))
                {
                    ActivateTrigger();
                    return;
                }
            }
            else
            {
                ActivateTrigger();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!allowExitTrigger)
        {
            return;
        }

        for (int i = 0; i < tagsToLookFor.Count; i++)
        {
            if (tagsToLookFor[i] != "")
            {
                if (other.transform.CompareTag(tagsToLookFor[i]))
                {
                    ActivateTrigger();
                    return;
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
            OnEnterTriggered.Invoke();
            return;
        }

        if (timesCanActivate > 0)
        {
            timesCanActivate--;
            OnEnterTriggered.Invoke();
        }
        if (disableWhenMaxActivated && timesCanActivate == 0)
        {
            gameObject.SetActive(false);
        }

    }
}
