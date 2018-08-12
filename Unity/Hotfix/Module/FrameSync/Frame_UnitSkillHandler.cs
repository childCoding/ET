using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class Frame_UnitCastItemHandler : AMHandler<Frame_UnitSkillCastItem>
    {
        protected override void Run(ETModel.Session session, Frame_UnitSkillCastItem message)
        {
            Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            switch (message.Index)
            {
                case 0:
                    pushBox(unit);
                    break;
                case 1:
                    helpother(unit);
                    break;
                case 2:
                    changeBody(unit);
                    break;
                case 3:
                    shoot(unit);
                    break;
                case 4:
                    useItem(unit);
                    break;
                case 5:
                    watch(unit);
                    break;
                case 6:
                    getItem(unit);
                    break;
                case 8:
                    bycast(unit);
                    break;
            }

        }

        private void pushBox(Unit unit)
        {
            var animator = unit.GetComponent<AnimatorComponent>();
            var move = unit.GetComponent<MoveComponent>();
            animator.SetTrigger("Attack");
            //ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 1 });
        }
        private void getItem(Unit unit)
        {
            //var animator = unit.GetComponent<AnimatorComponent>();
            //var move = unit.GetComponent<MoveComponent>();
            //move.DashMove();

            MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            Unit other = UnitComponent.Instance.GetAround(unit.Position, 0.2f,unit.Id);
            if (unit != other && other != null)
            {
                moveComponent.Drag(other);
                //var leftweapone = unit.GetComponent<BoneComponent>().TLeftWeapon;
                //other.GameObject.transform.SetParent(leftweapone);
                //other.GameObject.transform.localPosition = Vector3.zero;
            }
        }
        private void changeBody(Unit unit)
        {
            var animator = unit.GetComponent<AnimatorComponent>();
            var move = unit.GetComponent<MoveComponent>();
            move.Jump();
        }
        private void shoot(Unit unit)
        {
            var animator = unit.GetComponent<AnimatorComponent>();
            var move = unit.GetComponent<MoveComponent>();
            move.DashMove();
        }
        private void useItem(Unit unit)
        {
            var animator = unit.GetComponent<AnimatorComponent>();
            var move = unit.GetComponent<MoveComponent>();
           
        }
        private void watch(Unit unit)
        {
            var animator = unit.GetComponent<AnimatorComponent>();
            var move = unit.GetComponent<MoveComponent>();
            move.DashMove();
        }

        private void helpother(Unit unit)
        {
            long id = UnitComponent.Instance.nearDeathUnitId();
            if (id > 0)
            {
                MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
                moveComponent.HelpUnit(id);
            }
        }
        private void drag(Unit unit)
        {
            MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            Unit other = UnitComponent.Instance.GetAround(unit.Position, 0.2f, unit.Id);
            if (unit != other && other != null)
            {
                moveComponent.Drag(other);
            }
            //ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 0 });
        }
        private void bycast(Unit unit)
        {
            var animator = unit.GetComponent<AnimatorComponent>();
            var move = unit.GetComponent<MoveComponent>();
            
        }
    }


    [MessageHandler]
    public class Frame_UnitDragUnitHandler : AMHandler<Frame_UnitDragUnit>
    {
        protected override void Run(ETModel.Session session, Frame_UnitDragUnit message)
        {
            Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            Unit other = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.OtherId);
            if(unit!= null && other != null && UnitComponent.Instance.MyUnit.Id == message.OtherId)
            {
                unit.GetComponent<MoveComponent>().Drag(other);
            }
        }
    }
    [MessageHandler]
    public class Frame_UnitKickUnitHandler : AMHandler<Frame_UnitKickUnit>
    {
        protected override void Run(ETModel.Session session, Frame_UnitKickUnit message)
        {
            Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            Unit other = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.OtherId);
            if (unit!= null && other != null )
            {
                unit.GetComponent<MoveComponent>().PlayAnimation("Attack");
                other.GetComponent<MoveComponent>().ByCast(Utility.UnityVector3FromETVector3(message.Dir));
            }
        }
    }
    [MessageHandler]
    public class Frame_UnitReliveUnitHandler : AMHandler<Frame_UnitReliveUnit>
    {
        protected override void Run(ETModel.Session session, Frame_UnitReliveUnit message)
        {
            //Unit unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.Id);
            Unit other = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(message.OtherId);
            if (other != null)
            {
                other.GetComponent<MoveComponent>().Relive();
            }
        }
    }
}
