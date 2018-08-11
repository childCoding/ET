using ETModel;

namespace ETHotfix
{
	[MessageHandler]
	public class G2C_PlayerEnterMapHandler : AMHandler<G2C_PlayerEnterMap>
	{
		protected override void Run(ETModel.Session session, G2C_PlayerEnterMap message)
        {
            Log.Debug("wbbbbbbbbbbbbbbbbbbbb");
            Game.Scene.GetComponent<UIComponent>().Create(UIType.UIOperation);
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.UILobby);
        }
	}
}