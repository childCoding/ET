using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class ServerFrameComponentSystem : AwakeSystem<ServerFrameComponent>
    {
        public override void Awake(ServerFrameComponent self)
        {
            self.Awake();
        }
    }

    public static class ServerFrameComponentEx
    {
        public static void Awake(this ServerFrameComponent self)
        {
            self.Frame = 0;
            self.FrameMessage = new FrameMessage() { Frame = self.Frame };
            self.UpdateFrameAsync();
        }

        public static async void UpdateFrameAsync(this ServerFrameComponent self)
        {
            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();

            long instanceId = self.InstanceId;

            while (true)
            {
                if (self.InstanceId != instanceId)
                {
                    return;
                }

                await timerComponent.WaitAsync(100);
                MessageHelper.Broadcast(self.FrameMessage);

                ++self.Frame;
                self.FrameMessage = new FrameMessage() { Frame = self.Frame };
                ItemComponent itemComponent = Game.Scene.GetComponent<ItemComponent>();
                if (itemComponent != null)
                {
                    itemComponent.GetScore();
                }
                UnitComponent unitComponent = Game.Scene.GetComponent<UnitComponent>();
                if (unitComponent != null)
                {
                    unitComponent.GetScore();
                }
                MatchComponent matchComponent = Game.Scene.GetComponent<MatchComponent>();
                if (matchComponent != null)
                {
                    matchComponent.Update();
                }
            }
        }

        public static void Add(this ServerFrameComponent self, OneFrameMessage oneFrameMessage)
        {
            self.FrameMessage.Message.Add(oneFrameMessage);
        }
    }
}