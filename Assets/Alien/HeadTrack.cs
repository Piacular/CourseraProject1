using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrack : MonoBehaviour
{
    Animator animator;
    public bool ikActive = false, looking = false;
    public float lookWeight = 0;
    public Transform gazeTarget;

    //dummy pivot

    GameObject objPivot;

    private void Start()
    {
        animator = GetComponent<Animator>();

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
        //Debug.Log("pivotRotY");

        //Distance limiter:
        float dist = Vector3.Distance(objPivot.transform.position, gazeTarget.position);
        //Debug.Log(dist);

        //Rotation limiter:
        if (pivotRotY < 0.6f && pivotRotY > -0.6f && dist < 5)
        {
            //Target tracking
            lookWeight = Mathf.Lerp(lookWeight, 1 ,Time.deltaTime * 2.5f);
        }

        else
        {
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
}
