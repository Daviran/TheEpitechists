using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DestructibleObjects : MonoBehaviour
{
    public static Action objectDestroy;
    bool canInterract;
    [SerializeField] Image sprite;
    Sprite brokenWall;

    void Start()
    {
        canInterract = false;
        brokenWall = Resources.Load<Sprite>("/Sprites/RawSprites/Map/map_16");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canInterract = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInterract = false;
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Interract") && canInterract)
        {
            if(gameObject.CompareTag("destructibleWall"))
            {
                sprite.overrideSprite = brokenWall;
                objectDestroy?.Invoke();
            }
            else
            {
                Destroy(gameObject);
                objectDestroy?.Invoke();
            }
        }
    }
}
