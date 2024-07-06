using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ClickEat : MonoBehaviour
    {
        public int clickTimesDivision;
        public int clickTimes = 0;
        float time = 0;


        private void Awake()
        {

        }

        private void Start()
        {
            time = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time - time >= 1)
            {
                if (clickTimes <= 0) Player.Instance.ChangeHealth(-1);
                clickTimes = 0;
                time = Time.time;
            }
            if (Input.GetMouseButtonDown(0))
            {
                ++clickTimes;
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(pos, Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Food")
                    {
                        Food food = hit.collider.GetComponent<Food>();
                        Player.Instance.ChangeStomach(food.stomachIncrease);
                        Player.Instance.ChangeAppetite(-food.appetiteDecrease);
                        food.BeClicked();
                    }
                }
            }
            if (clickTimes > clickTimesDivision)
            {
                Player.Instance.ChangeAppetite(1);
                clickTimes = 0;
                time = Time.time;
            }
        }
    }
}
