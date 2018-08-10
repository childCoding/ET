using ETModel;
using System.Numerics;

namespace ETHotfix
{
    [ObjectSystem]
    public class ItemComponentSystem : AwakeSystem<ItemComponent>
    {
        public override void Awake(ItemComponent self)
        {
            self.Awake();
        }
    }

    public static class ItemComponentEx
    {
        public static void Awake(this ItemComponent self)
        {
            self.update();
        }

        private static async void update(this ItemComponent self)
        {
            return;
            //while (true)
            {
                int number = RandomHelper.RandomNumber(0, 1);
                await Game.Scene.GetComponent<TimerComponent>().WaitAsync(number * 1000);
                Item newItem = ComponentFactory.Create<Item>();
                ScoreConfig scoreConfig = (ScoreConfig)Game.Scene.GetComponent<ConfigComponent>().Get(typeof(ScoreConfig), 1);
                newItem.ConfigId = scoreConfig.Id;
                newItem.IsShow = true;
                newItem.Position = new Vector3(RandomHelper.RandomNumber(scoreConfig.MinX, scoreConfig.MaxX), scoreConfig.Y, RandomHelper.RandomNumber(scoreConfig.MinZ, scoreConfig.MaxZ));
                self.Add(newItem);
                Actor_CreateItems actorCreateItems = new Actor_CreateItems();
                Item[] items = self.GetAll();
                foreach (Item item in items)
                {
                    actorCreateItems.Items.Add(new ItemInformation { Id = item.Id, X = item.Position.X, Y = item.Position.Y, Z = item.Position.Z });
                }
                MessageHelper.Broadcast(actorCreateItems);
            }
        }

        public static void GetScore(this ItemComponent self)
        {
            return;
            Item[] itemds = self.GetAll();
            Actor_RemoveItems actorRemoveItems = new Actor_RemoveItems();
            foreach (Item item in itemds)
            {
                Unit[] units = Game.Scene.GetComponent<UnitComponent>().GetAll();
                foreach (Unit unit in units)
                {
                    Vector3 vector = unit.Position - item.Position;
                    if (vector.Length() < 1)
                    {
                        unit.Score++;
                        actorRemoveItems.Id.Add(item.Id);
                        self.Remove(item.Id);
                        break;
                    }
                }
            }
            if (actorRemoveItems.Id.Count > 0)
            {
                MessageHelper.Broadcast(actorRemoveItems);
            }
        }
    }
}