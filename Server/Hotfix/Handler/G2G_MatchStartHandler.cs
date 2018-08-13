using System;
using System.Net;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class G2G_MatchStartRequestHandler : AMRpcHandler<G2G_MatchStartRequest, G2G_MatchStartResponse>
    {
        protected override async void Run(Session session, G2G_MatchStartRequest message, Action<G2G_MatchStartResponse> reply)
        {
            G2G_MatchStartResponse response = new G2G_MatchStartResponse();
            try
            {
                // 在map服务器上创建战斗Unit
                IPEndPoint mapAddress = Game.Scene.GetComponent<StartConfigComponent>().MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
                Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
                await mapSession.Call(new G2M_CreateUnit() { PlayerId = message.PlayerId, GateSessionId = message.GateSessionId });

                //Player player = Game.Scene.GetComponent<PlayerComponent>().Get(message.PlayerId);
                //ActorMessageSenderComponent actorMessageSenderComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();
                //actorMessageSenderComponent.GetWithActorId(player.GateSessionActorId).Send(new G2C_PlayerEnterMap());
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}