namespace ETModel
{
    public class MatchComponent : Component
    {
        public Match Match = null;

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();
            this.Match.Dispose();
        }

        public bool CanSignUp(long id, UnitType unitType)
        {
            if (this.Match == null)
            {
                this.Match = ComponentFactory.Create<Match>();
                return true;
            }
            if (this.Match.PlayerIds.Count >= 5)
            {
                if (TimeHelper.ClientNowSeconds() >= this.Match.EndTime)
                {
                    this.Match = ComponentFactory.Create<Match>();
                    return true;
                }
                return false;
            }
            foreach (long playerId in this.Match.PlayerIds)
            {
                Player competitor = Game.Scene.GetComponent<PlayerComponent>().Get(playerId);
                if (competitor.UnitType == unitType)
                {
                    return false;
                }
            }
            return true;
        }

        public void SignUp(long id)
        {
            this.Match.PlayerIds.Remove(id);
            this.Match.PlayerIds.Add(id);
            if (this.Match.PlayerIds.Count >= 5)
            {
                this.matchStart();
            }
        }

        public void matchStart()
        {
            this.Match.EndTime = TimeHelper.ClientNowSeconds() + 3000;
            this.Match.IsStart = true;
        }

        public void MatchEnd()
        {
        }

        public long[] GetAllCompetitor()
        {
            return this.Match.PlayerIds.ToArray();
        }
    }
}