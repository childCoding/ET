using ETModel;
using UnityEngine;

namespace ETHotfix
{
	[Event(EventIdType.InitSceneStart)]
	public class InitSceneStart_CreateLoginUI: AEvent
	{
		public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Create(UIType.UILogin);
        }
	}
}
