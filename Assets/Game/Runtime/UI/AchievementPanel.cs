using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using UnityToolkit;

namespace Game
{
    public class AchievementPanel : UIPanel
    {
        public List<string> liststr =new List<string>(){ "0", "1", "2", "3", "4", "5", "6", "7", "8" };
        public List<Image> listImg = new List<Image>();

        public Button btnClose;

        private void Start()
        {
            btnClose.onClick.AddListener(() =>
            {
                UIRoot.Singleton.ClosePanel<AchievementPanel>();
            });
        }

        public void newstr()
        {
            for (int i = 0; i < 8; ++i)
            {
                PlayerPrefs.SetInt(liststr[i], 0);
            }
        }

        public void SetStr(int num)
        {
            PlayerPrefs.SetInt(liststr[num],1);
        }

        public bool IsNull()
        {
            return PlayerPrefs.GetInt("0",-1) != -1;
        }


        private void Update()
        {
            
            for (int i = 0; i < 8; ++i)
            {
                if (PlayerPrefs.GetInt(liststr[i]) == 0)
                {
                    listImg[i].color = Color.gray;
                }
                else
                {
                    listImg[i].color = Color.white;
                }
            }
        }
        
        
    }
}
