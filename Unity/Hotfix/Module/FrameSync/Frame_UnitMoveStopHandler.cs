using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class Frame_UnitMoveStopHandler : AMHandler<Frame_UnitMoveStop>
    {
        protected override void Run(ETModel.Session session, Frame_UnitMoveStop message)
        {
            Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            //Vector3 dir = ETModel.Utility.UnityVector3FromETVector3(message.Dir);
            //Vector3 dest = unit.Position + dir * 5;
            moveComponent.Stop();
            //moveComponent.MoveToDest(dest, 1);
        }
    }
}
