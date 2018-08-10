using UnityEngine;

namespace ETModel
{
    public static class UnitFactory
    {
        public static Unit Create(long id, UnitType unitType)
        {
            ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
            GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset("Unit.unity3d", "Unit");
            GameObject prefab = null;
            if (unitType == UnitType.Weak)
            {
                prefab = bundleGameObject.Get<GameObject>("Weak");
            }
            Unit unit = ComponentFactory.CreateWithId<Unit, UnitType>(id, unitType);
            unit.GameObject = UnityEngine.Object.Instantiate(prefab);
            GameObject parent = GameObject.Find($"/Global/Unit");
            unit.GameObject.transform.SetParent(parent.transform, false);
            unit.AddComponent<AnimatorComponent>();
            unit.AddComponent<MoveComponent>();
            Game.Scene.GetComponent<UnitComponent>().Add(unit);
            return unit;
        }
    }
}