using ETModel;

namespace ETHotfix
{
	public static class MessageHelper
	{
		public static void Broadcast(IActorMessage message)
		{
			Unit[] units = Game.Scene.GetComponent<UnitComponent>().GetAll();
			ActorMessageSenderComponent actorMessageSenderComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();
			foreach (Unit unit in units)
			{
				UnitGateComponent unitGateComponent = unit.GetComponent<UnitGateComponent>();
				if (unitGateComponent.IsDisconnect)
				{
					continue;
				}
				
				actorMessageSenderComponent.GetWithActorId(unitGateComponent.GateSessionActorId).Send(message);
			}
        }

        public static void BroadcastByPlayer(IActorMessage message)
        {
            Player[] players = Game.Scene.GetComponent<PlayerComponent>().GetAll();
            ActorMessageSenderComponent actorMessageSenderComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();
            foreach (Player player in players)
            {
                actorMessageSenderComponent.GetWithActorId(player.GateSessionActorId).Send(message);
            }
        }
    }
}
