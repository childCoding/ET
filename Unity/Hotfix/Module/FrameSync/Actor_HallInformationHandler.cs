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

            for(int i = 0;i < 5;i++)
            {
                uILobbyComponent.InitPlayer(i, 0,string.Empty);
            }
            foreach (PlayerChooseType playerChooseType in message.PlayerChooseType)
            {
                uILobbyComponent.InitPlayer(playerChooseType.Type, playerChooseType.Id, playerChooseType.Account);
            }

        }
	}
}
