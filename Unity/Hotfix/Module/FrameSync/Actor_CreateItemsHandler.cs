using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_CreateItemsHandler : AMHandler<Actor_CreateItems>
    {
        protected override void Run(ETModel.Session session, Actor_CreateItems message)
        {
            ItemComponent itemComponent = ETModel.Game.Scene.GetComponent<ItemComponent>();
            foreach (ItemInformation itemInformation in message.Items)
            {
                if (itemComponent.Get(itemInformation.Id) != null)
                {
                    continue;
                }
                Item item = ItemFactory.Create(itemInformation.Id);
                item.Position = new Vector3(itemInformation.X, itemInformation.Y, itemInformation.Z);
            }

        }
    }
}
