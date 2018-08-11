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
            while (true)
            {
                int number = RandomHelper.RandomNumber(0, 1);
                await Game.Scene.GetComponent<TimerComponent>().WaitAsync(number * 1000);
                IConfig[] scoreConfigs = Game.Scene.GetComponent<ConfigComponent>().GetAll(typeof(ScoreConfig));
                number = RandomHelper.RandomNumber(0, scoreConfigs.Length);
                ScoreConfig scoreConfig = scoreConfigs[number] as ScoreConfig;
                if (Game.Scene.GetComponent<ItemComponent>().Get(scoreConfigs[number].Id) == null)
                {
                    Item newItem = ComponentFactory.CreateWithId<Item>(scoreConfig.Id);
                    newItem.IsShow = true;
                    newItem.Position = new Vector3((float)scoreConfig.X, (float)scoreConfig.Y, (float)scoreConfig.Z);
                    self.Add(newItem);
                }
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
                        if (unit.UnitType == UnitType.Weak && item.Owner == 0)
                        {
                            unit.Score++;
                            actorRemoveItems.Id.Add(item.Id);
                            self.Remove(item.Id);
                            break;
                        }
                        else if (unit.UnitType != UnitType.Weak && item.Owner != 0)
                        {
                            unit.Score++;
                            actorRemoveItems.Id.Add(item.Id);
                            self.Remove(item.Id);
                            break;
                        }
                    }
                }
            }
            if (actorRemoveItems.Id.Count > 0)
            {
                MessageHelper.Broadcast(actorRemoveItems);
                Game.Scene.GetComponent<UnitComponent>().SendMatchInformation();
            }
        }
    }
}