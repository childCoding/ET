﻿using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	[ObjectSystem]
	public class UiLobbyComponentSystem : AwakeSystem<UILobbyComponent>
	{
		public override void Awake(UILobbyComponent self)
		{
			self.Awake();
		}
	}
	
	public class UILobbyComponent : Component
	{
		//public Text Text;

		public void Awake()
		{
			ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
			//GameObject sendBtn = rc.Get<GameObject>("Send");
			//GameObject sendRpcBtn = rc.Get<GameObject>("" + "SendRpc");
			//sendBtn.GetComponent<Button>().onClick.Add(this.OnSend);
			//sendRpcBtn.GetComponent<Button>().onClick.Add(this.OnSendRpc);

			//GameObject transfer1Btn = rc.Get<GameObject>("Transfer1");
			//GameObject transfer2Btn = rc.Get<GameObject>("Transfer2");
			//transfer1Btn.GetComponent<Button>().onClick.Add(this.OnTransfer1);
			//transfer2Btn.GetComponent<Button>().onClick.Add(this.OnTransfer2);
			
			rc.Get<GameObject>("EnterMap").GetComponent<Button>().onClick.Add(this.EnterMap);
			//this.Text = rc.Get<GameObject>("Text").GetComponent<Text>();
            rc.Get<GameObject>("boss").GetComponent<Button>().onClick.AddListener(() => { this.ChooseUnitType(0); });
            rc.Get<GameObject>("hero1").GetComponent<Button>().onClick.AddListener(() => { this.ChooseUnitType(1); });
            rc.Get<GameObject>("hero2").GetComponent<Button>().onClick.AddListener(() => { this.ChooseUnitType(2); });
            rc.Get<GameObject>("hero3").GetComponent<Button>().onClick.AddListener(() => { this.ChooseUnitType(3); });
            rc.Get<GameObject>("hero4").GetComponent<Button>().onClick.AddListener(() => { this.ChooseUnitType(4); });
        }
        public void InitPlayer(int index,long id, string account)
        {
            if (index < 0 || index > 4)
                return;
            var name = index == 0 ? "boss" : ($"hero{index}");
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            GameObject button = rc.Get<GameObject>(name);
            Utility.SearchChildRecurvese(button.transform, "select").gameObject.SetActive(id == 0);
            Utility.SearchChildRecurvese(button.transform, "Text").gameObject.GetComponent<Text>().text = id > 0 ? account : String.Empty;


        }
		private void OnSend()
		{
            //PlayerComponent.Instance.MyPlayer.
			// 发送一个actor消息
			ETModel.SessionComponent.Instance.Session.Send(new Actor_Test() { Info = "message client->gate->map->gate->client" });
		}

		private async void OnSendRpc()
		{
			try
			{
				// 向actor发起一次rpc调用
				Actor_TestResponse response = (Actor_TestResponse) await ETModel.SessionComponent.Instance.Session.Call(new Actor_TestRequest() { Request = "request actor test rpc" });
				Log.Info($"recv response: {JsonHelper.ToJson(response)}");
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		private async void OnTransfer1()
		{
			try
			{
				Actor_TransferResponse response = (Actor_TransferResponse) await ETModel.SessionComponent.Instance.Session.Call(new Actor_TransferRequest() {MapIndex = 0});
				Log.Info($"传送成功! {JsonHelper.ToJson(response)}");
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		private async void OnTransfer2()
		{
			Actor_TransferResponse response = (Actor_TransferResponse)await ETModel.SessionComponent.Instance.Session.Call(new Actor_TransferRequest() { MapIndex = 1 });
			Log.Info($"传送成功! {JsonHelper.ToJson(response)}");
		}

		private async void EnterMap()
		{
			try
			{
                Game.Scene.GetComponent<UIComponent>().Create(UIType.UIOperation);
                Game.Scene.GetComponent<UIComponent>().Remove(UIType.UILobby);
                await ETModel.SessionComponent.Instance.Session.Call(new C2G_EnterMap());
            }
			catch (Exception e)
			{
				Log.Error(e);
			}	
		}

        private void ChooseUnitType(int type)
        {
            ETModel.SessionComponent.Instance.Session.Send(new C2G_ChooseType() { Id = PlayerComponent.Instance.MyPlayer.Id, Type = type });
        }
         
    }
}
