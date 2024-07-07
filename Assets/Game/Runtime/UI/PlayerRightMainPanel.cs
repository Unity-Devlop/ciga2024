using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityToolkit;
using DG.Tweening;
using UnityEditor.Animations;

namespace Game
{
    public class PlayerRightMainPanel : UIPanel
    {
        // [SerializeField] private Button enterGameButton;
        [SerializeField] private Slider sliderHealth;
        [SerializeField] private Slider sliderWantEat; 
        [SerializeField] private Slider sliderSatiety;
        [SerializeField] private Text textMeshProTime ;
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

        public float time = 60.0f;
        public Image tixin;
        public Image yanjing;
        public Image zui;
        
        // public List<FoodItem> listFoodItem = new List<FoodItem>();

        public List<Sprite> list1Sprites = new List<Sprite>();
        public List<Sprite> list2Sprites = new List<Sprite>();
        private Animator _animatorshenti;
        private Animator _animatorface;
        private Animator _animatorzui;
        
        public void Init()
        {

            textMeshProTime.text = $"倒计时：{time}";

            _animatorshenti = tixin.GetComponent<Animator>();
            _animatorface = yanjing.GetComponent<Animator>();
            _animatorzui = zui.GetComponent<Animator>();
            
            //每一关的初始值
            SetSliderSatietyValue(0.5f);
            SetSliderHealthValue(0.5f);
            SetSliderWantEatValue(0.5f);
        }

        public void SetFoodNum(string op)
        {
            textNum.text = op;
        }

        public void Update()
        {
            if (Player.Instance.appetiteState == AppetiteState.High)
            {
                yanjing.sprite = list2Sprites[2];
                _animatorface.Play("tanlan");
            }
            else if (Player.Instance.appetiteState == AppetiteState.Middle)
            {
                
                yanjing.sprite = list2Sprites[1];
                _animatorface.Play("pindan");
            }
            else if(Player.Instance.appetiteState == AppetiteState.Low)
            {
                
                yanjing.sprite = list2Sprites[0];
                _animatorface.Play("jushang");
            }
            
            
            if (Player.Instance.stomachState == StomachState.High)
            {
                
                tixin.sprite = list1Sprites[2];
                _animatorshenti.Play("Pang");
                
            }
            else if (Player.Instance.stomachState == StomachState.Middle)
            {
                
                tixin.sprite = list1Sprites[1];
                _animatorshenti.Play("zhong");
            }
            else if(Player.Instance.stomachState == StomachState.Low)
            {
                
                tixin.sprite = list1Sprites[0];
                _animatorshenti.Play("shou");
            }
            
            
            if (Player.Instance.frequencyState == FrequencyState.High)
            {
                _animatorzui.speed = 1.5f;
            }
            else if (Player.Instance.frequencyState == FrequencyState.Middle)
            {
                _animatorzui.speed = 1.0f;
            }
            else if(Player.Instance.frequencyState == FrequencyState.Low)
            {
                _animatorzui.speed = 0.8f;
            }
        }


        public void Reset(string op)
        {
            textMeshProTime.text = "倒计时："+op;
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
                        mysSequence.Append(imageMyHand.transform.DORotate(new Vector3(0, 0, tmp + 180), 1f).SetEase(Ease.Linear)
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
            textMeshProTime.text = $"倒计时：{num}";
        }

        private void OnEnterGameButtonClick()
        {
            Global.Systems.Get<GameFlow>().EnterGame();
        }
    }
}