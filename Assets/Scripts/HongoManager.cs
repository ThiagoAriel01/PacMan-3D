using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HongoManager : MonoBehaviour
{
    [SerializeField] private string _poolToUse = "Particulas";

    public void Awake()
    {
        Character player = GameObject.FindWithTag("Player").GetComponent<Character>();
        player.onPastillaComida += OnPastillaComida;
    }
    void OnPastillaComida(Character c, Transform pastilla )
    {
        GameObject poolObject = PoolManager.Instance.GetPoolObject(_poolToUse);
        poolObject.SetActive(true);
        poolObject.transform.position = c.transform.position;
        poolObject.transform.rotation = c.transform.rotation;
    }
}
