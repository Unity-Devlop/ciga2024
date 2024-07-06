using System;
using System.Collections;
using System.Collections.Generic;
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
        // Start is called before the first frame update
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

            switch (endingType)
            {
                case EndingType.Perfect :
                    break;
                case EndingType.Common :
                    break;
                case EndingType.EatingDisorder:
                    break;
                case EndingType.EatintTooMuch:
                    break;
                case EndingType.Anorexia :
                    break;
                case EndingType.Death :
                    break;
                case EndingType.Starve :
                    break;
                case EndingType.Boom :
                    break;
                default:
                    break;
            }
            
            
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
