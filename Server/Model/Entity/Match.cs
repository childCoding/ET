using System.Collections.Generic;

namespace ETModel
{
	public sealed class Match: Entity
	{
        // 弱势方积分
        public int WeakScore { get; set; }
        // 强势方积分
        public int StrongScore { get; set; }
        // 比赛结束时间
        public long EndTime { get; set; }
        // 强势方是否活着
        public Dictionary<long, bool> IsStrongAlive = new Dictionary<long, bool>();
        // 参赛人员
        public List<long> PlayerIds = new List<long>();
        // 比赛开始
        public bool IsStart { get; set; }

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();
		}
	}
}