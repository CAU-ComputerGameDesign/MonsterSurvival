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

    public float nextExpRatio = 1.1f;

    public float gameTime = 0f;
    public float maxTime = 20 * 60f;

    public List<GameObject> expPool = new List<GameObject>();
    public GameObject expPrefab;

    public ChooseAbility abilitySetter;
    public UISetter uiSetter;
    
    public bool isGameStarted = false;

    private void Start()
    {
        // 시작 능력을 고름
        abilitySetter.Init(OnLevelUp);
        abilitySetter.ReadyToChoose();
    }

    private void Update()
    {
        if (playerExp >= nextExp)
        {
            LevelUp();
        }

        if (isGameStarted)
        {
            if (gameTime < maxTime)
            {
                gameTime += Time.deltaTime;
            }
            else
                gameTime = maxTime;
        }
    }

    public void LevelUp()
    {
        abilitySetter.gameObject.SetActive(true);
        abilitySetter.ReadyToChoose();
        level++;
        playerExp = 0;
        nextExp = nextExp * nextExpRatio;
    }
    private void OnLevelUp(Weapon weapon)
    {
        if (!isGameStarted)
            GameStart();

        abilitySetter.gameObject.SetActive(false);

        if (weapon != null)
        {
            uiSetter.SetAbilityUI(weapon);
        }
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

    public void GameStart()
    {
        isGameStarted = true;
    }
}
