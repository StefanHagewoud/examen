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
                    ActivateTrigger("Enter");
                    return;
                }
            }
            else
            {
                ActivateTrigger("Enter");
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
                    ActivateTrigger("Exit");
                    return;
                }
            }
            else
            {
                ActivateTrigger("Exit");
            }
        }
    }
    private void ActivateTrigger(string triggerMethod)
    {
        //Checking if player is allowed to activate and which method is being used
        if(timesCanActivate < 0)
        {
            if (triggerMethod == "Enter")
            {
                OnEnterTriggered.Invoke();
            }
            else if (triggerMethod == "Exit")
            {
                OnExitTriggered.Invoke();
            }
            return;
        }

        if (timesCanActivate > 0)
        {
            timesCanActivate--;
            if (triggerMethod == "Enter")
            {
                OnEnterTriggered.Invoke();
            }
            else if (triggerMethod == "Exit")
            {
                OnExitTriggered.Invoke();
            }
        }
        if (disableWhenMaxActivated && timesCanActivate == 0)
        {
            gameObject.SetActive(false);
        }

    }
}
