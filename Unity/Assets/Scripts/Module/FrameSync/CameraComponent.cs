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
            var dir = Quaternion.Euler( m_y,m_x, 0) * Vector3.forward;
            dir *= m_disttance;
            this.mainCamera.transform.position = Unit.Position + dir;

            //var pos = Unit.Transform.position - this.Unit.Transform.forward * 0.5f + this.Unit.Transform.up * 0.2f;
            //this.mainCamera.transform.position = pos;
            this.mainCamera.transform.LookAt(Unit.Position);
        }
        private float m_x = -45;
        private float m_y = -60;
        private float m_disttance = 2f;
        // 摄像机旋转
        public void UpdateRotation(Vector3 v)
        {
            m_y += v.y;
            m_y = ClampAngle(m_y, -85, 85); ;

            m_x += v.x;
            m_x = ClampAngle(m_x, -360, 360); ;
            //Log.Debug($"{m_y} {m_x} {v}");
            //this.mainCamera.transform.Rotate(vector.y, vector.x, 0);
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360) angle += 360;
            if (angle > 360) angle -= 360;
            return Mathf.Clamp(angle, min, max);
        }

    }
}
