using ETModel;
using System.Net;
using System.Numerics;

namespace ETHotfix
{
    public static class MatchComponentEx
    {
        public static async void Update(this MatchComponent self)
        {
            if (self.Match != null)
            {
                if (self.Match.IsStart)
                {
                    self.Match.IsStart = false;
                    IPEndPoint mapAddress = Game.Scene.GetComponent<StartConfigComponent>().MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
                    Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
                    foreach (long id in self.Match.PlayerIds)
                    {
                        Player player = Game.Scene.GetComponent<PlayerComponent>().Get(id);
                        await mapSession.Call(new G2G_MatchStartRequest() { PlayerId = player.Id, GateSessionId = player.GateSessionActorId });
                    }
                }
            }
        }
    }
}