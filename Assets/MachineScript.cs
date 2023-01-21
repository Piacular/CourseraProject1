using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
    public GameObject LightP, LightG, UFO;
    public Material mShader;
    public bool lightsOn, lightsToggle, onBoard, descending;
    
    // Start is called before the first frame update
    void Start()
    {
        mShader.DisableKeyword("_EMISSION");
        LightP.SetActive(false);
        LightG.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (lightsToggle && lightsOn)
        {
            lightsOn = false;
            TurnOff();
            lightsToggle = false;
        }

        else if (lightsToggle && !lightsOn)
        {
            lightsOn = true;
            TurnOn();
            lightsToggle = false;
        }
    }

    public void TurnOn()
    {
        mShader.EnableKeyword("_EMISSION");
        LightP.SetActive(true);
        LightG.SetActive(true);
    }

    public void TurnOff()
    {
        mShader.DisableKeyword("_EMISSION");
        LightP.SetActive(false);
        LightG.SetActive(false);
    }

}
