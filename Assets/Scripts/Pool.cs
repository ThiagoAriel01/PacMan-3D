using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private string _name;
    private GameObject _poolObject;
    private int _poolCount;
    private Vector3 _cementery;

    //variable de uso
    private GameObject[] _poolObjects;

    public void Initialize(string name, GameObject poolObject, int count, Vector3 cementery, Transform parent)
    {
        _name = name;
        _poolObject = poolObject;
        _poolCount = count;
        _cementery = cementery;
        transform.SetParent(parent);
        InstantiatePool(parent);
    }

    private void InstantiatePool(Transform parent)
    {
        _poolObjects = new GameObject[_poolCount];
        for (int i = 0; i < _poolObjects.Length; i++)
        {
            _poolObjects[i] = Instantiate(_poolObject, parent);
            
            MoveToCementery(_poolObjects[i]);
        }
    }

    public GameObject GetPoolObject()
    {
        foreach (GameObject go in _poolObjects)
        {
            if (!go.activeSelf)
            {
                return go;
            }
        }
        return null;
    }

    private void MoveToCementery(GameObject _object)
    {
        _object.SetActive(false);
        _object.transform.position = _cementery;
    }

    public void Clear()
    {
        foreach (var item in _poolObjects)
        {
            MoveToCementery(item);
        }
    }
}
