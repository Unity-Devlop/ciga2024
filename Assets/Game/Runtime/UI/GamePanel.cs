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
    
    public struct RingUp : IEvent { }
    
    public struct RingOver : IEvent { }
    
    public struct UnlockHeadSet : IEvent { }
    
    public struct ConnectHeadSet : IEvent { }
    
    public struct DisConnectHeadSet : IEvent { }
    
    
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
        [SerializeField] private Transform connect;
        [SerializeField] private Image imgConnect;
        [SerializeField] private Sprite[] spriteConnects = new Sprite[2];
        [SerializeField] private Transform headSet;
        [SerializeField] private Image imgHeadSet;
        [SerializeField] private Sprite[] spriteHeadSets = new Sprite[2];

        private void Start()
        {
            Init();
            
            Global.Event.Listen<TurnToInfiniteHp>(TurnToInfiniteHp);
            Global.Event.Listen<UpdateHp>(UpdateHp);
            Global.Event.Listen<UpdateEnergy>(UpdateEnergy);
            Global.Event.Listen<EnterLevel>(EnterLevel);
            Global.Event.Listen<RingUp>(RingUp);
            Global.Event.Listen<RingOver>(RingOver);
            
            Global.Event.Listen<UnlockHeadSet>(UnlockHeadSet);
            
            Global.Event.Listen<ConnectHeadSet>(ConnectHeadSet);
            Global.Event.Listen<DisConnectHeadSet>(DisConnectHeadSet);
        }
        
        private void OnDestroy()
        {
            if (Global.Singleton is null || Global.Event is null)
                return;
            
            Global.Event.UnListen<TurnToInfiniteHp>(TurnToInfiniteHp);
            Global.Event.UnListen<UpdateHp>(UpdateHp);
            Global.Event.UnListen<UpdateEnergy>(UpdateEnergy);
            Global.Event.UnListen<EnterLevel>(EnterLevel);
            Global.Event.UnListen<RingUp>(RingUp);
            Global.Event.UnListen<RingOver>(RingOver);
            
            Global.Event.UnListen<UnlockHeadSet>(UnlockHeadSet);
            
            Global.Event.UnListen<ConnectHeadSet>(ConnectHeadSet);
            Global.Event.UnListen<DisConnectHeadSet>(DisConnectHeadSet);
        }

        private void Init()
        {
            var data = GameMgr.Singleton.Local.data;
        }

        private void DisConnectHeadSet(DisConnectHeadSet obj)
        {
            imgConnect.sprite = spriteConnects[0];
            imgHeadSet.sprite = spriteHeadSets[0];
        }
        
        private void ConnectHeadSet(ConnectHeadSet obj)
        {
            imgConnect.sprite = spriteConnects[1];
            imgHeadSet.sprite = spriteHeadSets[1];
        }

        private void UnlockHeadSet(UnlockHeadSet obj)
        {
            headSet.gameObject.SetActive(true);   
        }
        
        private void RingOver(RingOver obj)
        {
            imgPhone.sprite = spritePhones[0];
            imgPhone.GetComponent<Animator>().enabled = false;
        }

        private void RingUp(RingUp obj)
        {
            imgPhone.sprite = spritePhones[1];
            imgPhone.GetComponent<Animator>().enabled = true;
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
