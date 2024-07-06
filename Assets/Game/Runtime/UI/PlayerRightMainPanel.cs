using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityToolkit;

namespace Game
{
    public class PlayerRightMainPanel : UIPanel
    {
        // [SerializeField] private Button enterGameButton;
        [SerializeField] private Slider sliderHealth;
        [SerializeField] private Slider sliderWantEat; 
        [SerializeField] private Slider sliderSatiety;
        [SerializeField] private TextMeshProUGUI textMeshProTime ;
        public float time = 30.0f;

        public GameObject foodItem;
        public ScrollRect scrollRectFood;
        public GameObject objparent;
        
        public void Init()
        {
            
            textMeshProTime.text = time.ToString("f2");
            SetSliderSatietyValue(0.5f);
            SetSliderHealthValue(0.5f);
            SetSliderWantEatValue(0.5f);

            // Transform container = transform.Find("Content");
            for (int i = 0; i < 20; ++i)
            {
                var obj = Instantiate(foodItem, objparent.transform);
            }
            
            //监听这个吃的事件
            Global.Event.Listen<OnEat>(StartEat);
        }

        struct OnEat : IEvent
        {
            private string _a ;
        }
        
        private void Awake()
        {
            Init();
            // SetSliderSatietyValue(0.5f);
            // Global.Event.Send<OnEat>();
            // enterGameButton.onClick.AddListener(OnEnterGameButtonClick);
        }

        private void StartEat(OnEat a)
        {
            print("我吃了");
        }

        public void SetSliderSatietyValue(float num)
        {
            sliderSatiety.value = num;
        }

        public void SetSliderHealthValue(float num)
        {
            sliderHealth.value = num;
        }
        
        public void SetSliderWantEatValue(float num)
        {
            sliderWantEat.value = num;
        }

        private void OnEnterGameButtonClick()
        {
            Global.Systems.Get<GameFlow>().EnterGame();
        }
    }
}