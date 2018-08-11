using ETModel;
using System.Numerics;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_SpitScoreHandler : AMHandler<C2G_SpitScore>
    {
        protected override void Run(Session session, C2G_SpitScore message)
        {
            Unit unit = Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            if (unit.UnitType == UnitType.Weak)
            {
                Item item = ComponentFactory.Create<Item>();
                item.Position = new Vector3(unit.Position.X, unit.Position.Y, unit.Position.Z);
                item.Owner = unit.Id;
                item.Number = unit.Score / 2;
                unit.Score -= item.Number;
                Game.Scene.GetComponent<ItemComponent>().Add(item);
                Actor_SpitScore actorSpitScore = new Actor_SpitScore();
                actorSpitScore.Id = unit.Id;
                actorSpitScore.ItemId = item.Id;
                actorSpitScore.Position = new vector3() { X = item.Position.X, Y = item.Position.Y, Z = item.Position.Z };
                actorSpitScore.Number = item.Number;
                if (item.Number != 0)
                {
                    MessageHelper.Broadcast(actorSpitScore);
                    Game.Scene.GetComponent<UnitComponent>().SendMatchInformation();
                }
            }
        }
    }
}