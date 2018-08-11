using UnityEngine;

namespace ETModel
{
    public static class UnitFactory
    {
        public static Unit Create(long id, UnitType unitType)
        {
            ResourcesComponent resourcesComponent = Game.Scene.GetComponent<ResourcesComponent>();
            GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset("Unit.unity3d", "Unit");

            string fab = unitType == UnitType.Weak ? "Weak" : "Hero";
            GameObject prefab = bundleGameObject.Get<GameObject>(fab);

            Unit unit = ComponentFactory.CreateWithId<Unit, UnitType>(id, unitType);
            unit.GameObject = UnityEngine.Object.Instantiate(prefab);
            GameObject parent = GameObject.Find($"/Global/Unit");
            unit.GameObject.transform.SetParent(parent.transform, false);
            unit.AddComponent<AnimatorComponent>();
            unit.AddComponent<MoveComponent>();
            unit.AddComponent<BoneComponent>();
            Game.Scene.GetComponent<UnitComponent>().Add(unit);
            InitColor(unit.GameObject,GetColor(unitType));
            return unit;
        }

        public static void InitColor(GameObject go,Color clr)
        {
            var render = go.GetComponentInChildren<SkinnedMeshRenderer>();
            if(render)
            {
                MaterialPropertyBlock block = new MaterialPropertyBlock();
                block.SetColor("_Color", clr);
                render.SetPropertyBlock(block);
            }
            
        }
        public static Color32 GetColor(UnitType t)
        {
            switch (t)
            {
                case UnitType.Strong1:return Color.green;
                case UnitType.Strong2:return Color.red;
                case UnitType.Strong3:return Color.blue;
                case UnitType.Strong4: return Color.yellow;
                default:return Color.white;
            }
        }
    }
}