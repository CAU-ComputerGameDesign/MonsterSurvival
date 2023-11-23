using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance = null;
    public GameObject[] prefabs; // 인스펙터에서 초기화
    public List<GameObject>[] particlePools;

    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            particlePools = new List<GameObject>[prefabs.Length];

            for (int index = 0; index < particlePools.Length; index++)
                particlePools[index] = new List<GameObject>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject GetParticle(int index, Vector3 position)
    {
        GameObject particle;
        if (particlePools[index].Count > 0)
        {
            particle = particlePools[index][particlePools[index].Count - 1];
            particlePools[index].RemoveAt(particlePools[index].Count - 1);
            
            particle.SetActive(true);
            particle.transform.position = position;
        }
        else
        {
            particle = Instantiate(prefabs[index], position, Quaternion.identity);
            particle.SetActive(true);
        }
        return particle;
    }

    public void RecycleParticle(int index, GameObject go)
    {
        particlePools[index].Add(go);
        go.SetActive(false);
    }
}
