using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Monster
{
    public enum Monster
    {
        Colobus = 0,
        Gecko = 1,
        Herring = 2,
        Muskrat = 3,
        Pudu = 4,
        Sparrow = 5,
        Squid = 6,
        Taipan = 7
    }
    public class MonsterPool : MonoBehaviour
    {
        public static MonsterPool Instance = null;
        
        
        public GameObject[] prefabs; // 인스펙터에서 초기화
        List<GameObject>[] pools;

        void Awake()
        {
            if (null == Instance)
            {
                Instance = this;
                pools = new List<GameObject>[prefabs.Length];

                for (int index = 0; index < pools.Length; index++)
                    pools[index] = new List<GameObject>();
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public GameObject Get(int index)
        {
            GameObject select = null;

            foreach (GameObject item in pools[index])
            {
                if (!item.activeSelf)
                {
                    select = item;
                    select.SetActive(true);
                    break;
                }
            }

            if (!select)
            {
                select = Instantiate(prefabs[index], transform);
                pools[index].Add(select);
            }

            return select;
        }

        public void Clear(int index)
        {
            foreach (GameObject item in pools[index])
                item.SetActive(false);
        }

        public void ClearAll()
        {
            for (int index = 0; index < pools.Length; index++)
                foreach (GameObject item in pools[index])
                    item.SetActive(false);
        }
    }
}