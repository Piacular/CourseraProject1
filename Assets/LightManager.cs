using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class LightManager : MonoBehaviour
{
    [SerializeField] GameObject Flashlight;
    public bool isActive = false;
    //private bool toggling = false;
    //public AudioSource source;
    //public AudioClip onSound;
    //public AudioClip offSound;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        Flashlight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle()
    {
        Debug.Log("Toggle() triggered.");

            if (isActive == false)
            {
                //source.PlayOneShot(onSound);
                Debug.Log("Toggle on triggered, isActive = " + isActive);
                Flashlight.SetActive(true);
                Debug.Log("Setting isActive to true");
                isActive = true;
            }

            else
            {
                //source.PlayOneShot(offSound);
                Debug.Log("Toggle off triggered, isActive = " + isActive);
                Flashlight.SetActive(false);
                Debug.Log("Setting isActive to false");
                isActive = false;
            }

            Debug.Log("Toggle() finishing, setting toggling to false");

    }
}
