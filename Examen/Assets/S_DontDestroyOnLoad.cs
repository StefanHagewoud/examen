using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
