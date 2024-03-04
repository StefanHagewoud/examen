using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_DialogeManager : MonoBehaviour
{
    public List<GameObject> dialoges;
    public UnityEvent onDialogeCompleted;

    [Header("Debug")]
    public bool dialogeEnabled;
    public bool allowDebug;//Add Diagram
    private int crDialoge;

    public void StartDialoge()
    {
        if (dialogeEnabled)
        {
            Debug.LogWarning("Dialoge has already started!");
            return;
        }
        dialogeEnabled = true;
        SkipNextDialoge();
    }
    public void SkipNextDialoge()
    {
        if (!dialogeEnabled)
        {
            return;
        }
        if (allowDebug)
        {
            print("DialogeNumber Before: " + crDialoge);
        }

        if (crDialoge >= dialoges.Count)//Als het de laatste dialoge is.
        {
            EndDialoge();
            return;
        }
        else
        {
            if(crDialoge -1 >= 0)
            {
                dialoges[crDialoge -1].SetActive(false);
            }
            if(crDialoge <= dialoges.Count)
            {
                dialoges[crDialoge].SetActive(true);
            }
            crDialoge++;
        }

        if (allowDebug)
        {
            print("DialogeNumber After: " + crDialoge);
        }
    }
    public void EndDialoge()
    {
        dialogeEnabled = false;
        dialoges[dialoges.Count -1].SetActive(false);
        crDialoge = 0;

        onDialogeCompleted.Invoke();
    }

}
