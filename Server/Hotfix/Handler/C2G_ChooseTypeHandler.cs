using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_ChooseTypeHandler : AMHandler<C2G_ChooseType>
    {
        protected override void Run(Session session, C2G_ChooseType message)
        {
            MatchComponent matchComponent = Game.Scene.GetComponent<MatchComponent>();
            if (matchComponent.CanSignUp(message.Id, (UnitType)message.Type))
            {
                matchComponent.SignUp(message.Id);
                session.GetComponent<SessionPlayerComponent>().Player.UnitType = (UnitType)message.Type;
            }
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            Actor_HallInformation actorHallInformation = new Actor_HallInformation();
            foreach (long id in matchComponent.GetAllCompetitor())
            {
                Player player = playerComponent.Get(id);
                actorHallInformation.PlayerChooseType.Add(new PlayerChooseType { Id = player.Id, Account = player.Account, Type = (int)player.UnitType });
            }
            MessageHelper.BroadcastByPlayer(actorHallInformation);
        }
    }
}