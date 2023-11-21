using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    
    public GameObject Player;

    public float expRange;

    public List<GameObject> expPool = new List<GameObject>();
    public GameObject expPrefab;
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject GetExpFromPool(Vector3 position)
    {
        GameObject go;
        if (expPool.Count > 0)
        {
            go = expPool[expPool.Count - 1];
            expPool.RemoveAt(expPool.Count - 1);
            go.transform.position = position;
        }
        else
        {
            go = Instantiate(expPrefab, position, Quaternion.identity);
        }

        return go;
    }

    public void RecycleEXPObject(GameObject go)
    {
        expPool.Add(go);
        go.SetActive(false);
    }
}
