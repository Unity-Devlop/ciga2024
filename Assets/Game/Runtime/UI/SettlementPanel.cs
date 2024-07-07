using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using UnityToolkit;
using DG.Tweening;

namespace Game
{
    public class SettlementPanel : UIPanel
    {

        [SerializeField]public EndingType endingType;
        
        
        [SerializeField]private Image image;
        [SerializeField]private Button btnExit;
        [SerializeField]private Button btnNext;
        [SerializeField]private Button btnReStart;

        public List<Sprite> listSprites = new List<Sprite>();
        // Start is called before the first frame update

        public override void OnLoaded()
        {
            base.OnLoaded();
            switch (endingType)
            {
                case EndingType.Perfect :
                    image.sprite = listSprites[0];
                    break;
                case EndingType.Common :
                    image.sprite = listSprites[1];
                    break;
                case EndingType.EatingDisorder:
                    image.sprite = listSprites[2];
                    break;
                case EndingType.EatintTooMuch:
                    image.sprite = listSprites[3];
                    break;
                case EndingType.Anorexia :
                    image.sprite = listSprites[4];
                    break;
                case EndingType.Death :
                    image.sprite = listSprites[5];
                    break;
                case EndingType.Starve :
                    image.sprite = listSprites[6];
                    break;
                case EndingType.Boom :
                    image.sprite = listSprites[7];
                    break;
                default:
                    break;
            }
        }

        void Start()
        {
            //退出游戏
            btnExit.onClick.AddListener((() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }));
            
            
            
            btnNext.onClick.AddListener((() =>
            {
                //下一关
                print($"下一关");
                Global.Singleton.LoadNextLevel();
                
                UIRoot.Singleton.ClosePanel<SettlementPanel>();
                
            }));
            
            btnReStart.onClick.AddListener((() =>
            {
                //重新开始这一关
                print($"重新开始这一关");
                Global.Singleton.ReSetLevel();

                UIRoot.Singleton.ClosePanel<SettlementPanel>();
            }));

            image.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            image.transform.DOScale(new Vector3(1, 1, 1), 0.2f);

           
        }

        private void OnDestroy()
        {
            btnExit.onClick.RemoveAllListeners();
            btnNext.onClick.RemoveAllListeners();
            btnReStart.onClick.RemoveAllListeners();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

}
