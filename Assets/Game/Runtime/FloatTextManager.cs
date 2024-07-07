using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class FloatTextManager : MonoBehaviour
    {
        private Vector3 mTarget;
        private Vector3 mScreen;
        public float valueAppetite;
        public float valueStomach;
        public Font font;

        public float ContentWidth = 100;
        public float ContentHeight = 50;

        private Vector2 mPoint; //GUI坐标
        public float DestoryTime = 0.5f; //伤害数字消失时间
                                         // Start is called before the first frame update
        void Start()
        {
            mTarget = transform.position; //获取目标位置
            mScreen = Camera.main.WorldToScreenPoint(mTarget); //转化为屏幕位置
            mPoint = new Vector2(mScreen.x, Screen.height - mScreen.y);
            StartCoroutine("Free"); //开启一个协程
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.up *2 * Time.deltaTime); //伤害数字上移效果
            mTarget = transform.position;
            mScreen = Camera.main.WorldToScreenPoint(mTarget);
            mPoint = new Vector2(mScreen.x, Screen.height - mScreen.y);//实时变化位置
        }

        void OnGUI()
        {
            GUIStyle labelFont = new GUIStyle();
            labelFont.normal.textColor = new Color(1, 1, 1);  
            labelFont.fontSize = 40;
            labelFont.fontStyle = FontStyle.Bold;
            labelFont.font = font;
            GUI.Label(new Rect(mPoint.x, mPoint.y, ContentWidth, ContentHeight),$"饱腹+{valueStomach}\r\n食欲-{valueAppetite}",labelFont);
        }
        IEnumerator Free()  //协程，伤害数字时间一到消失
        {
            yield return new WaitForSeconds(DestoryTime);
            GameObjectPoolManager.Release("FloatText",gameObject);
        }
    }
}
