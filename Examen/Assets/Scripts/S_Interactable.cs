using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_Interactable : MonoBehaviour//Dit script heeft Ruben gemaakt, zodat als je op interact drukt je een UnityEvent kan activeren.
{
    public int timesCanActivate = -1;//-1 == Infinite
    public bool disableWhenMaxActivated = false;
    public UnityEvent<GameObject> OnInteracted;
    public void Interacted(GameObject interactor)
    {
        if(timesCanActivate == 0)
        {
            return;
        }

        if (timesCanActivate > 0)
        {
            timesCanActivate--;
        }
        OnInteracted.Invoke(interactor);
        
        if (timesCanActivate == 0)
        {
            if (disableWhenMaxActivated)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
