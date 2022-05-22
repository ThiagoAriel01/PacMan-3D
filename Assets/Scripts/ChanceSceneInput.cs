using UnityEngine;

public class ChanceSceneInput : MonoBehaviour
{
    [SerializeField] private string _nextScene = "SceneB";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneController.Instance.CallScene(_nextScene);
        }
    }
}
