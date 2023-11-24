using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance = null;
    public GameObject[] prefabs; // 인스펙터에서 초기화
    public List<GameObject>[] bulletPools;

    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            bulletPools = new List<GameObject>[prefabs.Length];

            for (int index = 0; index < bulletPools.Length; index++)
                bulletPools[index] = new List<GameObject>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject GetBullet(int index, Vector3 position, Quaternion rotation, Vector3 target, float damage)
    {
        GameObject go;
        if (bulletPools[index].Count > 0)
        {
            go = bulletPools[index][bulletPools[index].Count - 1];
            bulletPools[index].RemoveAt(bulletPools[index].Count - 1);
            
            go.SetActive(true);
            go.transform.position = position;
            go.transform.rotation = rotation;
        }
        else
        {
            go = Instantiate(prefabs[index], position, rotation);
            go.SetActive(true);
        }

        IBullet bullet = go.GetComponent<IBullet>();
        bullet.SetTarget(target);
        bullet.SetDamage(damage);
        
        
        return go;
    }
    
    
    public void RecycleBullet(int index, GameObject go)
    {
        bulletPools[index].Add(go);
        go.SetActive(false);
    }
}
