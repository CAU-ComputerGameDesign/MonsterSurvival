using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    
    [Header("Player Settings")]
    public GameObject Player;

    public float expRange;
    [Header("Game")] 
    public int level = 1;
    
    public float playerExp = 0f;
    public float nextExp = 3f;

    public float gameTime = 0f;
    public float maxTime = 20 * 60f;

    public List<GameObject> expPool = new List<GameObject>();
    public GameObject expPrefab;

    private void Update()
    {
        if (playerExp >= nextExp)
        {
            levelUP();
        }
        
        if (gameTime < maxTime)
        {
            gameTime += Time.deltaTime;
        }
        else
            gameTime = maxTime;
    }

    public void levelUP()
    {
        level++;
        playerExp = 0;
        nextExp = nextExp * 1.7f;
    }

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
            go.SetActive(true);
            TrailRenderer tr = go.GetComponent<TrailRenderer>();
            tr.enabled = false;
            go.transform.position = position;
            tr.enabled = true;
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
