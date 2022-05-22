using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Image _loadinBar;
    [SerializeField] private Text _loadinText;
    // Start is called before the first frame update
    void Start()
    {
        SceneController.Instance.AutoCallNextScene(this);
    }

    public void UpdateUI(float progress)
    {
        _loadinText.text = Mathf.Round(progress * 100).ToString();
        _loadinBar.fillAmount = progress;
    }
}
