using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameObject levelComplete;
    [SerializeField] private AudioClip die;
    [SerializeField] private GameObject input;
    private int vida = 3;
    private void Start()
    {
        vida = hearts.Length;
    }
    void Update()
    {
        if (vida < 1)
        {
            //animation die
            hearts[0].gameObject.SetActive(false);
            AudioManager.Instance.Play3DClip(die, 0.5f, false, this.gameObject.transform.position, 1, 50);
            Invoke("Recargar",1.5f);
            input.SetActive(false);
        }
        else if (vida < 2)
        {
            //animation hit
            hearts[1].gameObject.SetActive(false);
        }
        else if (vida < 3)
        {
            //animation hit
            hearts[2].gameObject.SetActive(false);
        }
    }

    public void RestarVida()
    {
        if (!levelComplete.activeInHierarchy == true){
            vida--;
        }       
    }

    private void Recargar()
    {
        SceneController.Instance.Reset();
        input.SetActive(true);
    }
}
