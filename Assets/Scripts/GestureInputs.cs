using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureInputs : MonoBehaviour
{
    [SerializeField] private float errorMargin = 5;
    [SerializeField] private float idleTime = 0.5f;

    [SerializeField] private GestureData horizontalGesture;
    [SerializeField] private GestureData verticalGesture;
    [SerializeField] private GestureData diagonalGesture;
    [SerializeField] private GestureData plusGesture;

    private Vector2 _initialPos;
    private Vector3 _nullVector = new Vector3(-10000, -10000, -1000);

    private GestureData achievedGesture;

    private List<GestureData> gestureList;

    private void Awake()
    {
        gestureList = new List<GestureData>();

        horizontalGesture = new GestureData(new Vector3[] { new Vector3(100, 0, 0) }, "horizontal");
        verticalGesture = new GestureData(new Vector3[] { new Vector3(0, 0, 100) }, "vertical");
        diagonalGesture = new GestureData(new Vector3[] { new Vector3(100, 0, 100) }, "diagonal");
        plusGesture = new GestureData(new Vector3[] { new Vector3(0, 0, 100), new Vector3(100,0, 0) }, "plus");
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                ResetList();
                _initialPos = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 deltaMovement = Input.GetTouch(0).position - _initialPos;
                CheckGestures(deltaMovement);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                _initialPos = _nullVector;
            }
        }
    }

    private void CheckGestures(Vector3 deltaMovement)
    {
        GestureData[] gestureAux = gestureList.ToArray();

        foreach (GestureData gesture in gestureAux)
        {
            if (!CheckGesture(gesture, deltaMovement))
            {
                gestureList.Remove(gesture);
            }
        }

        if (gestureList.Count == 1)
        {
            Debug.Log("El gesto que se dio fue: " + achievedGesture._name);
            ResetList();
        }
        else if (gestureList.Count == 0)
        {
            Debug.Log("No se dio ningun gesto");
            ResetList();
        }
    }

    private bool CheckGesture(GestureData gesture, Vector3 deltaMovement)
    {
        if ((gesture.movements[gesture.index] - deltaMovement).magnitude <= errorMargin)
        {
            gesture.index++;
            if (gesture.index == gesture.movements.Length)
            {
                achievedGesture = gesture;
                return true;
            }
        }
        return false;
    }

    private void ResetList()
    {
        achievedGesture = null;

        gestureList.Clear();
        gestureList.Add(horizontalGesture);
        gestureList.Add(verticalGesture);
        gestureList.Add(diagonalGesture);
        gestureList.Add(plusGesture);
    }
}
public class GestureData
{
    public GestureData(Vector3[] gestures, string name)
    {
        _name = name;
        index = 0;
        _movement = gestures;
    }

    private Vector3[] _movement;
    public Vector3[] movements { get { return _movement; } }

    public int index;
    public string _name;
}
