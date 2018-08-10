using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Map)]
	public class Frame_ClickMapHandler : AMHandler<Frame_ClickMap>
	{
		protected override void Run(ETModel.Session session, Frame_ClickMap message)
		{
            Unit unit = Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            unit.Position = new System.Numerics.Vector3(message.X / 1000, 0, message.Z / 1000);
        }
	}
}
