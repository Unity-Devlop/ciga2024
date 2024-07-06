using Codice.Client.GameUI.Update;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class FoodCreatManager : MonoBehaviour
    {
        public List<GameObject> prefabs = new List<GameObject>();
        public float width;
        public float height;
        public PlayerRightMainPanel playerRightMainPanel;
        float time;

        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            var _ = GameMgr.Singleton;
            if (playerRightMainPanel == null) UIRoot.Singleton.GetOpenedPanel<PlayerRightMainPanel>(out playerRightMainPanel);
            CalculateWidthAndHeight();
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

        void CalculateWidthAndHeight()
        {
            float w = -(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - Camera.main.ScreenToWorldPoint(new Vector3(1920f, 0, 0)).x);
            float h= -(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - Camera.main.ScreenToWorldPoint(new Vector3(0, 1080f, 0)).y);
            width = 3*w/4;
            height= h;
            transform.position = new Vector3(width/2-w/2,0, 0);
        }
    }
}
