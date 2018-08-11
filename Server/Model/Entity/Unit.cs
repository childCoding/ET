using System.Numerics;
using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
	public enum UnitType
	{
        Idler = -1,
		Weak,
		Strong1,
        Strong2,
        Strong3,
        Strong4,
	}

	[ObjectSystem]
	public class UnitSystem : AwakeSystem<Unit, UnitType>
	{
		public override void Awake(Unit self, UnitType a)
		{
			self.Awake(a);
		}
	}

	public sealed class Unit: Entity
	{
		public UnitType UnitType { get; private set; }
		public long PlayerId { get; set; }
        // 玩家积分
        public int Score { get; set; }
		[BsonIgnore]
		public Vector3 Position { get; set; }
		
		public void Awake(UnitType unitType)
		{
			this.UnitType = unitType;
		}

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