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
        public float offset_x;
        public float offset_y;
        float width;
        float height;
        float time;
        float frequency;

        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            CalculateWidthAndHeight();
            //�����ע��
            for (int i = 0; i < prefabs.Count; ++i)
            {
                GameObjectPoolManager.Create(prefabs[i].GetComponent<Food>().type, prefabs[i]);
            }
            time = Time.time;
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Player.Instance.isEnd) return;
            if(Player.Instance.appetite<40)
            {
                frequency = 1f / 5f;
            }
            else if(Player.Instance.appetite>80)
            {
                frequency = 1f / 1f;
            }
            else
            {
                frequency = 1f / 3f;
            }

            if(Time.time-time>=frequency)
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
            go.GetComponent<FoodItemAnimation>().i = 0;
            
            
            Vector3 v = Random.insideUnitCircle;
            v.x *= width / 2*offset_x;
            v.y *= height / 2*offset_y;
            v.z = 0;
            go.transform.position = transform.position + v;
        }

        void CalculateWidthAndHeight()
        {
            float w = -(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - Camera.main.ScreenToWorldPoint(new Vector3(1920f, 0, 0)).x);
            float h= -(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - Camera.main.ScreenToWorldPoint(new Vector3(0, 1080f, 0)).y);
            width = 2*w/3;
            height= h;
            transform.position = new Vector3(width/2-w/2,0, 0);
        }
    }
}
