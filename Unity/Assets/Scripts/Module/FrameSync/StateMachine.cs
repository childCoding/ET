using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
    public class IState<T> where T:new()
    {
        public T Entity;
        public StateMachineComponent.States ID;
        public IState(T entity,StateMachineComponent.States id ) { ID = id; Entity = entity; }
        public virtual StateMachineComponent.States Update() { return ID; }
        public virtual void Enter(IState<T> next) { }
        public virtual void Leave(IState<T> Per) { }
    }
    public class Move : IState<MoveComponent>
    {
        public Move(MoveComponent entity) : base(entity,StateMachineComponent.States.Move) {

        }
        public override StateMachineComponent.States Update() {
            if (Entity.ByCastTrigger)
                return StateMachineComponent.States.ByCast;
            if (Entity.JumpTrigger)
                return StateMachineComponent.States.Jump;
            if (Entity.DashTrigger)
                return StateMachineComponent.States.DashMove;
            if (!Entity.IsNeedMove())
                return StateMachineComponent.States.Idle;

            var dir = (Entity.Dest - Entity.Position).normalized;
            var motion = dir * Entity.MoveSpeed * Time.deltaTime;
            Entity.SetMotion(motion.x, motion.z);

            return ID;
        }
        public override void Enter(IState<MoveComponent> next) {
            Entity.PlayAnimation("Run");
        }
        public override void Leave(IState<MoveComponent> Per) { }
    }
    public class Jump : IState<MoveComponent>
    {
        private float t = 0;
        public Jump(MoveComponent entity) : base(entity, StateMachineComponent.States.Jump) {
           
        }
        public override StateMachineComponent.States Update() {

            var dir = (Entity.Dest - Entity.Position).normalized;
            var motion = dir * Entity.MoveSpeed * Time.deltaTime;
            Entity.SetMotion(motion.x, motion.z);

            t += Time.deltaTime;
            var v = Entity.JumpSpeed + Entity.Gravity * t;
            Entity.SetMotion(v);
            if (v < 0)
                return StateMachineComponent.States.Fall;
            return ID;
        }
        public override void Enter(IState<MoveComponent> next) {  t = 0; Entity.PlayAnimation("Run"); }
        public override void Leave(IState<MoveComponent> Per) { }
    }
    public class Idle : IState<MoveComponent>
    {
        public Idle(MoveComponent entity) : base(entity, StateMachineComponent.States.Idle) { }
        public override StateMachineComponent.States Update() {
            if (Entity.ByCastTrigger)
                return StateMachineComponent.States.ByCast;
            if (Entity.JumpTrigger)
                return StateMachineComponent.States.Jump;
            if (Entity.DashTrigger)
                return StateMachineComponent.States.DashMove;
            if (Entity.IsNeedMove())
                return StateMachineComponent.States.Move;
            if (Entity.HelpTrigger)
                return StateMachineComponent.States.Help;

            return ID;
        }
        public override void Enter(IState<MoveComponent> next) {
            Entity.PlayAnimation(ID.ToString());
            Entity.SetMotion(Vector3.zero);
        }
        public override void Leave(IState<MoveComponent> Per) { }
    }
    public class Attack : IState<MoveComponent>
    {
        public Attack(MoveComponent entity) : base(entity, StateMachineComponent.States.Attack) { }
        public override StateMachineComponent.States Update() { return ID; }
        public override void Enter(IState<MoveComponent> next) {
            Entity.PlayAnimation(ID.ToString());
        }
        public override void Leave(IState<MoveComponent> Per) { }
    }
    public class Fall : IState<MoveComponent>
    {
        //private float v = 0;
        private float t = 0;
        public Fall(MoveComponent entity) : base(entity, StateMachineComponent.States.Fall) {  }
        public override StateMachineComponent.States Update()
        {
            var dir = (Entity.Dest - Entity.Position).normalized;
            var motion = dir * Entity.MoveSpeed * Time.deltaTime;
            Entity.SetMotion(motion.x, motion.z);

            t += Time.deltaTime;
            var v = Entity.Gravity * t;
            Entity.SetMotion(v);
            if (Entity.IsOnGround())
            {
                if(Entity.IsNeedMove())
                    return StateMachineComponent.States.Move;
                return StateMachineComponent.States.Idle;
            }
                
            return ID;
        }
        public override void Enter(IState<MoveComponent> next) {  t = 0; }
        public override void Leave(IState<MoveComponent> Per) { }
    }

    public class DashMove : IState<MoveComponent>
    {
        private float t = 0;
        public DashMove(MoveComponent entity) : base(entity, StateMachineComponent.States.Move)
        {

        }
        public override StateMachineComponent.States Update()
        {
            t += Time.deltaTime;
            if (Entity.ByCastTrigger)
                return StateMachineComponent.States.ByCast;
            if (Entity.JumpTrigger)
                return StateMachineComponent.States.Jump;
            //if (Entity.DashTrigger)
            //    return StateMachineComponent.States.DashMove;
            if (!Entity.IsNeedMove())
                return StateMachineComponent.States.Idle;
            if (t > Entity.DashMoveDurationTime)
                return StateMachineComponent.States.Move;

            var dir = (Entity.Dest - Entity.Position).normalized;
            var motion = dir * Entity.DashMoveSpeed * Time.deltaTime;
            Entity.SetMotion(motion.x, motion.z);

            return ID;
        }
        public override void Enter(IState<MoveComponent> next)
        {
            t = 0;
            Entity.PlayAnimation("Run");
            Entity.SetAnimationSpeed(Entity.DashMoveAnimationSpeed);
        }
        public override void Leave(IState<MoveComponent> Per)
        {
            Entity.SetAnimationSpeed(1);
        }
    }
    public class Death : IState<MoveComponent>
    {
        private float t = 0;
        public Death(MoveComponent entity) : base(entity, StateMachineComponent.States.Death)
        {

        }
        public override StateMachineComponent.States Update()
        {
            if (Entity.ReliveTrigger)
                return StateMachineComponent.States.Idle;
            return ID;
        }
        public override void Enter(IState<MoveComponent> next)
        {
            Entity.PlayAnimation(ID.ToString());
        }
        public override void Leave(IState<MoveComponent> Per)
        {
        }
    }
    public class Drag : IState<MoveComponent>
    {
        public Drag(MoveComponent entity) : base(entity, StateMachineComponent.States.Drag)
        {

        }
        public override StateMachineComponent.States Update()
        {
            if (Entity.CastTrigger)
                return StateMachineComponent.States.Cast;

            return ID;
        }
        public override void Enter(IState<MoveComponent> next)
        {
            Entity.PlayAnimation(ID.ToString());
        }
        public override void Leave(IState<MoveComponent> Per)
        {
        }
    }
    public class ByDrag : IState<MoveComponent>
    {
        public ByDrag(MoveComponent entity) : base(entity, StateMachineComponent.States.ByDrag)
        {

        }
        public override StateMachineComponent.States Update()
        {
            return ID;
        }
        public override void Enter(IState<MoveComponent> next)
        {
            Entity.PlayAnimation(ID.ToString());
        }
        public override void Leave(IState<MoveComponent> Per)
        {
        }
    }
    public class Cast : IState<MoveComponent>
    {
        public Cast(MoveComponent entity) : base(entity, StateMachineComponent.States.Cast)
        {

        }
        public override StateMachineComponent.States Update()
        {
            return ID;
        }
        public override void Enter(IState<MoveComponent> next)
        {
            Entity.PlayAnimation(ID.ToString());
        }
        public override void Leave(IState<MoveComponent> Per)
        {
        }
    }
    public class ByCast : IState<MoveComponent>
    {
        private float t = 0;
        private float v = 0;
        private Vector3 dir = Vector3.zero;
        public ByCast(MoveComponent entity) : base(entity, StateMachineComponent.States.ByCast)
        {

        }
        public override StateMachineComponent.States Update()
        {
            t += Time.deltaTime;
            var vv = dir * Entity.ByCastSpeed * Time.deltaTime;
            Entity.SetMotion(vv.x, vv.z);

            v += Entity.Gravity * Time.deltaTime;
            Entity.SetMotion(v);
            if (t > 1.0f && Entity.IsOnGround())
                return StateMachineComponent.States.Death;
            return ID;
        }
        public override void Enter(IState<MoveComponent> next)
        {
            t = 0;
            dir = Entity.ByCastDirection;
            dir.y = 0;dir.Normalize();
            v = 0.1f;
            Entity.PlayAnimation("Run");
        }
        public override void Leave(IState<MoveComponent> Per)
        {
            Entity.ByCastDirection = Vector3.zero;
            Entity.SetMotion(Vector3.zero);
        }
    }
    public class Help : IState<MoveComponent>
    {
        private float t = 0;
        public Help(MoveComponent entity) : base(entity, StateMachineComponent.States.Help)
        {

        }
        public override StateMachineComponent.States Update()
        {
            t += Time.deltaTime;
            if (t > Entity.HelpDurationTime)
                return StateMachineComponent.States.Idle;

            if (Entity.ByCastTrigger)
                return StateMachineComponent.States.ByCast;
            if (Entity.IsNeedMove())
                return StateMachineComponent.States.Move;
            return ID;
        }
        public override void Enter(IState<MoveComponent> next)
        {
            t = 0;
            Entity.PlayAnimation(ID.ToString());
        }
        public override void Leave(IState<MoveComponent> Per)
        {
            if (t > Entity.HelpDurationTime)
            {
                ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitReliveUnit() { OtherId = Entity.HelpUnitID });
            }
            Entity.HelpUnitID = 0;
        }
    }
}