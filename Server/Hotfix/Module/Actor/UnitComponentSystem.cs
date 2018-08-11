using ETModel;
using System.Collections.Generic;

namespace ETHotfix
{
    public static class UnitComponentEx
    {
        private static void SendMatchInformation(this UnitComponent self)
        {
            Actor_MatchInformation actorMatchInformation = new Actor_MatchInformation();
            List<Unit> units = self.GetUnitsByType(UnitType.Weak);
            foreach (Unit unit in units)
            {
                actorMatchInformation.WeakScore += unit.Score;
            }
            units = self.GetUnitsByType(UnitType.Strong1);
            foreach (Unit unit in units)
            {
                actorMatchInformation.StrongScore += unit.Score;
            }
            MessageHelper.Broadcast(actorMatchInformation);
        }
    }
}