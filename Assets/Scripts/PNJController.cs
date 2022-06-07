using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJController : MonoBehaviour
{

    public PNJInstance[] pnjs;

    private void Awake()
    {
        pnjs = FindObjectsOfType<PNJInstance>();
        for (var i = 0; i < pnjs.Length; i++)
        {
            pnjs[i].controllerIndex = i;
            pnjs[i].pnjController = this;
        }
    }
    void Update()
    {
    }
}
