using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class Frame_UnitMoveHandler : AMHandler<Frame_UnitMove>
    {
        protected override void Run(ETModel.Session session, Frame_UnitMove message)
        {
            Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            Vector3 dir = ETModel.Utility.UnityVector3FromETVector3(message.Dir);
            Vector3 dest = unit.Position + dir * 5;
            
            moveComponent.MoveToDir(dir);
        }
    }
}
