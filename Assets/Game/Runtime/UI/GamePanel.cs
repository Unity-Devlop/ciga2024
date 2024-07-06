using System;
using UnityEngine;
using UnityEngine.UI;
using UnityToolkit;

namespace Game
{
    public struct TurnToInfiniteHp : IEvent { }
    
    public struct UpdateHp : IEvent { }

    public struct UpdateEnergy : IEvent { public float energyFillAmount; }
    
    public struct EnterLevel : IEvent { public int levelNum; }
    
    public class GamePanel : UIPanel
    {
        [SerializeField] private Transform infiniteHp;
        [SerializeField] private Transform hpRoot;
        [SerializeField] private Image[] imgHps = new Image[3];
        [SerializeField] private Sprite[] spriteHps = new Sprite[2];
        
        [SerializeField] private Image imgEnegy;
        
        [SerializeField] private Image imgState;
        [SerializeField] private Sprite[] spriteStates = new Sprite[4];
        
        [SerializeField] private Image imgPhone;
        [SerializeField] private Sprite[] spritePhones = new Sprite[2];
        [SerializeField] private Image imgConnect;
        [SerializeField] private Sprite[] spriteConnects = new Sprite[2];
        [SerializeField] private Image imgHeadSet;
        [SerializeField] private Sprite[] spriteHeadSets = new Sprite[2];

        private void Start()
        {
            Global.Event.Listen<TurnToInfiniteHp>(TurnToInfiniteHp);
            Global.Event.Listen<UpdateHp>(UpdateHp);
            Global.Event.Listen<UpdateEnergy>(UpdateEnergy);
            Global.Event.Listen<EnterLevel>(EnterLevel);
        }
        
        private void OnDestroy()
        {
            Global.Event.UnListen<TurnToInfiniteHp>(TurnToInfiniteHp);
            Global.Event.UnListen<UpdateHp>(UpdateHp);
            Global.Event.UnListen<UpdateEnergy>(UpdateEnergy);
            Global.Event.UnListen<EnterLevel>(EnterLevel);
        }

        private void EnterLevel(EnterLevel obj) => imgState.sprite = spriteStates[obj.levelNum - 1];
        
        private void UpdateEnergy(UpdateEnergy obj) => imgEnegy.fillAmount = obj.energyFillAmount;
        
        private void UpdateHp(UpdateHp obj)
        {
            var nowHP = GameMgr.Singleton.Local.data.Health;
            for (int i = 0; i < nowHP; ++i)
                imgHps[i].sprite = spriteHps[1];
            for (int i = nowHP; i < 3; ++i)
                imgHps[i].sprite = spriteHps[0];
        }

        private void TurnToInfiniteHp(TurnToInfiniteHp obj)
        {
            hpRoot.gameObject.SetActive(false);
            infiniteHp.gameObject.SetActive(true);
        }
    }
}
