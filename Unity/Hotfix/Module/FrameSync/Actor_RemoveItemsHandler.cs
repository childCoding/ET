using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_RemoveItemsHandler : AMHandler<Actor_RemoveItems>
    {
        protected override void Run(ETModel.Session session, Actor_RemoveItems message)
        {
            ItemComponent itemComponent = ETModel.Game.Scene.GetComponent<ItemComponent>();
            foreach (long id in message.Id)
            {
                Item item = itemComponent.Get(id);
                if (item != null)
                {
                    if (item.Owner != 0)
                    {
                        item.Dispose();
                        itemComponent.Remove(id);
                    }
                    else
                    {
                        item.GameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
