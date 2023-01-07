using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class UFOAnimanager : MonoBehaviour
{
    [SerializeField] GameObject Ship;
    [SerializeField] GameObject Lights;
    [SerializeField] GameObject MagicButton;

    //audio files
    public AudioSource Hum_Ab4, Hum_Db4, Hum_F3, Hum_F5, Hum_Ques, HovLoopSlo, HovLoopFst, HovLand, HovLeave;


    public Animator shipAnim;
    public Animator lightsAnim;
    MagicRockscript buttonScript;
    private bool ufoSummoned = false;
    private bool ufoHere = false;
    private bool lightsOn = false;

    
    // Start is called before the first frame update
    void Start()
    {
        lightsAnim = GetComponentInChildren<Animator>();
        Lights.SetActive(false);
        Ship.SetActive(false);
        buttonScript = MagicButton.GetComponent<MagicRockscript>();
    }

    public void PlayPitches()
    {
        
        Hum_Ab4.Play();
        Hum_Db4.Play();
        Hum_F3.Play();
        Hum_F5.Play();
        Hum_Ques.Play();
    }

    public void StopPitches()
    {
        Hum_Ab4.Pause();
        Hum_Db4.Pause();
        Hum_F3.Pause();
        Hum_F5.Pause();
        Hum_Ques.Pause();
    }

    public void PlayHover()
    {
        HovLoopFst.Play();
        HovLoopSlo.Play();
    }

    public void PlayLand()
    {
        HovLand.Play();
    }

    public void PlayLeave()
    {
        HovLeave.Play();
    }

    public void StopHover()
    {
        HovLoopFst.Pause();
        HovLoopSlo.Pause();
    }

    public void LightsOn()
    {
        if (lightsOn == false)
        {
            lightsOn = true;
            Debug.Log("UFO lights on.");
            //PlayPitches();
            lightsAnim.Play("LightsTurnOn");
        }
        else
        {
            Debug.Log("LightsOn() failed, already on");
        }
    }

    public void LightsOff()
    {
        if (Lights.activeInHierarchy == true)
        {
            lightsOn = false;
            Debug.Log("lightsAnim Playing LightsTurnOff.");
            lightsAnim.Play("LightsTurnOff");
            Debug.Log("StoppingPitches");
            StopPitches();
            
        }
        else
        {
            Debug.Log("LightsOff() failed, already off");
        }
    }


    public void SummonUFO()
    {
        Debug.Log("UFOAnim SummonUFO() - summoned = " + ufoSummoned + ", here = " + ufoHere);

        if (ufoSummoned == false && ufoHere == false)
        {
            Debug.Log("UFO Anim SummonUFO() success");
            UFOreset();
            ufoSummoned = true;
            Debug.Log("ufoSummoned set true. ufoHere = " + ufoHere);
            Ship.SetActive(true);
            Debug.Log("UFO has been summoned, playing Fly-In");
            shipAnim.Play("Fly-In");
        }
        
        else
        {
            Debug.Log("SummonUFO() failed.");
        }
    }

    public void BanishUFO()
    {
        Debug.Log("UFOAnim banishUFO() - summoned = " + ufoSummoned + ", here = " + ufoHere);

        if (ufoSummoned == true && ufoHere == true)
        {

            Debug.Log("Calling LightsOff()");
            LightsOff();
            Debug.Log("UFO is leaving.");
            shipAnim.Play("Fly-Out");
            ufoSummoned = false;
        }
        else
        {
            Debug.Log("BanishUFO() failed.");
        }
    }

    public void SetShipActive()
    {
        Ship.SetActive(true);
    }

    public void SetHereFalse()
    {
        Debug.Log("UFO Animanager 'here' set false");
        ufoHere = false;
        buttonScript.SetHere(false);
    }

    public void SetSummonedFalse()
    {
        Debug.Log("UFO Animanager 'ufoSummoned' set false");
        ufoSummoned = false;
    }

    public void SetHereTrue()
    {
        Debug.Log("UFO Animanager 'ufoHere' set true");
        ufoHere = true;
        buttonScript.SetHere(true);
    }

    public void SetSummonedTrue()
    {
        Debug.Log("UFO Animanager 'ufoSummoned' set true");
        ufoSummoned = true;
    }

    public void UFOreset()
    {
        buttonScript.ResetUFO();
    }
}
