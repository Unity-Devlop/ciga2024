using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public enum EatFrequency
    {
        High,
        Middle,
        Low
    }

    public class ClickEat : MonoBehaviour
    {
        public PlayerRightMainPanel playerRightMainPanel;
        public int clickTimesDivision_H;
        public int clickTimesDivision_L;
        public int clickTimes = 0;
        float time = 0;
        public EatFrequency eatFrequency;


        private void Awake()
        {

        }

        private void Start()
        {
            var _ = GameMgr.Singleton;

            if (playerRightMainPanel == null) UIRoot.Singleton.GetOpenedPanel<PlayerRightMainPanel>(out playerRightMainPanel);
            time = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time - time >= 4)
            {
                Player.Instance.EatFrequencyList.Add(clickTimes);
                if (clickTimes > clickTimesDivision_H)
                {
                    //Player.Instance.ChangeAppetite(1);
                    eatFrequency = EatFrequency.High;
                }
                else if (clickTimes < clickTimesDivision_L)
                {
                    eatFrequency = EatFrequency.Low;
                }
                else
                {
                    eatFrequency = EatFrequency.Middle;
                }

                clickTimes = 0;
                time = Time.time;
            }
            if (Input.GetMouseButtonDown(0))
            {
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(pos, Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Food")
                    {
                        ++clickTimes;
                        if (playerRightMainPanel==null) UIRoot.Singleton.GetOpenedPanel<PlayerRightMainPanel>(out playerRightMainPanel);
                        Food food = hit.collider.GetComponent<Food>();
                        Player.Instance.ChangeStomach(food.stomachIncrease);
                        Player.Instance.ChangeAppetite(-food.appetiteDecrease);
                        food.BeClicked(playerRightMainPanel);
                    }
                }
            }
        }
    }
}
