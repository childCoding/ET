using MongoDB.Bson.Serialization.Attributes;
using System.Numerics;

namespace ETModel
{
    public sealed class Item: Entity
	{
        public bool CanGet { get; set; }
		[BsonIgnore]
		public Vector3 Position { get; set; }
        // 归属
        public long Owner { get; set; }
        // 数量
        public int Number { get; set; }

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