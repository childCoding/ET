using ETModel;
using System.Numerics;

namespace ETHotfix
{
    public static class UnitComponentEx
    {
        public static void SendMatchInformation(this UnitComponent self)
        {
            Actor_MatchInformation actorMatchInformation = new Actor_MatchInformation();
            foreach (Unit unit in self.GetUnitsByType(UnitType.Weak))
            {
                actorMatchInformation.WeakScore += unit.Score;
            }
            foreach (Unit unit in self.GetUnitsByType(UnitType.Strong1))
            {
                actorMatchInformation.StrongScore += unit.Score;
            }
            foreach (Unit unit in self.GetUnitsByType(UnitType.Strong2))
            {
                actorMatchInformation.StrongScore += unit.Score;
            }
            foreach (Unit unit in self.GetUnitsByType(UnitType.Strong3))
            {
                actorMatchInformation.StrongScore += unit.Score;
            }
            foreach (Unit unit in self.GetUnitsByType(UnitType.Strong4))
            {
                actorMatchInformation.StrongScore += unit.Score;
            }
            MessageHelper.Broadcast(actorMatchInformation);
        }

        public static void GetScore(this UnitComponent self)
        {
            Unit[] units = self.GetAll();
            foreach (Unit unit in units)
            {
                foreach (Unit other in units)
                {
                    if (unit.UnitType != other.UnitType)
                    {
                        Vector3 vector = unit.Position - other.Position;
                        if (vector.Length() < 1 && self.GetScoreTime + 5 < TimeHelper.Now())
                        {
                            self.GetScoreTime = TimeHelper.ClientNowSeconds();
                            int score = 0;
                            if (other.UnitType == UnitType.Weak)
                            {
                                score = other.Score / 2;
                                unit.Score += score;
                                other.Score -= score;
                            }
                            else
                            {
                                score = unit.Score / 2;
                                unit.Score -= score;
                                other.Score += score;
                            }
                        }
                    }
                }
            }
            self.SendMatchInformation();
        }
    }
}