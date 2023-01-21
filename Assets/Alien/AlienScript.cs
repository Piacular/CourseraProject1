using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour
{

    public GameObject Ship, Instrument;
    public Transform ShipSeatTransform, HandsTransform;
    public Quaternion ShipSeatQuat;
    public Animator AlienAnim;
    public bool Carrying, Piloting, Descending, Grounded;
    public float RotateSpeed;
    Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        AlienAnim = GetComponent<Animator>();

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

    }

    public void BeginPilot()
    {
        if (!Piloting)
        {
            Piloting = true;
            this.GetComponent<Rigidbody>().useGravity = false;
            AlienAnim.Play("Piloting");
        }

        if (Descending)
        {
            transform.Rotate( RotateSpeed * Time.deltaTime, 0f, 0f, Space.Self);
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

    public void PlayLanding()
    {
        if (Descending)
        {
            Grounded = true;
            Descending = false;
            AlienAnim.Play("Landing1");
        }
    }

    public bool GetDescending()
    {
        return Descending;
    }

}
