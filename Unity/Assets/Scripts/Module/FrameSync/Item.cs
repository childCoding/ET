﻿using UnityEngine;

namespace ETModel
{
	public sealed class Item: Entity
	{
		public GameObject GameObject;
        // 归属
        public long Owner { get; set; }
        // 数量
        public int Number { get; set; }
		
		public void Awake()
		{
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