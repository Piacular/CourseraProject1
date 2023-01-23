using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;

public class MovementRecognizer : MonoBehaviour
{
    //Right hand input
    public XRNode inputSource1;
    public InputHelpers.Button inputButton1;
    public InputHelpers.Button inputButton2;
    public Transform movementSource1;
    private bool endingRight, endingLeft;

    //Left hand input
    public XRNode inputSource2;
    public InputHelpers.Button inputButton21;
    public InputHelpers.Button inputButton22;
    public Transform movementSource2;

    public float inputThreshhold = 0.1f;
    public GameObject DebugCubePrefab1;
    public GameObject DebugCubePrefab2;
    public bool creationMode = false;
    public string newGestureName;
    public AlienScript alienScript;
    public bool isWaitingForWave;

    public Animator AlienAnim;

    public float recognitionThreshhold = 0.9f;

    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
    public UnityStringEvent OnRecognized;

    public float newPositionThresholdDistance = 0.15f;

    private List<Gesture> trainingSet = new List<Gesture>();
    private bool isMoving1 = false;
    private bool isMoving2 = false;
    private List<Vector3> positionList1 = new List<Vector3>();
    private List<Vector3> positionList2 = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        alienScript = this.GetComponentInParent<AlienScript>();
        AlienAnim = GetComponentInParent<Animator>();
        
        string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (var item in gestureFiles)
        {
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Right Hand input pressed
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource1), inputButton1, out bool isPressed1, inputThreshhold);
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource1), inputButton2, out bool isPressed2, inputThreshhold);

        //Left hand input pressed
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource2), inputButton21, out bool isPressed21, inputThreshhold);
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource2), inputButton22, out bool isPressed22, inputThreshhold);

        if (isWaitingForWave)
        {
            //Start Movement right
            if (!isMoving1 && !isPressed1 && !isPressed2 &&!endingRight)
            {
                //Debug.Log("Start movement1");
                StartMovementRight();
            }

            //Ending Movement right
            else if (isMoving1 && isPressed1 && !endingRight || isMoving1 && isPressed2 && !endingRight)
            {
                //Debug.Log("End movement1");
                EndMovementRight();
            }

            //Updating movement right
            else if (isMoving1 && !isPressed1 && !endingRight || isMoving1 && !isPressed2 && !endingRight)
            {
                if (positionList1.Count < 20)
                {
                    //Debug.Log("updating movement1");
                    UpdateMovementRight();
                }

                else
                {
                    EndMovementRight();
                }
            }


            //Start movement left
            if (!isMoving2 && !isPressed21 && !isPressed22 && !endingLeft)
            {
                //Debug.Log("Start movement2");
                StartMovementLeft();
            }

            //Ending Movement left
            else if (isMoving2 && isPressed21 && !endingLeft || isMoving2 && isPressed22 && !endingLeft)
            {
                //Debug.Log("End movement2");
                EndMovementLeft();
            }

            //Updating movement left
            else if (isMoving2 && !isPressed21 && !endingLeft || isMoving2 && !isPressed22 && !endingLeft)
            {
                if (positionList2.Count < 20)
                {
                    //Debug.Log("updating movement2");
                    UpdateMovementLeft();
                }

                else
                {
                    EndMovementLeft();
                }
            }
        }
    }

    void StartMovementRight()
    {
        //Debug.Log("Start Movement1");
        isMoving1 = true;
        positionList1.Clear();
        positionList1.Add(movementSource1.position);

        if (DebugCubePrefab1)
        {
            Destroy(Instantiate(DebugCubePrefab1, movementSource1.position, Quaternion.identity), 3);
        }
    }

    void EndMovementRight()
    {
        endingRight = true;
        //Debug.Log("End Movement1");
        isMoving1 = false;

        //Create Gesture from Pos_list
        Point[] pointArray1 = new Point[positionList1.Count];

        if (pointArray1.Length < 2)
        {
            endingRight = false;
            return;
        }

        for (int i = 0; i < positionList1.Count; i++)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionList1[i]);
            pointArray1[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }

        Gesture newGesture = new Gesture(pointArray1);
        
        //Add a gesture to training set
        if (creationMode)
        {
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);

            string fileName = Application.persistentDataPath + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointArray1, newGestureName, fileName);
            endingRight = false;
        }

        //recognize
        else
        {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log(result.GestureClass + result.Score);
            if (result.Score > recognitionThreshhold)
            {
                OnRecognized.Invoke(result.GestureClass);
                SetisWaitingWave(false);
                AlienAnim.SetBool("PlayerWave", true);
                endingRight = false;
            }
            else endingRight = false;
        }
    }

    void UpdateMovementRight()
    {
        //Debug.Log("Update Movement1");
        Vector3 lastPosition = positionList1[positionList1.Count - 1];
        if (Vector3.Distance(movementSource1.position, lastPosition) > newPositionThresholdDistance)
        {
            positionList1.Add(movementSource1.position);
            if (DebugCubePrefab1)
            {
                Destroy(Instantiate(DebugCubePrefab1, movementSource1.position, Quaternion.identity), 3);
            }

        }
    }

    void StartMovementLeft()
    {
        //Debug.Log("Start Movement2");
        isMoving2 = true;
        positionList2.Clear();
        positionList2.Add(movementSource2.position);

        if (DebugCubePrefab2)
        {
            Destroy(Instantiate(DebugCubePrefab2, movementSource2.position, Quaternion.identity), 3);
        }
    }

    void EndMovementLeft()
    {
        endingLeft = true;
        //Debug.Log("End Movement2");
        isMoving2 = false;

        //Create Gesture from Pos_list
        Point[] pointArray2 = new Point[positionList2.Count];

        if (pointArray2.Length < 2)
        {
            endingLeft = false;
            return;
        }

        for (int i = 0; i < positionList2.Count; i++)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionList2[i]);
            pointArray2[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }

        Gesture newGesture = new Gesture(pointArray2);

        //Add a gesture to training set
        if (creationMode)
        {
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);

            string fileName = Application.persistentDataPath + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointArray2, newGestureName, fileName);
            endingLeft = false;
        }

        //recognize
        else
        {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log(result.GestureClass + result.Score);
            if (result.Score > recognitionThreshhold)
            {
                OnRecognized.Invoke(result.GestureClass);
                SetisWaitingWave(false);
                AlienAnim.SetBool("PlayerWave", true);
                endingLeft = false;
            }

            else endingLeft = false;
        }
    }

    void UpdateMovementLeft()
    {
        //Debug.Log("Update Movement2");
        Vector3 lastPosition = positionList2[positionList2.Count - 1];
        if (Vector3.Distance(movementSource2.position, lastPosition) > newPositionThresholdDistance)
        {
            positionList2.Add(movementSource2.position);
            if (DebugCubePrefab2)
            {
                Destroy(Instantiate(DebugCubePrefab2, movementSource2.position, Quaternion.identity), 3);
            }

        }
    }

    public bool GetisWaitingForWave()
    {
        return isWaitingForWave;
    }

    public void SetisWaitingWave(bool temp)
    {
        isWaitingForWave = temp;
    }
}
