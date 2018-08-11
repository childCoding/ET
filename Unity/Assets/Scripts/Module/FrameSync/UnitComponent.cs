using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ETModel
{
	[ObjectSystem]
	public class UnitComponentSystem : AwakeSystem<UnitComponent>
	{
		public override void Awake(UnitComponent self)
		{
			self.Awake();
		}
	}
	
	public class UnitComponent: Component
	{
		public static UnitComponent Instance { get; private set; }

		public Unit MyUnit;
		
		private readonly Dictionary<long, Unit> idUnits = new Dictionary<long, Unit>();

		public void Awake()
		{
			Instance = this;
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();

			foreach (Unit unit in this.idUnits.Values)
			{
				unit.Dispose();
			}

			this.idUnits.Clear();

			Instance = null;
		}
        public long nearDeathUnitId()
        {
            long id = 0;
            var me = UnitComponent.Instance.MyUnit;
            var units = UnitComponent.Instance.GetAll();
            foreach (var u in units)
            {
                if (u.GetComponent<StateMachineComponent>().IsDeath)
                {
                    var dir = me.Position - u.Position;
                    if (dir.magnitude < 1f)
                    {
                        id = u.Id;
                    }
                }
            }
            return id;
        }
		public void Add(Unit unit)
		{
			this.idUnits.Add(unit.Id, unit);
		}
        public Unit GetForwardUnit(Vector3 pos, float distance, Unit self)
        {
            foreach (var kv in idUnits)
            {
                if (kv.Value.UnitType == UnitType.Weak || kv.Value.Id == self.Id)
                    continue;
                var dir = kv.Value.Position - pos;
                var forward = self.Transform.forward;
                var angle = Vector3.Angle(forward, dir);
                if (angle < 120 && dir.magnitude < 1)
                {
                    return kv.Value;
                }
            }
            return null;
        }
        public Unit GetAround(Vector3 pos,float distance,long selfid )
        {
            foreach(var kv in idUnits)
            {
                if (kv.Value.UnitType == UnitType.Weak || kv.Value.Id == selfid)
                    continue;
                var dir = kv.Value.Position - pos;
                if (dir.magnitude < distance)
                {
                    return kv.Value;
                }
            }
            return null;
        }
		public Unit Get(long id)
		{
			Unit unit;
			this.idUnits.TryGetValue(id, out unit);
			return unit;
		}

		public void Remove(long id)
		{
			Unit unit;
			this.idUnits.TryGetValue(id, out unit);
			this.idUnits.Remove(id);
			unit?.Dispose();
		}

		public void RemoveNoDispose(long id)
		{
			this.idUnits.Remove(id);
		}

		public int Count
		{
			get
			{
				return this.idUnits.Count;
			}
		}

		public Unit[] GetAll()
		{
			return this.idUnits.Values.ToArray();
		}
	}
}