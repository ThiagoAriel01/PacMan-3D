using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripter : MonoBehaviour
{
    void Start(){ 
        Invoke("EndSlash",5.0f); 
    }

    private void EndSlash(){ 
        SceneController.Instance.CallScene("SceneA"); 
    }

}
