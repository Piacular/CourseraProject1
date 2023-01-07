using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRockscript : MonoBehaviour
{
    [SerializeField] GameObject ship;
    [SerializeField] Animator myAnim;

    public AudioSource MButtonPressed, RockHum;

    Vector3 uBasev3;
    Quaternion uBaseQ;

    bool summoned = false;
    bool here = false;
    UFOAnimanager ufoAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponentInParent<Animator>();
        ufoAnim = ship.GetComponent<UFOAnimanager>();
        uBasev3 = ship.transform.position;
        uBaseQ = ship.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MagicButtonPressed()
    {

        Debug.Log("MagicButtonPressed() - summoned = " + summoned + ", here = " + here);

        if (summoned == false && here == false)
        {
            summoned = true;
            PlayButtonSound();
            PlayRockHum();
            Debug.Log("ButtonAnimanager 'summoned' set" + summoned + ", here = " + here);
            Debug.Log("Playing Blue2Green");
            myAnim.Play("Blue2Green");
            Debug.Log("Calling ufoAnim.SummonUFO()");
            ufoAnim.SummonUFO();
            Debug.Log("UFO has been summoned by magic button script.");
        }

        else if (summoned == true && here == true)
        {
            Debug.Log("ButtonAnimanager 'summoned' set false");
            summoned = false;
            PlayButtonSound();
            StopRockHum();
            Debug.Log("Playing Green2Blue");
            myAnim.Play("Green2Blue");
            Debug.Log("Calling ufoAnim.SummonUFO()");
            ufoAnim.BanishUFO();
            Debug.Log("UFO has been banished by magic button script.");
        }

        else
        {
            Debug.Log("MagicButtonPressed() failed. - summoned = " + summoned + ", here = " + here);
        }
    }

    public void SetHere(bool newHere)
    {
        Debug.Log("ButtonAnimanager SetHere() " + newHere);
        here = newHere;
    }

    public void SetSummoned(bool newSummoned)
    {
        Debug.Log("ButtonAnimanager setSummoned() " + newSummoned);
        summoned = newSummoned;

        if (newSummoned == true)
        {
            Debug.Log("Calling ufoAnim SetSummoned(true)");
            ufoAnim.SetSummonedTrue();
        }

        else
        {
            Debug.Log("Calling ufoAnim.SetSummonedTrue()");
        }

    }
    public void ResetUFO()
    {
        Debug.Log("MagButtScript: ResetUFO()");
        ship.transform.SetPositionAndRotation(uBasev3, uBaseQ);
    }

    public void Green2Blue()
    {
        Debug.Log("Playing Green2Blue");

    }

    public void Blue2Green()
    {
        Debug.Log("Playing Blue2Green");
    }

    public void PlayButtonSound()
    {
        MButtonPressed.Play();
    }

    public void PlayRockHum()
    {
        RockHum.Play();
    }

    public void StopRockHum()
    {
        RockHum.Pause();
    }
}

