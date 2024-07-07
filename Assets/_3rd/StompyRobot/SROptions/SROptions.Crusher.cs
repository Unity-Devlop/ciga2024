// using Crusher.PbMessage;
// using CrusherBridge;
// using EngineCore;
// using FallGuys;
// using Script.Ad;
// using System.ComponentModel;
// using UnityEngine;
//
// public partial class SROptions
// {
//     [Category("服务器"), DisplayName("自定义GM地址"), Sort(0)]
//     public string NetworkAddressLocalGM
//     {
//         get => GameConfig.Instance.CUSTOM_GM;
//         set => GameConfig.Instance.CUSTOM_GM = value;
//     }
//
//     [Category("服务器"), DisplayName("ServerGmType"), Sort(0)]
//     public GameConfig.EmGMServerType GMServerType
//     {
//         get => GameConfig.Instance.GMServerType;
//         set => GameConfig.Instance.GMServerType = value;
//     }
//
//
//     [Category("道具"), DisplayName("金币"), Sort(0)]
//     [Increment(100), NumberRange(0, 1000000)]
//     public int GoodsCoin
//     {
//         get
//         {
//             return CrusherGameClient.User.GetGoodsCount(ConstCrusherGoodsId.Coin);
//         }
//         set
//         {
//             CrusherGameClient.User.RequestC2SChangeGoodsCount(ConstCrusherGoodsId.Coin, value - GoodsCoin);
//         }
//     }
//
//
//     [Category("道具"), DisplayName("钻石"), Sort(1)]
//     [Increment(100), NumberRange(0, 1000000)]
//     public int GoodsDiamond
//     {
//         get
//         {
//             return CrusherGameClient.User.GetGoodsCount(ConstCrusherGoodsId.Diamond);
//         }
//         set
//         {
//             CrusherGameClient.User.RequestC2SChangeGoodsCount(ConstCrusherGoodsId.Diamond, value - GoodsDiamond);
//         }
//     }
//
//     [Category("道具"), DisplayName("经验"), Sort(2)]
//     [Increment(100), NumberRange(0, 1000000)]
//     public int GoodsExp
//     {
//         get
//         {
//             return CrusherGameClient.User.GetGoodsCount(ConstCrusherGoodsId.Exp);
//         }
//         set
//         {
//             CrusherGameClient.User.RequestC2SChangeGoodsCount(ConstCrusherGoodsId.Exp, value - GoodsExp);
//         }
//     }
//
//     [Category("道具"), DisplayName("皇冠"), Sort(3)]
//     [Increment(1), NumberRange(0, 1000000)]
//     public int GoodsCrown
//     {
//         get
//         {
//             return CrusherGameClient.User.GetGoodsCount(ConstCrusherGoodsId.Crown);
//         }
//         set
//         {
//             CrusherGameClient.User.RequestC2SChangeGoodsCount(ConstCrusherGoodsId.Crown, value - GoodsCrown);
//         }
//     }
//
//
//     // [Category("网络连接"), DisplayName("Client"), Sort(2)]
//     // public void StartClient()
//     // {
//     //     if (NetworkServer.active)
//     //     {
//     //         Debug.LogError("当前设备已设置为服务器，无法再将此设备设为客户端，只能二选一");
//     //         return;
//     //     }
//     //     CrusherGameClient.FallGuysNetworkComponent.MirrorNetworkManager.StartClient();
//     // }
//     // [Category("网络连接"), DisplayName("服务器地址"), Sort(3)]
//     // public string NetworkAddress
//     // {
//     //     get => CrusherGameClient.FallGuysNetworkComponent.MirrorNetworkManager.networkAddress;
//     //     set => CrusherGameClient.FallGuysNetworkComponent.MirrorNetworkManager.networkAddress = value;
//     // }
//
//
//
//
//
//     [Category("系统设置"), DisplayName("NewAccount"), Sort(4)]
//     public void ClearUserLevel()
//     {
//         PlayerPrefs.DeleteAll();
//         Application.Quit();
//     }
//
//
//     [Category("GM"), DisplayName("Debug用户的UserID"), Sort(3)]
//     public string GMContent
//     {
//         get; set;
//     }
//     [Category("GM"), DisplayName("设置Debug用户"), Sort(2)]
//     public void SendGMMessage()
//     {
//         SendGMCommandMessage(GMCommandConst.DebugUser, GMContent);
//     }
//
//     [Category("GM"), DisplayName("MapID"), Sort(5)]
//     public string MapID
//     {
//         get; set;
//     }
//
//     [Category("GM"), DisplayName("进入地图"), Sort(6)]
//     public void SetEnterMapID()
//     {
//         //CrusherGameClient.FallGuysNetworkComponent.PinusNetworkManager.SendMessage("game.gameMatchRoomHandler.gameStartMatchRequest", new PinusProtocol.C2SStartMatch { EnterMapID = int.Parse(MapID) });
//     }
//
//
//     private void SendGMCommandMessage(string name, string content)
//     {
//         /**
//             if (CrusherGameClient.FallGuysNetworkComponent.PinusNetworkManager.NetworkState != PinusClient.NetWorkState.CONNECTED)
//             {
//                 Debug.LogWarning("当前客户端需要先连接到服务器才能发送GM消息");
//                 return;
//             }
//
//             Debug.Log($"sendGmCommand:   name={name}    content={content}");
//             CrusherGameClient.FallGuysNetworkComponent.PinusNetworkManager.SendMessage("game.gameUserHandler.gameGMCommandRequest", new PinusProtocol.C2SGMCommand
//             {
//                 commandName = name,
//                 content = content
//             });
//         **/
//         C2SGMCommand msg = new C2SGMCommand();
//         msg.Params.Add(name);
//         msg.Params.Add(content);
//         CrusherGameClient.NetworkConnectionComponent.SendMessage(msg);
//     }
//
//     [Category("天梯"), DisplayName("赛季奖杯积分"), Sort(1)]
//     [Increment(5), NumberRange(0, 100)]
//     public int SeasonLadderPoint
//     {
//         get
//         {
//             return ISingleton<ClientActivityModule>.Instance.SeasonLadderLogic.LadderData?.LadderPoint ?? 0;
//         }
//         set
//         {
//             SendGMCommandMessage(GMCommandConst.DebugSetSeasonLadder, $"{value},-1,-1");
//         }
//     }
//
//     [Category("天梯"), DisplayName("赛季星星"), Sort(2)]
//     [Increment(5), NumberRange(0, 999)]
//     public int SeasonLadderStar
//     {
//         get
//         {
//             return ISingleton<ClientActivityModule>.Instance.SeasonLadderLogic.LadderData?.LadderStar ?? 0;
//         }
//         set
//         {
//             SendGMCommandMessage(GMCommandConst.DebugSetSeasonLadder, $"-1,{value},-1");
//         }
//     }
//
//     [Category("天梯"), DisplayName("赛季段位"), Sort(3)]
//     [Increment(1), NumberRange(1, 33)]
//     public int SeasonLadderDan
//     {
//         get
//         {
//             return ISingleton<ClientActivityModule>.Instance.SeasonLadderLogic.LadderData?.LadderDan ?? 0;
//         }
//         set
//         {
//             SendGMCommandMessage(GMCommandConst.DebugSetSeasonLadder, $"-1,-1,{value}");
//         }
//     }
//
//     [Category("战令"), DisplayName("改积分"), Sort(1)]
//     [Increment(10), NumberRange(0, 10000)]
//     public int PartyPassScore
//     {
//         get
//         {
//             return ISingleton<ClientActivityModule>.Instance.PartyPassLogic.CurPassData?.TotalScore ?? 0;
//         }
//         set
//         {
//             SendGMCommandMessage(GMCommandConst.DebugSetPartyPassScore, $"{value}");
//         }
//     }
//
//     [Category("七日"), DisplayName("类型"), Sort(10)]
//     [Increment(1), NumberRange(1, 5)]
//     public int SevenDayType { get; set; } = 1;
//
//     [Category("七日"), DisplayName("增加的数量"), Sort(11)]
//     [Increment(1), NumberRange(1, 1000)]
//     public int SevenDayProgress { get; set; }
//
//     [Category("七日"), DisplayName("设置"), Sort(12)]
//     public void SendSevenDayGMCommand()
//     {
//         SendGMCommandMessage(GMCommandConst.DebugSetSevenDay, $"{SevenDayType},{SevenDayProgress}");
//     }
//
//
//     [Category("七日开始时间"), DisplayName("提前天数"), Sort(14)]
//     [Increment(1), NumberRange(1, 7)]
//     public int SevenDayAhead { get; set; }
//
//     [Category("七日开始时间"), DisplayName("设置七日天数"), Sort(15)]
//     public void SendSevenDayAheadDaysGMCommand()
//     {
//         if (SevenDayAhead <= 0)
//         {
//             throw new System.ArgumentException("提前的天数必须大于0");
//         }
//         SendGMCommandMessage(GMCommandConst.DebugSetSevenDayModifiyTime, $"{SevenDayAhead}");
//
//         CrusherGameClient.NetworkConnectionComponent.SendMessage(new C2SSevenDayInfo());
//     }
//
//     [Category("皮肤"), DisplayName("测试皮肤"), Sort(0)]
//     public bool GMUnlockAllActorSkins
//     {
//         get => PublicTools.Instance.TestUnlockAllSkins;
//         set => PublicTools.Instance.TestUnlockAllSkins = value;
//     }
//
//     [Category("转盘"), DisplayName("免费抽"), Sort(0)]
//     public bool GMFreeLuckySpin
//     {
//         get => PublicTools.Instance.TestFreeLuckySpin;
//         set => PublicTools.Instance.TestFreeLuckySpin = value;
//     }
//
//
//     [Category("Test"), DisplayName("普通报错")]
//     public void MakeCrush()
//     {
//         GameObject go = null;
//         go.name = "a";
//     }
//
//     [Category("Test"), DisplayName("Unity干崩")]
//     public void MakeUpdateCrush()
//     {
//         UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.FatalError);
//     }
//
//     [Category("Ad"), DisplayName("广告测试详情")]
//     public void ShowTestAdView()
//     {
//         AdComponent.Instance.ShowTest();
//     }
//
// }
