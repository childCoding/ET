using UnityEngine;

namespace ETModel
{
    public static class ItemFactory
    {
        public static Item Create(long id)
        {
	        ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
	        GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset("Item.unity3d", "Item");
            Item item = ComponentFactory.CreateWithId<Item>(id);
            item.GameObject = UnityEngine.Object.Instantiate(bundleGameObject);
            GameObject parent = GameObject.Find($"/Global/Unit");
            item.GameObject.transform.SetParent(parent.transform, false);
            Game.Scene.GetComponent<ItemComponent>().Add(item);
            return item;
        }
    }
}