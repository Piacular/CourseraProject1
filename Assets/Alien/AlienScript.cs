using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour
{

    public GameObject Ship, Instrument;
    public Transform ShipSeatTransform, HandsTransform;
    public Quaternion ShipSeatQuat;
    public Animator AlienAnim;
    public HeadTrack headTrack;
    public bool Carrying, Piloting, Descending, Grounded, playerSeen;
    public float RotateSpeed;
    public MovementRecognizer movementRecognizer;
 
    //Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        AlienAnim = GetComponent<Animator>();
        headTrack = GetComponent<HeadTrack>();
        movementRecognizer = GetComponentInChildren<MovementRecognizer>();

        BeginPilot();
    }

    // Update is called once per frame
    void Update()
    {

        AlienAnim.SetFloat("Prox2Ground", this.transform.position.y);

        //Set Alien to Pilot Position
        if (Piloting)
        {         
            transform.SetPositionAndRotation(ShipSeatTransform.position, ShipSeatTransform.rotation);
        }

        //Descent rotation
        /**if (Descending)
        {
              transform.Rotate(RotateSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        }**/

        if (!headTrack.PlayerSeen())
        {
            playerSeen = false;
            AlienAnim.SetBool("PlayerSeen", false);
        }

        if(headTrack.PlayerSeen())
        {
            playerSeen = true;
            AlienAnim.SetBool("PlayerSeen", true);
        }
    }

    public void BeginPilot()
    {
        if (!Piloting)
        {
            Piloting = true;
            this.GetComponent<Rigidbody>().useGravity = false;
            AlienAnim.Play("Piloting");
        }

    }

    public void ComeDown()
    {
        Debug.Log("ComeDown() called");
        Debug.Log("Set Descending true");
        Descending = true;
        Debug.Log("Set Piloting false");
        Piloting = false;
        Debug.Log("Changing Gravity, Drag, Mass");
        this.GetComponent<Rigidbody>().mass = 0.3f;
        this.GetComponent<Rigidbody>().drag = 3f;
        this.GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("Playing 'Floating Down' Animation");
        AlienAnim.Play("FloatingDown");
    }

    //Animation Events
    /**
     * OLD SETANIM()
     * 
     * public void SetAnim()
    {
        int tempInt = AlienAnim.GetInteger("PlayerUnseenAnim");
        Debug.Log("TempInt = " + tempInt);
        int newInt = 0;
        Debug.Log("NewInt = " + newInt);
        if (tempInt < 5) {
            newInt = tempInt + 1;
            Debug.Log("Setting newInt = " + newInt);
        }
        else
        {
            newInt = 0;
            Debug.Log("Setting newInt = " + newInt);
        }
        Debug.Log("Setting PlayerUnseenAnim from " + tempInt + " to " + newInt);
        AlienAnim.SetInteger("PlayerUnseenAnim", newInt);
    }**/
    public void SetAnim(string paramName, int numInCycle)
    {
        Debug.Log("SetAnim(" + paramName + ", " + numInCycle + ")");
        int max = numInCycle;
        int tempInt = AlienAnim.GetInteger(paramName);
        Debug.Log("TempInt = " + tempInt);
        int newInt = 0;
        Debug.Log("NewInt = " + newInt);
        if (tempInt < max)
        {
            newInt = tempInt + 1;
            Debug.Log("Setting newInt = " + newInt);
        }
        else
        {
            newInt = 0;
            Debug.Log("Setting newInt = " + newInt);
        }
        Debug.Log("Setting " + paramName + " from " + tempInt + " to " + newInt);
        AlienAnim.SetInteger(paramName, newInt);
    }

    public void SetPlayerUnseenAnim()
    {
        SetAnim("PlayerUnseenAnim", 5);
    }

    public void SetPlayerWave()
    {
        SetAnim("WaitingOnWave", 7);
    }

    public void ResetAnim()
    {
        AlienAnim.SetInteger("PlayerUnseenAnim", 0);
    }

    public void RepeatCount(string paramName, int times)
    {
        int cap = times;
        int tempInt = AlienAnim.GetInteger(paramName);
        int newInt;
        string par = paramName;

        if (tempInt < cap)
        {
            newInt = tempInt + 1;
            Debug.Log(par + " = " + newInt);
        }
        else
        {
            newInt = 0;
            Debug.Log("Setting newInt = " + newInt);
        }
        Debug.Log(par + " from " + tempInt + " to " + newInt);
        AlienAnim.SetInteger(par, newInt);
    }

    public void BoredCount()
    {
        RepeatCount("BoredCount", 3);
    }

    public void BoredCountReset()
    {
        RepeatCount("BoredCount", 0);
    }


    //Getters & Setters
    public bool GetDescending()
    {
        return Descending;
    }

    public void SetPlayerInRange(bool visible)
    {
        playerSeen = visible;
    }

    public bool GetPlayerSeen()
    {
        return playerSeen;
    }

    public bool GetisWaitingForWave()
    {
        return movementRecognizer.GetisWaitingForWave();
    }

    public void SetisWaitingWaveTrue()
    {
        movementRecognizer.SetisWaitingWave(true);
    }

    public void SetisWaitingWaveFalse()
    {
        movementRecognizer.SetisWaitingWave(false);
    }
}
