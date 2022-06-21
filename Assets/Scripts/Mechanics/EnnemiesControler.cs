using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using UnityEngine;

public class EnnemiesControler : MonoBehaviour
{
    public List<EnnemiesInstance> ennemies;
    public GameObject[] spawnPoints;
    public List<EnnemiesControler> trueEnnemies;

    private void Awake()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int random = Random.Range(0, 1);
            if (random == 0)
            {
                ennemies.Add(new Bat());
                trueEnnemies[i].gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Ennemies/Bat") as Sprite;
            }
            else 
            {
                ennemies.Add(new OldMac());
                trueEnnemies[i].gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Ennemies/OldMac") as Sprite;
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(trueEnnemies[i], spawnPoints[i].transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
