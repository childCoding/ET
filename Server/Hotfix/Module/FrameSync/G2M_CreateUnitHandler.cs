﻿using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class G2M_CreateUnitHandler : AMRpcHandler<G2M_CreateUnit, M2G_CreateUnit>
    {
        protected override async void Run(Session session, G2M_CreateUnit message, Action<M2G_CreateUnit> reply)
        {
            M2G_CreateUnit response = new M2G_CreateUnit();
            try
            {
                Unit unit = ComponentFactory.Create<Unit,UnitType>(Game.Scene.GetComponent<PlayerComponent>().Get(message.PlayerId).UnitType);
                unit.PlayerId = message.PlayerId;
                await unit.AddComponent<MailBoxComponent>().AddLocation();
                unit.AddComponent<UnitGateComponent, long>(message.GateSessionId);
                Game.Scene.GetComponent<UnitComponent>().Add(unit);
                response.UnitId = unit.Id;
                response.Count = Game.Scene.GetComponent<UnitComponent>().Count;
                reply(response);

                Actor_CreateUnits actorCreateUnits = new Actor_CreateUnits();
                Unit[] units = Game.Scene.GetComponent<UnitComponent>().GetAll();
                foreach (Unit u in units)
                {
                    actorCreateUnits.Units.Add(new UnitInfo() { UnitId = u.Id, PlayerId = u.PlayerId, Type = (int)u.UnitType, X = (int)(u.Position.X * 1000), Z = (int)(u.Position.Z * 1000) });
                }
                MessageHelper.Broadcast(actorCreateUnits);
                Game.Scene.GetComponent<UnitComponent>().SendMatchInformation();
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}