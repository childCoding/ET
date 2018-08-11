using ETModel;

namespace ETHotfix
{
	[MessageHandler]
	public class Actor_SpitScoreHandler : AMHandler<Actor_SpitScore>
	{
		protected override void Run(ETModel.Session session, Actor_SpitScore message)
		{
            Item item = ItemFactory.Create(message.ItemId);
            item.Position = Utility.UnityVector3FromETVector3(message.Position);
            item.Number = message.Number;
            item.Owner = message.Id;
            item.GameObject.transform.localScale = new UnityEngine.Vector3(2, 2, 2);
        }
	}
}
