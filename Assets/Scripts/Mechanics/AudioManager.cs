using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }
    public AudioSource mainOST;
    public AudioSource phone;

    private void OnEnable()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnDisable()
    {
        Destroy(Instance);
    }
    
    public void PlayClip(bool activate)
    {
        if (activate)
            mainOST.Play();
        else
            mainOST.Stop();
    }

    public void PhoneClip(bool activate)
    {
        if (activate)
            phone.Play();
        else
            phone.Stop();
    }
}
