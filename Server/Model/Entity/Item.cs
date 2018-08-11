using MongoDB.Bson.Serialization.Attributes;
using System.Numerics;

namespace ETModel
{
    public sealed class Item: Entity
	{
        public bool IsShow { get; set; }
		[BsonIgnore]
		public Vector3 Position { get; set; }

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