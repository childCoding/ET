using UnityEngine;

namespace ETModel
{
	[ObjectSystem]
	public class CameraComponentAwakeSystem : AwakeSystem<CameraComponent, Unit>
	{
		public override void Awake(CameraComponent self, Unit unit)
		{
			self.Awake(unit);
		}
	}

	[ObjectSystem]
	public class CameraComponentLateUpdateSystem : LateUpdateSystem<CameraComponent>
	{
		public override void LateUpdate(CameraComponent self)
		{
			self.LateUpdate();
		}
	}

	public class CameraComponent : Component
	{
		// 战斗摄像机
		public Camera mainCamera;

        public Unit Unit;

		public Camera MainCamera
		{
			get
			{
				return this.mainCamera;
			}
		}

		public void Awake(Unit unit)
		{
			this.mainCamera = Camera.main;
            this.Unit = unit;
        }

		public void LateUpdate()
		{
			// 摄像机每帧更新位置
			UpdatePosition();
		}

		private void UpdatePosition()
		{
			Vector3 cameraPos = this.mainCamera.transform.position;
			this.mainCamera.transform.position = new Vector3(this.Unit.Position.x, cameraPos.y, this.Unit.Position.z - 1);
        }
	}
}
