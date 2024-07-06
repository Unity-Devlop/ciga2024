using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class FoodCreatManager : MonoBehaviour
    {
        public List<GameObject> prefabs = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            //¶ÔÏó³Ø×¢²á
            for (int i = 0; i < prefabs.Count; ++i)
            {
                GameObjectPoolManager.Create(prefabs[i].GetComponent<Food>().type, prefabs[i]);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
