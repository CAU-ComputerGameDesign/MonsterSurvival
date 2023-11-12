using System.Collections;
using System.Collections.Generic;
using Monster;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    
    
    private float timer;
    // Update is called once per frame

    void Update()
    {
        transform.position = GameManager.Instance.Player.transform.position;
        
        
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            Vector3 point = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position;
            RaycastHit hit;
            if (Physics.Raycast(point, -Vector3.up, out hit))
                point = new Vector3(point.x, hit.point.y, point.z);
            GameObject monster = MonsterPool.Instance.Get(Random.Range(0, 8), point);
            timer = 0f;
        }
    }
}
