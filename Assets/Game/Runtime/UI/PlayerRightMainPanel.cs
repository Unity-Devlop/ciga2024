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

        public List<GameObject> listFoodItem = new List<GameObject>();
        
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
                obj.GetComponent<FoodItem>().Init(20);
                listFoodItem.Insert(i,obj);
            }
            
        }
        
        private void Awake()
        {
            Init();
        }

        
        public void OnEat(FoodType foodType)
        {
            for (int i = 0; i < listFoodItem.Count; ++i)
            {
                if (listFoodItem[i].GetComponent<FoodItem>().foodType == foodType)
                {
                    listFoodItem[i].GetComponent<FoodItem>().myNum--;
                    listFoodItem[i].GetComponent<FoodItem>().textNum.text =
                        listFoodItem[i].GetComponent<FoodItem>().myNum.ToString();
                }
            }
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