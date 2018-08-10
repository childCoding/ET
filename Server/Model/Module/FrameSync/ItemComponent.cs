using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ETModel
{
    public class ItemComponent: Component
	{
		private readonly Dictionary<long, Item> idItems = new Dictionary<long, Item>();

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
			this.idItems.Clear();
		}

		public void Add(Item item)
		{
			this.idItems.Add(item.Id, item);
		}

		public Item Get(long id)
		{
			this.idItems.TryGetValue(id, out Item item);
			return item;
		}

		public void Remove(long id)
		{
            Item item;
			this.idItems.TryGetValue(id, out item);
			this.idItems.Remove(id);
            item?.Dispose();
		}

		public void RemoveNoDispose(long id)
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
	}
}