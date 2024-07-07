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

    public enum AppetiteState
    {
        High,
        Middle,
        Low,
    }

    public enum StomachState
    {
        High,
        Middle,
        Low,
    }

    public enum FrequencyState
    {
        High,
        Middle,
        Low,
    }

    public enum EndingType
    {
        Perfect,//�������
        Common,//��ͨ���
        EatingDisorder,//��ʳ�ϰ�
        EatintTooMuch,//������ʳ
        Anorexia,//��ʳ
        Death,//����֪ͨ��
        Starve,//����
        Boom,//�������
    }

    public class Player : MonoBehaviour
    {
        private static Player _player = null;
        public static Player Instance => _player;

        //[HideInInspector]
        //����ֵ
        public int health;
        //[HideInInspector]
        //ʳ��ֵ
        public int appetite;
        //[HideInInspector]
        //����ֵ
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
        AudioSource _audio;
        public bool isEnd = false;
        public HealthState healthState;
        public AppetiteState appetiteState;
        public StomachState stomachState;
        public FrequencyState frequencyState;


        private void Awake()
        {
            if (_player == null)
            {
                _player = this;
            }
            var _=GameMgr.Singleton;

            if (playerRightMainPanel == null) UIRoot.Singleton.GetOpenedPanel<PlayerRightMainPanel>(out playerRightMainPanel);
            _audio = GetComponent<AudioSource>();

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
            CheckMask();
            CheckHealth();
            //print($"{(float)health/maxHealth}  {(float)stomach/maxStomach} {(float)appetite/maxAppetite}");
            playerRightMainPanel.SetSliderHealthValue((float)health/maxHealth);
            playerRightMainPanel.SetSliderSatietyValue((float)stomach/maxStomach);
            playerRightMainPanel.SetSliderWantEatValue((float)appetite/maxAppetite);
        }

        private void CheckHealth()
        {
            if (Time.time - time >= 1)
            {
                if (Mathf.Abs(appetite - stomach) <= healthDiff_Up) health += 3;
                else if (Mathf.Abs(appetite - stomach) >= healthDiff_Down) health -= 3;
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
                _audio.Play();
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
            if (appetite > 80) appetiteState = AppetiteState.High;
            else if (appetite < 40) appetiteState = AppetiteState.Low;
            else appetiteState = AppetiteState.Middle;
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
            if (stomach > 70) stomachState = StomachState.High;
            else if (stomach < 40)
            {
                if(stomach<=10&&!_audio.isPlaying) _audio.Play();
                stomachState = StomachState.Low;
            }
            else stomachState = StomachState.Middle;
        }

        private void Die()
        {
            isEnd = true;
            healthState = HealthState.Death;
            SettlementPanel settlementPanel;
            UIRoot.Singleton.OpenPanel<SettlementPanel>();
            UIRoot.Singleton.GetOpenedPanel<SettlementPanel>(out settlementPanel);
            if (health<=0)
            {
                //����֪ͨ��
                settlementPanel.endingType = EndingType.Death;
            }
            else if(stomach<=0)
            {
                //����
                settlementPanel.endingType = EndingType.Starve;
            }
            else if(stomach>=maxStomach)
            {
                //�������
                settlementPanel.endingType = EndingType.Boom;
            }
            settlementPanel.ChangeText();
            TypeEventSystem.Global.UnListen<TimeEndEvent>(Live);
        }

        void CheckMask()
        {
            if(stomach>70||stomach<10||appetite>80||appetite<40)
            {
                playerRightMainPanel.mask.gameObject.SetActive(true);
                playerRightMainPanel.maskDark.color = new Color(0f, 0f, 0f, Mathf.Lerp(playerRightMainPanel.maskDark.color.a, 1f,Time.deltaTime));
            }
            else
            {
                playerRightMainPanel.mask.gameObject.SetActive(false);
                playerRightMainPanel.maskDark.color = new Color(0f, 0f, 0f,0f);
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
            if (isDie) return;
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
            SettlementPanel settlementPanel;
            if(health>60&&appetite>=50&&appetite<=70&&stomach>=70&&stomach<=80&&avg>=6&&avg<=12)
            {
                //�������
                
                UIRoot.Singleton.OpenPanel<SettlementPanel>();
                UIRoot.Singleton.GetOpenedPanel<SettlementPanel>(out settlementPanel);
                settlementPanel.endingType = EndingType.Perfect;
            }
            else if(freq_h>=2&&frep_l>=2)
            {
                //��ʳ�ϰ�
                
                UIRoot.Singleton.OpenPanel<SettlementPanel>();
                UIRoot.Singleton.GetOpenedPanel<SettlementPanel>(out settlementPanel);
                settlementPanel.endingType = EndingType.EatingDisorder;
            }
            else if(appetite>80)
            {
                //������ʳ
                
                UIRoot.Singleton.OpenPanel<SettlementPanel>();
                UIRoot.Singleton.GetOpenedPanel<SettlementPanel>(out settlementPanel);
                settlementPanel.endingType = EndingType.EatintTooMuch;
            }
            else if(appetite<40)
            {
                //��ʳ
                UIRoot.Singleton.OpenPanel<SettlementPanel>();
                UIRoot.Singleton.GetOpenedPanel<SettlementPanel>(out settlementPanel);
                settlementPanel.endingType = EndingType.Anorexia;
            }
            else
            {
                //��ͨ���
                UIRoot.Singleton.OpenPanel<SettlementPanel>();
                UIRoot.Singleton.GetOpenedPanel<SettlementPanel>(out settlementPanel);
                settlementPanel.endingType = EndingType.Common;
            }
            settlementPanel.ChangeText();
            TypeEventSystem.Global.UnListen<TimeEndEvent>(Live);
        }
    }
}
