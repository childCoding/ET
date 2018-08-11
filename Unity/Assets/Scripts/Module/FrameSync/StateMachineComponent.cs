using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
	[ObjectSystem]
	public class StateMachineComponentAwakeSystem : AwakeSystem<StateMachineComponent>
	{
		public override void Awake(StateMachineComponent self)
		{
			self.Awake();
		}
	}

	[ObjectSystem]
	public class StateMachineComponentUpdateSystem : UpdateSystem<StateMachineComponent>
	{
		public override void Update(StateMachineComponent self)
		{
			self.Update();
		}
	}

	public class StateMachineComponent : Component
	{
        public enum States { Idle ,Move,Jump,Fall,Attack, DashMove }
        public Dictionary<States, IState<MoveComponent>> states = new Dictionary<States, IState<MoveComponent>>();
        public IState<MoveComponent> curState = null;
        public MoveComponent moveComponent = null;
		public void Awake()
		{
            moveComponent = GetParent<Unit>().GetComponent<MoveComponent>();
            states[States.Idle] = new Idle(moveComponent);
            states[States.Move] = new Move(moveComponent);
            states[States.Jump] = new Jump(moveComponent);
            states[States.Fall] = new Fall(moveComponent);
            states[States.Attack] = new Attack(moveComponent);
            states[States.DashMove] = new DashMove(moveComponent);
            curState = states[States.Idle];
            
        }

		public void Update()
		{
            if (curState.ID != States.Jump && curState.ID != States.Fall)
                moveComponent.SetMotion(-1);

            if (curState != null)
            {
                var id = curState.Update();
                if (curState.ID != id)
                {
                    curState.Leave(states[id]);
                    states[id].Enter(curState);
                    curState = states[id];
                }
            }
            moveComponent.JumpTrigger = false;
            moveComponent.DashTrigger = false;
        }

        public void Drag(Unit attach)
        {
            if(attach != null)
            {

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