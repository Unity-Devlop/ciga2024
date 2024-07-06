using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityToolkit;
using DG.Tweening;

namespace Game
{
    public class PlayerRightMainPanel : UIPanel
    {
        // [SerializeField] private Button enterGameButton;
        [SerializeField] private Slider sliderHealth;
        [SerializeField] private Slider sliderWantEat; 
        [SerializeField] private Slider sliderSatiety;
        [SerializeField] private TextMeshProUGUI textMeshProTime ;
        [SerializeField] private Sprite spriteInMyHand;
        [SerializeField] private Sprite defaultspriteInMyHand;

        [SerializeField] private Image imageMyHand;
        [SerializeField] private GameObject imageFoodInHand;

        public float time = 30.0f;
        
        public List<FoodItem> listFoodItem = new List<FoodItem>();
        
        public void Init()
        {
            textMeshProTime.text = time.ToString("f2");
            //每一关的初始值
            SetSliderSatietyValue(0.5f);
            SetSliderHealthValue(0.5f);
            SetSliderWantEatValue(0.5f);



        }

        public void Reset()
        {
            for (int i = 0; i < listFoodItem.Count; ++i)
            {
                listFoodItem[i].myNum = 10000;
            }
        }
        
        private void Awake()
        {
            Init();
            
        }
        
        public void OnEat(FoodType foodType)
        {
            int a = 0;
            //每次进来我先赋值
            
            imageFoodInHand.GetComponent<Image>().sprite = defaultspriteInMyHand;
            for (int i = 0; i < listFoodItem.Count; ++i)
            {
                if (listFoodItem[i].foodType == foodType)
                {
                    listFoodItem[i].myNum--;
                    print($"我吃了一个类型{listFoodItem[i].foodType}的食物");

                    imageMyHand = listFoodItem[i].foodSprite;
                    
                    //画出对应类型的动画
                    Sequence mysSequence = DOTween.Sequence();
                    mysSequence.Append(imageMyHand.transform.DORotate(new Vector3(0, 0, 90), 1f).SetEase(Ease.Linear)
                        .SetLoops(2, LoopType.Yoyo).OnStepComplete(() =>
                        {
                            a++;
                            if (a == 1)
                            {
                                // imageFoodInHand = null;
                                imageFoodInHand.GetComponent<Image>().sprite = null;
                            }
                    
                        }));
                    mysSequence.Play();
                    
                    break;
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