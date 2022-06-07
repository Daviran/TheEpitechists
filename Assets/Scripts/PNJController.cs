using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJController : MonoBehaviour
{

    public PNJInstance[] pnjs;

    int randomIndex;


    private void Awake()
    {
        pnjs = FindObjectsOfType<PNJInstance>();
        for (var i = 0; i < pnjs.Length; i++)
        {
            pnjs[i].controllerIndex = i;
            pnjs[i].pnjController = this;
        }
        randomIndex = Random.Range(0, pnjs[0].dialogs.Length);
    }
    void Update()
    {
    }
}
