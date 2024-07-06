using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public enum HealthState
    {
        Health,
        SubHealth,
        Dying,
        Death,
    }

    public enum EndingType
    {
        Perfect,//完美结局
        Common,//普通结局
        EatingDisorder,//进食障碍
        EatintTooMuch,//暴饮暴食
        Anorexia,//厌食
        Death,//死亡通知书
        Starve,//饿死
        Boom,//爆体而亡
    }

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

        public float healthDiff_Up;
        public float healthDiff_Down;

        public PlayerRightMainPanel playerRightMainPanel;
        public List<int> EatFrequencyList = new List<int>();

        float time = 0;
        float appetiteTime = 0;
        float stomachTime = 0;
        bool isDie = false;
        bool isEnd = false;
        HealthState healthState;

        private void Awake()
        {
            if (_player == null)
            {
                _player = this;
            }
            var _=GameMgr.Singleton;

            if (playerRightMainPanel == null) UIRoot.Singleton.GetOpenedPanel<PlayerRightMainPanel>(out playerRightMainPanel);

            TypeEventSystem.Global.Listen<TimeEndEvent>(Live);
        }


        private void Update()
        {
            if (isEnd)
            {
                return;
            }
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
                if (Mathf.Abs(appetite - stomach) <= healthDiff_Up) health += 1;
                else if (Mathf.Abs(appetite - stomach) >= healthDiff_Down) health -= 2;
                time = Time.time;
            }
            CheckHealthState();
        }

        private void CheckHealthState()
        {
            if (health >= 60)
            {
                healthState = HealthState.Health;
            }
            else if (health > 20 && health < 60)
            {
                healthState = HealthState.SubHealth;
            }
            else if (health >= 1 && health <= 20)
            {
                healthState = HealthState.Dying;
            }
            else
            {
                healthState = HealthState.Death;
                Die();
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
            if(stomach<=0||stomach>=maxStomach)
            {
                Die();
            }
        }

        private void Die()
        {
            isEnd = true;
            healthState = HealthState.Death;
            if(health<=0)
            {
                //死亡通知书
            }
            else if(stomach<=0)
            {
                //饿死
            }
            else if(stomach>=maxStomach)
            {
                //爆体而亡
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

        public void Live(TimeEndEvent e)
        {
            isEnd = true;
            float avg = 0;
            if(EatFrequencyList.Count>0) avg = EatFrequencyList.Sum() / EatFrequencyList.Count;
            int freq_h = 0;
            int frep_l = 0;
            for(int i=0;i<EatFrequencyList.Count;++i)
            {
                if (EatFrequencyList[i] > 12) ++freq_h;
                else if (EatFrequencyList[i] < 6) ++frep_l;
            }
            if(health>60&&appetite>=50&&appetite<=70&&stomach>=70&&stomach<=80&&avg>=6&&avg<=12)
            {
                //完美结局
            }
            else if(freq_h>=2&&frep_l>=2)
            {
                //进食障碍
            }
            else if(appetite>80)
            {
                //暴饮暴食
            }
            else if(appetite<40)
            {
                //厌食
            }
            else
            {
                //普通结局
            }
        }
    }
}
