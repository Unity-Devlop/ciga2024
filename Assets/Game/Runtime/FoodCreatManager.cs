using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class FoodCreatManager : MonoBehaviour
    {
        public List<GameObject> prefabs = new List<GameObject>();
        public float baseValue;
        public float width;
        public float height;
        Vector3[] corners;
        float time;

        // Start is called before the first frame update
        void Start()
        {
            //¶ÔÏó³Ø×¢²á
            for (int i = 0; i < prefabs.Count; ++i)
            {
                GameObjectPoolManager.Create(prefabs[i].GetComponent<Food>().type, prefabs[i]);
            }
            time = Time.time;
            
        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time-time>=1)
            {
                Create();
                time = Time.time;
            }
        }

        void Create()
        {
            if (prefabs.Count <= 0) return;
            int r = Random.Range(0, prefabs.Count);
            GameObject go=GameObjectPoolManager.Get(prefabs[r].GetComponent<Food>().type);
            Vector3 v = Random.insideUnitCircle;
            v.x *= width / 2;
            v.y *= height / 2;
            v.z = 0;
            go.transform.position = transform.position + v;
        }
    }
}
