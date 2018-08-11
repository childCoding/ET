using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2M_ReloadHandler : AMHandler<C2G_ChooseType>
    {
        protected override void Run(Session session, C2G_ChooseType message)
        {
            var curplayer = session.GetComponent<SessionPlayerComponent>().Player;
            Player[] players = Game.Scene.GetComponent<PlayerComponent>().GetAll();
            for(int i=0;i<players.Length;i++)
            {
                if( players[i].UnitType == (UnitType)message.Type && players[i].Id != curplayer.Id)
                {
                    return;
                }
            }
            curplayer.UnitType = (UnitType)message.Type;
            Actor_HallInformation actorHallInformation = new Actor_HallInformation();
            foreach (Player player in players)
            {
                actorHallInformation.PlayerChooseType.Add(new PlayerChooseType { Id = player.Id, Account = player.Account, Type = (int)player.UnitType });
            }
            MessageHelper.BroadcastByPlayer(actorHallInformation);
        }
    }
}