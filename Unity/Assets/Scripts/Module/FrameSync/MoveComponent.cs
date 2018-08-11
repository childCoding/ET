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


	public class MoveComponent : Component
	{
        //private enum State { Moving ,Idle,ByDrag, ByCasting }
        //private State UnitState = State.Idle;
		private AnimatorComponent animatorComponent;
        private CharacterController characterComponent;
        private StateMachineComponent stateMachineComponent;
        private bool m_CanControl = true;
        public bool CanControl {
            get {
                if (stateMachineComponent == null)
                    stateMachineComponent = GetParent<Unit>().GetComponent<StateMachineComponent>();
                if (stateMachineComponent.curState.ID == StateMachineComponent.States.ByCast ||
                    stateMachineComponent.curState.ID == StateMachineComponent.States.ByDrag ||
                    stateMachineComponent.curState.ID == StateMachineComponent.States.Death  )
                    return false;
                return m_CanControl;
            }
        }

        public bool DragTrigger = false;
        public bool ByDragTrigger = false;

        public bool CastTrigger = false;
        public bool ByCastTrigger = false;
        public Vector3 ByCastDirection = Vector3.zero;
        public float ByCastSpeed = 8.0f;

        public bool JumpTrigger = false;
        public bool DashTrigger = false;

        public float Gravity = -0.5f;
        public float JumpSpeed = 0.1f;
        public float MoveSpeed = 2.0f;

        public float DashMoveSpeed = 8.0f;
        public float DashMoveAnimationSpeed = 4.0f;
        public float DashMoveDurationTime = 3.0f;

        public bool ReliveTrigger = false;

        public bool HelpTrigger = false;
        public long HelpUnitID = 0;
        public long HelpDurationTime = 5;


        public Vector3 Direction { get { return this.GetParent<Unit>().Transform.forward; } }
        public Vector3 Position = Vector3.zero;
        public Vector3 Dest = Vector3.zero;

        public bool IsNeedMove()
        {
            if ((Dest - Position).magnitude > 0.1f)
                return true;
            return false;
        }

        public long mainSpeed;

		// turn
		public Quaternion To;
		public Quaternion From;
		public float t = float.MaxValue;
		public float TurnTime = 0.1f;

		public bool IsArrived { get; private set; } = true;

        public bool IsOnGround()
        {
            return characterComponent.isGrounded;
        }
        public void PlayAnimation(string name,float normalizeTime = 0.1f)
        {
            animatorComponent?.Animator.Play(name, 0, normalizeTime);
        }
        public void SetAnimationSpeed(float v)
        {
            if (animatorComponent != null)
            {
                animatorComponent.Animator.speed = v;
            }  
        }
        public bool hasDest;
		//public Vector3 Dest;

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

			this.animatorComponent = this.Entity.GetComponent<AnimatorComponent>();
            this.characterComponent = this.GetParent<Unit>().GameObject.GetComponent<CharacterController>();

        }
        private Vector3 motion = Vector3.zero;
        public void SetMotion(float x,float z) { motion.x = x;motion.z = z; }
        public void SetMotion(float y) { motion.y = y; }
        public void SetMotion(Vector3 m) { motion = m; }

		public void Update()
		{

            //JumpTrigger = false;
            //DashTrigger = false;

            UpdateTurn();

            characterComponent.Move(motion);

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

            //UnitState = State.Moving;
        }

		public void MoveToDir(Vector3 dir)
		{
            if (!CanControl)
                return;

            Turn2D(dir);
            Dest = Position + dir * 5f;

        }


		/// <summary>
		/// 停止移动Unit,只停止地面正常移动,不停止击飞等移动
		/// </summary>
		public void Stop()
		{
            if (!CanControl)
                return;
            Dest = Position;

        }
        /// <summary>
        /// 停止移动Unit,只停止地面正常移动,不停止击飞等移动
        /// </summary>
        public void Jump()
        {
            if (!CanControl)
                return;
            JumpTrigger = true;
        }

        public void DashMove()
        {
            if (!CanControl)
                return;
            DashTrigger = true;
            Dest = Position + Direction * 2;
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
                var leftweapone = unit.GetComponent<BoneComponent>().TRightWeapon;
                other.GameObject.transform.SetParent(leftweapone);
                other.GameObject.transform.localPosition = Vector3.zero;
                AttachObject = unit;
                DragTrigger = true;
            }
        }
        private Unit AttachObject = null;
        public void Cast()
        {
            if(AttachObject!= null)
            {
                ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 8 });
            }
        }
        public void ByCast(Vector3 dir)
        {
            ByCastTrigger = true;
            ByCastDirection = dir;
        }
        public void HelpUnit(long id)
        {
            HelpTrigger = true;
            HelpUnitID = id;
        }
        public void Relive()
        {
            ReliveTrigger = true;
        }
        //private 
        //public bool IsByDrag { get { return UnitState == State.ByDrag; } }
        #endregion
    }
}