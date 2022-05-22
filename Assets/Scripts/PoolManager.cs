using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //todas las pools de mi escena
    [SerializeField] private PoolData[] poolsData;

    //variables de uso
    private Pool[] pools;
    #region Singelton
    static private PoolManager _instance;
    static public PoolManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        pools = new Pool[poolsData.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new GameObject().AddComponent<Pool>();
            pools[i].gameObject.name = poolsData[i].name;
            pools[i].Initialize(poolsData[i].name, poolsData[i].poolObject, poolsData[i].poolCount, poolsData[i].cementery, transform);
        }
    }
    #endregion 

    /// <summary>
    /// Devuelve el objeto de la pool si lo hay, si no devuelve null.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetPoolObject(string name)
    {
        foreach (Pool pool in pools)
        {
            if (pool.name == name)
            {
                return pool.GetPoolObject();
            }
        }
        return null;
    }

    public void Clear()
    {
        foreach (var item in pools){
            item.Clear();
        }
    }
}
[System.Serializable]
public class PoolData
{
    [SerializeField] private string _name;
    public string name { get { return _name; } }

    [SerializeField] private GameObject _poolObject;
    public GameObject poolObject { get { return _poolObject; } }

    [SerializeField] private int _poolCount;
    public int poolCount { get { return _poolCount; } }

    [SerializeField] private Vector3 _cementery;
    public Vector3 cementery { get { return _cementery; } }

}