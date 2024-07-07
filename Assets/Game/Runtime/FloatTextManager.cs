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

        private Vector2 mPoint; //GUI����
        public float DestoryTime = 0.5f; //�˺�������ʧʱ��
                                         // Start is called before the first frame update
        void Start()
        {
            mTarget = transform.position; //��ȡĿ��λ��
            mScreen = Camera.main.WorldToScreenPoint(mTarget); //ת��Ϊ��Ļλ��
            mPoint = new Vector2(mScreen.x, Screen.height - mScreen.y);
            StartCoroutine("Free"); //����һ��Э��
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.up *2 * Time.deltaTime); //�˺���������Ч��
            mTarget = transform.position;
            mScreen = Camera.main.WorldToScreenPoint(mTarget);
            mPoint = new Vector2(mScreen.x, Screen.height - mScreen.y);//ʵʱ�仯λ��
        }

        void OnGUI()
        {
            GUIStyle labelFont = new GUIStyle();
            labelFont.normal.textColor = new Color(1, 1, 1);  
            labelFont.fontSize = 40;
            labelFont.fontStyle = FontStyle.Bold;
            labelFont.font = font;
            GUI.Label(new Rect(mPoint.x, mPoint.y, ContentWidth, ContentHeight),$"����+{valueStomach}\r\nʳ��-{valueAppetite}",labelFont);
        }
        IEnumerator Free()  //Э�̣��˺�����ʱ��һ����ʧ
        {
            yield return new WaitForSeconds(DestoryTime);
            GameObjectPoolManager.Release("FloatText",gameObject);
        }
    }
}
