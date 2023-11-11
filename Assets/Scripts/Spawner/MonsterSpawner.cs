using System.Collections;
using System.Collections.Generic;
using Monster;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    
    
    private float timer;
    // Update is called once per frame

    void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        transform.position = GameManager.Instance.Player.transform.position;
        
        
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            GameObject monster = MonsterPool.Instance.Get(Random.Range(0, 8));
            monster.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
            timer = 0f;
        }
    }
}
