using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text puntosText;
    [SerializeField] private int scoreMax;
    [SerializeField] private string nextScene;
    [SerializeField] private GameObject levelComplete;
    [SerializeField] private AudioClip win;
    private int score = 0;
    private int unit = 0;

    private void Awake(){
        levelComplete.SetActive(false);
    }
    public void RasieScore(int s)
    {
        score += s;
        puntosText.text = score.ToString();
    }

    public void EatBall()
    {
        unit++;
        if (unit >= scoreMax){
            levelComplete.SetActive(true);
            AudioManager.Instance.PlayClip(win, 0.2f, false, AudioManager.ChannelType.UIfx);
            Invoke("NextScene", 2.5f);          
        }        
    }

    private void NextScene(){ SceneController.Instance.CallScene(nextScene); }
}
