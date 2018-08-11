using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2M_ReloadHandler : AMHandler<C2G_ChooseType>
    {
        protected override void Run(Session session, C2G_ChooseType message)
        {
            session.GetComponent<SessionPlayerComponent>().Player.UnitType = (UnitType)message.Type;
            Actor_HallInformation actorHallInformation = new Actor_HallInformation();
            Player[] players = Game.Scene.GetComponent<PlayerComponent>().GetAll();
            foreach (Player player in players)
            {
                actorHallInformation.PlayerChooseType.Add(new PlayerChooseType { Id = player.Id, Type = (int)player.UnitType });
            }
            MessageHelper.BroadcastByPlayer(actorHallInformation);
        }
    }
}