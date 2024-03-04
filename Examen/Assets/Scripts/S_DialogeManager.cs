using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_DialogeManager : MonoBehaviour
{
    public List<Dialoge> dialoges = new List<Dialoge>();
    public UnityEvent onDialogeCompleted;
    public Animator animationCutscene;//Add Diagram

    [Header("Debug")]
    public bool dialogeEnabled;
    public bool allowDebug;//Add Diagram
    private int crDialoge;
    private int crDialogeFrame;

    [Serializable]
    public class Dialoge 
    {
        public List<DialogeFrame> dialogeFrames = new List<DialogeFrame>(2);

        [Serializable]
        public class DialogeFrame
        {
            public List<GameObject> gameObjectsToChange;
        }
    }

    

    public void StartDialoge(int dialogeNumber)
    {
        if (dialogeEnabled)
        {
            Debug.LogWarning("Dialoge has already started!", this);
            return;
        }
        crDialoge = dialogeNumber;
        dialogeEnabled = true;
        if (!animationCutscene)
        {
            Debug.LogWarning("Variable animationCutscene is not attached to the script!", this);
        }
        animationCutscene.SetBool("Cutscene", true);//Elke dialoge begint met een animatie.

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
            print("DialogeNumber Before: " + crDialogeFrame);
        }

        if (crDialogeFrame >= dialoges[crDialoge].dialogeFrames.Count)//Als het de laatste dialoge is.
        {
            EndDialoge();
            return;
        }
        else
        {
            if(crDialogeFrame -1 >= 0)//Disables de vorige dialoge.
            {
                for (int i = 0; i < dialoges[crDialoge].dialogeFrames[crDialogeFrame - 1].gameObjectsToChange.Count; i++)
                {
                    dialoges[crDialoge].dialogeFrames[crDialogeFrame - 1].gameObjectsToChange[i].SetActive(false);
                }
            }
            if(crDialogeFrame <= dialoges[crDialoge].dialogeFrames.Count)//Enables de volgende dialoge.
            {
                for (int i = 0; i < dialoges[crDialoge].dialogeFrames[crDialogeFrame].gameObjectsToChange.Count; i++)
                {
                    dialoges[crDialoge].dialogeFrames[crDialogeFrame].gameObjectsToChange[i].SetActive(true);
                }
            }
            crDialogeFrame++;
        }

        if (allowDebug)
        {
            print("DialogeNumber After: " + crDialogeFrame);
        }
    }
    public void EndDialoge()
    {
        dialogeEnabled = false;
        animationCutscene.SetBool("Cutscene", false);//Elke dialoge eindigt met een animatie.

        for (int i = 0; i < dialoges[crDialoge].dialogeFrames[crDialogeFrame - 1].gameObjectsToChange.Count; i++)//Veranderd alle GameObjects naar false in een dialoge.
        {
            dialoges[crDialoge].dialogeFrames[dialoges[crDialoge].dialogeFrames.Count - 1].gameObjectsToChange[i].SetActive(false);
        }

        if (allowDebug)
        {
            print("DialogeFrames done on: " + crDialogeFrame);
        }
        crDialogeFrame = 0;

        onDialogeCompleted.Invoke();
    }
        

}
