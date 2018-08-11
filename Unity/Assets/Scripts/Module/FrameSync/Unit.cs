using UnityEngine;

namespace ETModel
{
	public enum UnitType
    {
        Weak,
        Strong1,
        Strong2,
        Strong3,
        Strong4,
    }
	
	public sealed class Unit: Entity
	{
        public long PlayerId { get; set; }

		public VInt3 IntPos;

		public GameObject GameObject;
        // 积分
        public int Score { get; set; }
        // 类型
        public UnitType UnitType { get; set; }
		
		public void Awake(UnitType unitType)
		{
            this.UnitType = unitType;
		}
        public Transform Transform {
            get { return GameObject.transform; }
            private set {}
        }

        public Vector3 Position
		{
			get
			{
				return GameObject.transform.position;
			}
			set
			{
				GameObject.transform.position = value;
			}
		}

		public Quaternion Rotation
		{
			get
			{
				return GameObject.transform.rotation;
			}
			set
			{
				GameObject.transform.rotation = value;
			}
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