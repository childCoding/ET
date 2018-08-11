using System;
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
		private GameObject enterMap;
		public Text Text;

		public void Awake()
		{
			ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
			GameObject sendBtn = rc.Get<GameObject>("Send");
			GameObject sendRpcBtn = rc.Get<GameObject>("" + "SendRpc");
			sendBtn.GetComponent<Button>().onClick.Add(this.OnSend);
			sendRpcBtn.GetComponent<Button>().onClick.Add(this.OnSendRpc);

			GameObject transfer1Btn = rc.Get<GameObject>("Transfer1");
			GameObject transfer2Btn = rc.Get<GameObject>("Transfer2");
			transfer1Btn.GetComponent<Button>().onClick.Add(this.OnTransfer1);
			transfer2Btn.GetComponent<Button>().onClick.Add(this.OnTransfer2);
			
			enterMap = rc.Get<GameObject>("EnterMap");
			enterMap.GetComponent<Button>().onClick.Add(this.EnterMap);
			this.Text = rc.Get<GameObject>("Text").GetComponent<Text>();
            GameObject button = rc.Get<GameObject>("Weak");
            button.GetComponent<Button>().onClick.AddListener(() => { this.ChooseUnitType(0); });
            button = rc.Get<GameObject>("Strong1");
            button.GetComponent<Button>().onClick.AddListener(() => { this.ChooseUnitType(1); });
            button = rc.Get<GameObject>("Strong2");
            button.GetComponent<Button>().onClick.AddListener(() => { this.ChooseUnitType(2); });
            button = rc.Get<GameObject>("Strong3");
            button.GetComponent<Button>().onClick.AddListener(() => { this.ChooseUnitType(3); });
            button = rc.Get<GameObject>("Strong4");
            button.GetComponent<Button>().onClick.AddListener(() => { this.ChooseUnitType(4); });
        }

		private void OnSend()
		{
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
            ETModel.SessionComponent.Instance.Session.Send(new C2G_ChooseType() { Type = type });
        }

    }
}
