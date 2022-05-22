using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    #region Singelton
    static private SceneController _instance;
    static public SceneController Instance { get { return _instance; } }
    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    #endregion 

    [SerializeField] private string _loadingSceneName = "LoadingScene";

    //variable de uso
    private string _nextScene;

    public void CallScene(string sceneName)
    {
        _nextScene = sceneName;
        PoolManager.Instance.Clear();
        SceneManager.LoadScene(_loadingSceneName);
    }
    public void Reset()
    {
        CallScene(SceneManager.GetActiveScene().name);
    }
    public void AutoCallNextScene(LoadingScene loading)
    {
        StartCoroutine(LoadAsyncScene(loading));
    }

    private IEnumerator LoadAsyncScene(LoadingScene loading)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(_nextScene);
        while (!async.isDone)
        {
            loading.UpdateUI(async.progress);
            yield return new WaitForFixedUpdate();
        }
    }
}
