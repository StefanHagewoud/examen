using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Cutscene : MonoBehaviour
{
    public Animator maffiaAutos;

    public void MaffiaCutscene()
    {
        maffiaAutos.SetTrigger("Maffiaauto Cutscene");
    }
}
