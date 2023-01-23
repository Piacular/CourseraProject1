using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrack : MonoBehaviour
{
    Animator animator;
    //AlienScript alienScript;
    public bool ikActive = false, looking = false;
    public float lookWeight = 0;
    [SerializeField] Transform gazeTarget;
    [SerializeField] GameObject Alien;

    //dummy pivot

    GameObject objPivot;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //alienScript = Alien.GetComponent<AlienScript>();

        //Dummy Pivot
        objPivot = new GameObject("DummyPivot");
        objPivot.transform.parent = transform;
        objPivot.transform.localPosition = new Vector3(0, 0.9f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //target position 1
        objPivot.transform.LookAt(gazeTarget);
        float pivotRotY = objPivot.transform.localRotation.y;

        //Distance limiter:
        float dist = Vector3.Distance(objPivot.transform.position, gazeTarget.position);

        //Rotation limiter:
        if (pivotRotY < 0.6f && pivotRotY > -0.6f && dist < 7)
        {
            if (!looking)
            {
                Debug.Log("Looking set true");
                looking = true;
            }
            //Target tracking
            lookWeight = Mathf.Lerp(lookWeight, 1 ,Time.deltaTime * 2.5f);
        }

        else
        {
            if (looking)
            {
                Debug.Log("Looking set false");
                looking = false;
            }
            //Target release
            lookWeight = Mathf.Lerp(lookWeight, 0, Time.deltaTime * 2.5f);
        }
    }

    private void OnAnimatorIK()
    {
        if (animator)
        {
            if (ikActive)
            {
                // set looking true or false
                
                if (gazeTarget != null)
                {
                    animator.SetLookAtWeight(lookWeight);
                    animator.SetLookAtPosition(gazeTarget.position);
                }
            }
            else
            {
                animator.SetLookAtWeight(lookWeight);
            }
        }
    }
    public bool PlayerSeen()
    {
        return looking;
    }
}
