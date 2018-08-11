using ETModel;
using UnityEngine;

namespace ETHotfix
{
	[MessageHandler]
	public class Frame_SprintHandler : AMHandler<Frame_Sprint>
	{
		protected override void Run(ETModel.Session session, Frame_Sprint message)
		{
			Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
			MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            //Transform transform = unit.GameObject.transform;
            //transform.Translate(transform, )
            Vector3 dest = unit.Position + unit.Rotation.eulerAngles.normalized * -5;
			moveComponent.Turn2D(dest - unit.Position);
			moveComponent.MoveToDest(dest, 5);
        }
	}
}
