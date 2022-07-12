using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJController : MonoBehaviour
{

    public PNJ[] pnjs;

    private void Awake()
    {
        pnjs = FindObjectsOfType<PNJ>();
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
