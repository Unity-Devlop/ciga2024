using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace Game
{

    public class Player : MonoBehaviour
    {
        private static Player _player = null;
        public static Player Instance => _player;

        //[HideInInspector]
        //健康值
        public int health;
        //[HideInInspector]
        //食欲值
        public int appetite;
        //[HideInInspector]
        //饱腹值
        public int stomach;

        public int maxHealth;
        public int maxAppetite;
        public int maxStomach;

        public int appetiteIncreaseValue;
        public int stomachLossValue;

        public float difference;

        public PlayerRightMainPanel playerRightMainPanel;

        float time = 0;
        float appetiteTime = 0;
        float stomachTime = 0;

        private void Awake()
        {
            if (_player == null)
            {
                _player = this;
            }
            var _=GameMgr.Singleton;

            if (playerRightMainPanel == null) UIRoot.Singleton.GetOpenedPanel<PlayerRightMainPanel>(out playerRightMainPanel);
        }

        private void FixedUpdate()
        {
            AppetiteIncrease();
            StomachLoss();
            CheckHealth();
            playerRightMainPanel.SetSliderHealthValue((float)health/maxHealth);
            playerRightMainPanel.SetSliderSatietyValue((float)stomach/maxStomach);
            playerRightMainPanel.SetSliderWantEatValue((float)appetite/maxAppetite);
        }

        private void CheckHealth()
        {
            if (Time.time - time >= 1)
            {
                if (Mathf.Abs(appetite - stomach) <= difference) health += 1;
                else health -= 1;
                time = Time.time;
            }
        }

        private void AppetiteIncrease()
        {
            if ((Time.time - appetiteTime) >= 1)
            {
                appetiteTime = Time.time;
                appetite += appetiteIncreaseValue;
            }
        }

        private void StomachLoss()
        {
            if ((Time.time - stomachTime) >= 1)
            {
                stomachTime = Time.time;
                stomach -= stomachLossValue;
                if (stomach < 0) stomach = 0;
            }
        }

        public void ChangeStomach(int value)
        {
            stomach += value;
            if (stomach < 0) stomach = 0;
            if (stomach > maxStomach) stomach = maxStomach;
        }

        public void ChangeAppetite(int value)
        {
            appetite += value;
            if (appetite < 0) appetite = 0;
            if (appetite > maxAppetite) appetite = maxAppetite;
        }

        public void ChangeHealth(int value)
        {
            health += value;
            if (health < 0) health = 0;
            if (health > maxHealth) health = maxHealth;
        }
    }
}
