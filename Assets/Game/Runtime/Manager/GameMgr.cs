using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityToolkit;

namespace Game
{
    public class GameMgr : MonoSingleton<GameMgr>
    {
        protected override void OnInit()
        {
            UIRoot.Singleton.OpenPanel<PlayerRightMainPanel>();
            PlayerRightMainPanel playerRightMainPanel;
            UIRoot.Singleton.GetOpenedPanel<PlayerRightMainPanel>(out playerRightMainPanel);
            var player= GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
            GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Click"));
            var foodCreater= GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/FoodCreateManager"));
            var countDown= GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/CountDown"));
            var config= Global.Singleton.GetConfig();
            player.GetComponent<Player>().health = config.health;
            player.GetComponent<Player>().appetite = config.appetite;
            player.GetComponent<Player>().stomach = config.stomach;
            countDown.GetComponent<CountDown>().totalTime = config.totalTime;
            playerRightMainPanel.Reset(config.totalTime.ToString());
            foodCreater.GetComponent<FoodCreatManager>().prefabs = config.foodList;
        }

        protected override void OnDispose()
        {
            UIRoot.Singleton.ClosePanel<PlayerRightMainPanel>();
            Time.timeScale = 1;
        }

        public void Stop()
        {
            Time.timeScale = 0;
        }
    }
}