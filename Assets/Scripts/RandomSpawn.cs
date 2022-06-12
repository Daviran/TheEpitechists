using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public PNJInstance[] PNJLists;
    Vector3 spawn;
    float x = 0;
    float y = 0;
    float z = 0;
    void Start()
    {
        foreach (PNJInstance pnj in PNJLists)
        {
            x = Random.Range(-9, 9);
            y = Random.Range(-4, 4);
            spawn = new Vector3(x, y, z);
            Instantiate(pnj, spawn, Quaternion.identity);
            Debug.Log("plop");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
