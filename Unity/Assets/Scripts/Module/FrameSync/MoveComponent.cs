using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
	[ObjectSystem]
	public class MoveComponentAwakeSystem : AwakeSystem<MoveComponent>
	{
		public override void Awake(MoveComponent self)
		{
			self.Awake();
		}
	}

	[ObjectSystem]
	public class MoveComponentUpdateSystem : UpdateSystem<MoveComponent>
	{
		public override void Update(MoveComponent self)
		{
			self.Update();
		}
	}

	public class Speed
	{
		public long Id;

		public Vector3 Value;

		public Speed()
		{
		}

		public Speed(long id)
		{
			this.Id = id;
		}
	}

	public class MoveComponent : Component
	{
        private enum State { Moving ,Idle,ByDrag, ByCasting }
        private State UnitState = State.Idle;
		private AnimatorComponent animatorComponent;
        private CharacterController characterComponent;

		public long mainSpeed;
		public Dictionary<long, Speed> speeds = new Dictionary<long, Speed>();

		// turn
		public Quaternion To;
		public Quaternion From;
		public float t = float.MaxValue;
		public float TurnTime = 0.1f;

		public bool IsArrived { get; private set; } = true;


		public bool hasDest;
		public Vector3 Dest;

		public Vector3 MainSpeed
		{
			get
			{
				return m_Speed;
			}
			set
			{
                m_Speed = value;
				animatorComponent?.SetFloatValue("Speed", m_Speed.magnitude);
			}
		}

        public Vector3 m_Speed;
 

		public void Awake()
		{ 
			this.mainSpeed = this.AddSpeed(new Vector3());
			this.animatorComponent = this.Entity.GetComponent<AnimatorComponent>();
            this.characterComponent = this.GetParent<Unit>().GameObject.GetComponent<CharacterController>();

        }

		public void Update()
		{
			UpdateTurn();


			Unit unit = this.GetParent<Unit>();
			Vector3 moveVector3 = this.m_Speed * Time.deltaTime;

            moveVector3 += animatorComponent.Animator ? animatorComponent.Animator.deltaPosition:Vector3.zero;
            if (!characterComponent.isGrounded)
            {
                moveVector3.y += -0.5f;
            }
            //unit.Position = unit.Position + moveVector3;
            if(moveVector3.magnitude > 0.001f)
                characterComponent.Move(moveVector3);

        }
        #region 移动
        private void UpdateTurn()
		{
			//Log.Debug($"update turn: {this.t} {this.TurnTime}");
			if (this.t > this.TurnTime)
			{
				return;
			}

			this.t += Time.deltaTime;

			Quaternion v = Quaternion.Slerp(this.From, this.To, this.t / this.TurnTime);
			this.GetParent<Unit>().Rotation = v;
		}

		public void MoveToDest(Vector3 dest, float speedValue)
		{
			if ((dest - this.GetParent<Unit>().Position).magnitude < 0.1f)
			{
				this.IsArrived = true;
				return;
			}
			this.IsArrived = false;
			this.hasDest = true;
			Vector3 speed = dest - this.GetParent<Unit>().Position;
			speed = speed.normalized * speedValue;
			this.MainSpeed = speed;
			this.Dest = dest;

            UnitState = State.Moving;
        }

		public void MoveToDir(Vector3 dir)
		{
			this.IsArrived = false;
			this.hasDest = false;
			this.MainSpeed = dir;
            UnitState = State.Moving;
        }

		public long AddSpeed(Vector3 spd)
		{
			Speed speed = new Speed() { Value = spd };
			this.speeds.Add(speed.Id, speed);
			return speed.Id;
		}

		public Vector3 GetSpeed(long id)
		{
			return m_Speed;
		}

		public void RemoveSpeed(long id)
		{

		}

		/// <summary>
		/// 停止移动Unit,只停止地面正常移动,不停止击飞等移动
		/// </summary>
		public void Stop()
		{
			this.m_Speed = Vector3.zero;
			this.animatorComponent?.SetFloatValue("Speed", 0);
            //this.animatorComponent?.Play(MotionType.Idle);
            UnitState = State.Idle;
        }
        /// <summary>
        /// 停止移动Unit,只停止地面正常移动,不停止击飞等移动
        /// </summary>
        public void Jump()
        {
            MainSpeed += Vector3.up * 20;
            //this.speeds.Clear();
            //this.animatorComponent?.SetFloatValue("Speed", 0);
            //this.animatorComponent?.Play(MotionType.Idle);
            //UnitState = State.Moving;
        }
        /// <summary>
        /// 改变Unit的朝向
        /// </summary>
        public void Turn2D(Vector3 dir, float turnTime = 0.1f)
		{
			Vector3 nexpos = this.GetParent<Unit>().GameObject.transform.position + dir;
			Turn(nexpos, turnTime);
		}

		/// <summary>
		/// 改变Unit的朝向
		/// </summary>
		public void Turn(Vector3 target, float turnTime = 0.1f)
		{
			Quaternion quaternion = PositionHelper.GetVector3ToQuaternion(this.GetParent<Unit>().Position, target);

			this.To = quaternion;
			this.From = this.GetParent<Unit>().Rotation;
			this.t = 0;
			this.TurnTime = turnTime;
		}

		/// <summary>
		/// 改变Unit的朝向
		/// </summary>
		/// <param name="angle">与X轴正方向的夹角</param>
		public void Turn(float angle, float turnTime = 0.1f)
		{
			Quaternion quaternion = PositionHelper.GetAngleToQuaternion(angle);

			this.To = quaternion;
			this.From = this.GetParent<Unit>().Rotation;
			this.t = 0;
			this.TurnTime = turnTime;
		}

		public void Turn(Quaternion quaternion, float turnTime = 0.1f)
		{
			this.To = quaternion;
			this.From = this.GetParent<Unit>().Rotation;
			this.t = 0;
			this.TurnTime = turnTime;
		}

		public void TurnImmediately(Quaternion quaternion)
		{
			this.GetParent<Unit>().Rotation = quaternion;
		}

		public void TurnImmediately(Vector3 target)
		{
			Vector3 nowPos = this.GetParent<Unit>().Position;
			if (nowPos == target)
			{
				return;
			}

			Quaternion quaternion = PositionHelper.GetVector3ToQuaternion(this.GetParent<Unit>().Position, target);
			this.GetParent<Unit>().Rotation = quaternion;
		}

		public void TurnImmediately(float angle)
		{
			Quaternion quaternion = PositionHelper.GetAngleToQuaternion(angle);
			this.GetParent<Unit>().Rotation = quaternion;
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();
		}
#endregion


        #region 技能
        /// <summary>
        /// 拾取对象
        /// </summary>
        /// <param name="u"></param>
        public void Drag(Unit other)
        {
            Unit unit = this.GetParent<Unit>();
            if (unit != other && other != null)
            {
                var leftweapone = unit.GetComponent<BoneComponent>().TLeftWeapon;
                other.GameObject.transform.SetParent(leftweapone);
                other.GameObject.transform.localPosition = Vector3.zero;
            }
        }
        //private 
        public bool IsByDrag { get { return UnitState == State.ByDrag; } }
        #endregion
    }
}