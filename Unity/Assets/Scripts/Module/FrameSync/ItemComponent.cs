using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
	[ObjectSystem]
	public class ItemComponentAwakeSystem : AwakeSystem<ItemComponent>
	{
		public override void Awake(ItemComponent self)
		{
			self.Awake();
		}
	}
	
	public class ItemComponent : Component
	{
		private readonly Dictionary<long, Item> idItems = new Dictionary<long, Item>();

		public void Awake()
		{
		}
		
		public void Add(Item item)
		{
			this.idItems.Add(item.Id, item);
		}

		public Item Get(long id)
		{
			Item item;
			this.idItems.TryGetValue(id, out item);
			return item;
		}

		public void Remove(long id)
		{
			this.idItems.Remove(id);
		}

		public int Count
		{
			get
			{
				return this.idItems.Count;
			}
		}

		public Item[] GetAll()
		{
			return this.idItems.Values.ToArray();
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();
			foreach (Item item in this.idItems.Values)
			{
                item.Dispose();
			}
		}
	}
}