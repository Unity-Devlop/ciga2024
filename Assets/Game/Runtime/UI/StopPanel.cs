using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityToolkit;

namespace Game
{
    public class StopPanel : MonoBehaviour
    {
        public static StopPanel panel=null;
        public static StopPanel Panel => panel;


        [SerializeField] private Button btnExit;
        [SerializeField] private Button btnContinue;
        [SerializeField] private Button btnReStart;


        private void Awake()
        {
            if(panel==null)
            {
                panel = this;
            }

            btnExit.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            });

            btnContinue.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });

            btnReStart.onClick.AddListener(() =>
            {
                Global.Singleton.ReSetLevel();

                gameObject.SetActive(false);
            });
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }

        public void OnDestroy()
        {
            Time.timeScale = 1;
        }
    }
}
