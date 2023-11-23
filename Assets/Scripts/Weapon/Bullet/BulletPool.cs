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

    public GameObject GetBullet(int index, Vector3 position, Quaternion rotation, Vector3 target)
    {
        GameObject bullet;
        if (bulletPools[index].Count > 0)
        {
            bullet = bulletPools[index][bulletPools[index].Count - 1];
            bulletPools[index].RemoveAt(bulletPools[index].Count - 1);
            
            bullet.SetActive(true);
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
        }
        else
        {
            bullet = Instantiate(prefabs[index], position, rotation);
            bullet.SetActive(true);
        }
        bullet.GetComponent<IBullet>().SetTarget(target);
        return bullet;
    }
    
    
    public void RecycleBullet(int index, GameObject go)
    {
        bulletPools[index].Add(go);
        go.SetActive(false);
    }
}
