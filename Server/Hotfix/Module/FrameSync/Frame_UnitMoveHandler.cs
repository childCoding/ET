using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class Frame_UnitMoveHandler : AMHandler<Frame_UnitMove>
    {
        protected override void Run(ETModel.Session session, Frame_UnitMove message)
        {
            Unit unit = Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            unit.Position = new System.Numerics.Vector3(message.Pos.X, message.Pos.Y, message.Pos.Z);
        }
    }
}
