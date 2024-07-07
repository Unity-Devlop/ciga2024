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

        public List<GameObject> listobj = new List<GameObject>();

        public Sequence mysSequence;
        public int num = 0;
        public Text textNum;
        public Image mask;
        public Image maskDark;

        public float time = 30.0f;
        
        // public List<FoodItem> listFoodItem = new List<FoodItem>();
        
        public void Init()
        {
            textMeshProTime.text = time.ToString();
            //每一关的初始值
            SetSliderSatietyValue(0.5f);
            SetSliderHealthValue(0.5f);
            SetSliderWantEatValue(0.5f);
        }

        public void SetFoodNum(string op)
        {

            textNum.text = op;
        }

        public void Reset(string op)
        {
            textMeshProTime.text = op;
            textNum.text = "0";
            mask.gameObject.SetActive(false);
        }
        
        private void Awake()
        {
            Init();
        }

        public void OnEat(FoodType foodType)
        {
            for (int i = 0; i < listobj.Count; ++i)
            {
                if (listobj[i].GetComponent<Food>().type == foodType)
                {
                    print(foodType);
                    //画出对应类型的动画
                    int a = 0;
                    // mysSequence.Kill();
                    if (mysSequence == null)
                    {
                        imageFoodInHand.GetComponent<Image>().sprite = listobj[i].GetComponent<SpriteRenderer>().sprite;
                        mysSequence = DOTween.Sequence();
                        var tmp = imageMyHand.transform.localScale.z;
                        mysSequence.Append(imageMyHand.transform.DORotate(new Vector3(0, 0, tmp + 90), 1f).SetEase(Ease.Linear)
                            .SetLoops(2, LoopType.Yoyo).OnStepComplete(() =>
                            {
                                a++;
                                if (a == 1)
                                {
                                    // imageFoodInHand = null;
                                    imageFoodInHand.GetComponent<Image>().sprite = null;
                                    imageFoodInHand.SetActive(false);
                                }

                            }).OnComplete(() =>
                            {
                                mysSequence.Kill(); 
                                mysSequence = null;
                                
                                imageFoodInHand.SetActive(true);
                            }));
                        mysSequence.Play();
                    }
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

        public void SetTextMeshProTime(float num)
        {
            textMeshProTime.text = num.ToString();
        }

        private void OnEnterGameButtonClick()
        {
            Global.Systems.Get<GameFlow>().EnterGame();
        }
    }
}