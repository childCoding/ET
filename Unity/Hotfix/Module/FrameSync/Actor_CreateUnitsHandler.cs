using UnityEngine;
using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_CreateUnitsHandler : AMHandler<Actor_CreateUnits>
    {
        protected override void Run(ETModel.Session session, Actor_CreateUnits message)
        {
            // 加载资源
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("unit.unity3d");
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("item.unity3d");
            UnitComponent unitComponent = ETModel.Game.Scene.GetComponent<UnitComponent>();

            foreach (UnitInfo unitInfo in message.Units)
            {
                Log.Debug($"unitid:{unitInfo.UnitId} playerid:{unitInfo.PlayerId} type:{unitInfo.Type} myunitid:{PlayerComponent.Instance.MyPlayer.Id}");
                if (unitComponent.Get(unitInfo.UnitId) != null)
                {
                    continue;
                }
                Log.Debug($"unitid:{unitInfo.UnitId} create");
                Unit unit = UnitFactory.Create(unitInfo.UnitId, (UnitType)unitInfo.Type);
                unit.PlayerId = unitInfo.PlayerId;
                unit.Position = new Vector3(unitInfo.X / 1000f, 0, unitInfo.Z / 1000f);
                unit.IntPos = new VInt3(unitInfo.X, 0, unitInfo.Z);
                if (PlayerComponent.Instance.MyPlayer.Id == unit.PlayerId)
                {
                    unitComponent.MyUnit = unit;
                    unit.AddComponent<ItemComponent>();
                    var ui = Game.Scene.GetComponent<UIComponent>().Get(UIType.UIOperation);
                    ui.GetComponent<UIOperationComponent>().SetUnit(unit);
                    ETModel.Game.Scene.AddComponent<CameraComponent, Unit>(unit);
                    Game.Scene.AddComponent<OperaComponent>();
                }
            }
            
        }
    }
}
