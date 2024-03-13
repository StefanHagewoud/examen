using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_DialogeManager : MonoBehaviour
{
    public List<Dialoge> dialoges = new List<Dialoge>(new Dialoge[1]);
    public UnityEvent<int> onDialogeCompleted;
    public Animator animationCutscene;//Add Diagram

    [Header("Debug")]
    public bool dialogeEnabled;
    public bool allowDebug;//Add Diagram
    public bool disablePlayerMovement = true;//Add Diagram
    private int crDialoge;
    private int dialogeFrame;//Add Diagram

    [Header("Scripts")]
    private S_PlayerMovement playerMovementScript;//Add Diagram

    [Serializable]
    public class Dialoge 
    {
        public List<DialogeFrame> dialogeFrames = new List<DialogeFrame>(new DialogeFrame[1]);

        [Serializable]
        public class DialogeFrame
        {
            public List<GameObject> gameObjectsToChange = new List<GameObject>(new GameObject[1]);
        }
    }
    private void Start() {
        playerMovementScript = GameObject.FindGameObjectWithTag("Rolling").GetComponent<S_PlayerMovement>();
    }

    public void StartDialoge(int dialogeNumber)
    {
        if (dialogeEnabled)
        {
            Debug.LogWarning("Dialoge has already started!", this);
            return;
        }
        if (!animationCutscene)
        {
            Debug.LogWarning("Variable animationCutscene is not attached to the script!", this);
        }

        crDialoge = dialogeNumber;
        dialogeEnabled = true;
        playerMovementScript.allowAnyMovement = false;
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
            print("DialogeNumber Before: " + dialogeFrame);
        }

        if (dialogeFrame >= dialoges[crDialoge].dialogeFrames.Count)//Als het de laatste dialoge is.
        {
            EndDialoge();
            return;
        }
        else
        {
            if(dialogeFrame -1 >= 0)//Disables de vorige dialoge.
            {
                for (int i = 0; i < dialoges[crDialoge].dialogeFrames[dialogeFrame - 1].gameObjectsToChange.Count; i++)
                {
                    dialoges[crDialoge].dialogeFrames[dialogeFrame - 1].gameObjectsToChange[i].SetActive(false);
                }
            }
            if(dialogeFrame <= dialoges[crDialoge].dialogeFrames.Count)//Enables de volgende dialoge.
            {
                for (int i = 0; i < dialoges[crDialoge].dialogeFrames[dialogeFrame].gameObjectsToChange.Count; i++)
                {
                    dialoges[crDialoge].dialogeFrames[dialogeFrame].gameObjectsToChange[i].SetActive(true);
                }
            }
            dialogeFrame++;
        }

        if (allowDebug)
        {
            print("DialogeNumber After: " + dialogeFrame);
        }
    }
    public void EndDialoge()
    {
        dialogeEnabled = false;
        animationCutscene.SetBool("Cutscene", false);//Elke dialoge eindigt met een animatie.

        for (int i = 0; i < dialoges[crDialoge].dialogeFrames[dialogeFrame - 1].gameObjectsToChange.Count; i++)//Veranderd alle GameObjects naar false in een dialoge.
        {
            dialoges[crDialoge].dialogeFrames[dialoges[crDialoge].dialogeFrames.Count - 1].gameObjectsToChange[i].SetActive(false);
        }

        if (allowDebug)
        {
            print("DialogeFrames done on: " + dialogeFrame);
        }
        int crDialogeFrame = dialogeFrame;
        dialogeFrame = 0;
        playerMovementScript.allowAnyMovement = true;
        onDialogeCompleted.Invoke(crDialogeFrame);
    }
        

}
