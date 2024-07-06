using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class CountDown : MonoBehaviour
    {
        public PlayerRightMainPanel playerRightMainPanel;
        public float totalTime;
        float startTime;
        float time;

        private void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            var _ = GameMgr.Singleton;
            if (playerRightMainPanel == null) UIRoot.Singleton.GetOpenedPanel<PlayerRightMainPanel>(out playerRightMainPanel);
            startTime = Time.time;
            time = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (Player.Instance.isEnd) return;
            if(Time.time-time>=1)
            {
                time = Time.time;
                totalTime -= 1f;
                playerRightMainPanel.SetTextMeshProTime(totalTime);
            }
            if (totalTime <= 0) TimeEnd();
        }

        public void TimeEnd()
        {
            TypeEventSystem.Global.Send<TimeEndEvent>();
        }
    }

    public class TimeEndEvent
    {

    }
}
