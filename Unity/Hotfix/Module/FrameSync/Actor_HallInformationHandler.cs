using ETModel;
using UnityEngine;

namespace ETHotfix
{
	[MessageHandler]
	public class Actor_HallInformationHandler : AMHandler<Actor_HallInformation>
	{
		protected override void Run(ETModel.Session session, Actor_HallInformation message)
		{
            UILobbyComponent uILobbyComponent = Game.Scene.GetComponent<UIComponent>().Get(UIType.UILobby).GetComponent<UILobbyComponent>();
            string text = "";
            foreach (PlayerChooseType playerChooseType in message.PlayerChooseType)
            {
                text += "(" + playerChooseType.Id.ToString() + ","+ playerChooseType.Type.ToString() + ")";
            }
            uILobbyComponent.Text.text = text;
        }
	}
}
