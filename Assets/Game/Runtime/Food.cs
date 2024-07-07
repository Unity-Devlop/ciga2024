using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public enum FoodType
    {
        Hamburger,
        Watermelon,
        Pizza,
        PopCorn,
        Icecream,
        Chip,
        FriedChick,
        Cola,
        Oden,
        Bread,
        Cake,
        Drink,
        Lunch,
        RiceBall,
        Apple,
    }

    public class Food : MonoBehaviour
    {
        public int stomachIncrease;
        public int appetiteDecrease;
        public FoodType type;
        public float disappearTime;

        float time = 0;

        private void OnEnable()
        {
            time = Time.time;
        }

        private void Update()
        {
            if (Time.time - time >= disappearTime||Player.Instance.isEnd)
            {
                Disappear(null);
            }
        }

        public void BeClicked(PlayerRightMainPanel playerRightMainPanel)
        {
            GameObject go = GameObjectPoolManager.Get("FloatText");
            go.transform.position = transform.position;
            go.GetComponent<FloatTextManager>().valueAppetite = appetiteDecrease;
            go.GetComponent<FloatTextManager>().valueStomach = stomachIncrease;
            Disappear(playerRightMainPanel);
        }

        private void Disappear(PlayerRightMainPanel playerRightMainPanel)
        {
            if (playerRightMainPanel != null) playerRightMainPanel.OnEat(type);
            GameObjectPoolManager.Release(type, gameObject);
        }
    }
}
