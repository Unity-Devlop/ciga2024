using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public enum FoodType
    {
        Hamburger,
    }

    public class Food : MonoBehaviour
    {
        public int stomachIncrease;
        public int appetiteDecrease;
        public FoodType type;

        float time = 0;

        private void OnEnable()
        {
            time = Time.time;
        }

        private void Update()
        {
            if (Time.time - time >= 2)
            {
                Disappear(null);
            }
        }

        public void BeClicked(PlayerRightMainPanel playerRightMainPanel)
        {
            Disappear(playerRightMainPanel);
        }

        private void Disappear(PlayerRightMainPanel playerRightMainPanel)
        {
            if (playerRightMainPanel != null) playerRightMainPanel.OnEat(type);
            GameObjectPoolManager.Release(type, gameObject);
        }
    }
}
