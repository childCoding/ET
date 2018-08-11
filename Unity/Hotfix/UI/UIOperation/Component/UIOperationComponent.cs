using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIOperationComponentAwakeSystem : AwakeSystem<UIOperationComponent>
    {
        public override void Awake(UIOperationComponent self)
        {
            self.Awake();
        }
    }
    [ObjectSystem]
    public class UIOperationComponentUpdateSystem : UpdateSystem<UIOperationComponent>
    {
        public override void Update(UIOperationComponent self)
        {
            self.Update();
        }
    }

    public class UIOperationComponent : Component
    {
        private Joystick joystick = null;
        private Unit Unit = null;
        private MoveComponent MoveC = null;
        // 弱势方积分
        public Text WeakScore { get; set; }
        // 强势方积分
        public Text StrongScore { get; set; }

        GameObject spitButton = null;

        GameObject shootButton = null;
        GameObject getItemButton = null;
        GameObject watchButton = null;
        GameObject helpotherButton = null;
        GameObject changeBodyButton = null;
        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            GameObject pushBoxButton = rc.Get<GameObject>("NormalSkill");
            spitButton = rc.Get<GameObject>("NormalSkill0");
            this.spitButton.gameObject.SetActive(false);
            changeBodyButton = rc.Get<GameObject>("NormalSkill1");
            shootButton = rc.Get<GameObject>("NormalSkill2");
            GameObject useItemButton = rc.Get<GameObject>("NormalSkill3");
            watchButton = rc.Get<GameObject>("NormalSkill4");
            getItemButton = rc.Get<GameObject>("NormalSkill5");
            helpotherButton = rc.Get<GameObject>("helpother");

            pushBoxButton.gameObject.SetActive(false);
            pushBoxButton.GetComponent<Button>().onClick.Add(this.pushBox);
            changeBodyButton.gameObject.SetActive(true);
            changeBodyButton.GetComponent<Button>().onClick.Add(this.changeBody);
            shootButton.gameObject.SetActive(false);
            shootButton.GetComponent<Button>().onClick.Add(this.shoot);
            useItemButton.gameObject.SetActive(false);
            useItemButton.GetComponent<Button>().onClick.Add(this.useItem);
            watchButton.gameObject.SetActive(false);
            watchButton.GetComponent<Button>().onClick.Add(this.watch);
            getItemButton.gameObject.SetActive(false);
            getItemButton.GetComponent<Button>().onClick.Add(this.dragcast);
            helpotherButton.gameObject.SetActive(false);
            helpotherButton.GetComponent<Button>().onClick.Add(this.helpother);

            joystick = rc.Get<GameObject>("Joystick").GetComponent<Joystick>();
            this.WeakScore = rc.Get<GameObject>("Weak").GetComponent<Text>();
            this.StrongScore = rc.Get<GameObject>("Strong").GetComponent<Text>();
        }
        public void SetUnit(Unit u)
        {
            Unit = u;
            MoveC = Unit.GetComponent<MoveComponent>();
            if (Unit.UnitType == UnitType.Weak)
            {
                this.spitButton.gameObject.SetActive(true);
                spitButton.GetComponent<Button>().onClick.Add(this.spit);

                this.shootButton.gameObject.SetActive(true);
            }
            else
            {
                getItemButton.gameObject.SetActive(true);
            }
        }
        private bool IsStop = true;
        private float DetlaTime;
        public void Update()
        {
            if (MoveC != null && !MoveC.CanControl)
                return;
            DetlaTime += Time.deltaTime;
            if (DetlaTime < 0.1f)
                return;
            DetlaTime = 0;


            long id = UnitComponent.Instance.nearDeathUnitId();
            helpotherButton.gameObject.SetActive(id > 0);

            Vector3 moveVector = joystick.WorldDirection;// Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical;
            moveVector.y = 0;
            if (moveVector.magnitude > 0.1f)
            {
                moveVector *= 8;
                //moveVector.Normalize();
                IsStop = false;
                Vector3 pos = Unit.Position;
                ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitMove() { Dir = ETModel.Utility.ETVector3FromUnityVector3(moveVector), Pos = ETModel.Utility.ETVector3FromUnityVector3(pos) });
            }
            else if (!IsStop)
            {
                IsStop = true;
                Vector3 dir = Unit.Rotation.eulerAngles;
                Vector3 pos = Unit.Position;
                ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitMoveStop() { Pos = ETModel.Utility.ETVector3FromUnityVector3(pos), Dir = ETModel.Utility.ETVector3FromUnityVector3(dir) });
            }

            //Log.Debug($"dir : {moveVector} {Time.frameCount}");
        }
        // 推箱子
        private void pushBox()
        {
            ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 0 });
        }
        // 吐豆子
        private void spit()
        {
            if (this.Unit.Score >= 2)
            {
                ETModel.SessionComponent.Instance.Session.Send(new C2G_SpitScore() { Id = this.Unit.Id });
            }
        }
        // 变身
        private void changeBody()
        {
            ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 2 });
        }
        // 加速
        private void shoot()
        {
            ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 3 });
        }
        // 使用道具
        private void useItem()
        {
            ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 4 });
        }
        // 观察
        private void watch()
        {
            //ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 5 });
        }
        // 拾取道具
        private void dragcast()
        {
            MoveComponent moveComponent = Unit.GetComponent<MoveComponent>(); 
            Unit other = UnitComponent.Instance.GetForwardUnit(Unit.Position, 0.2f, Unit);
            //Unit other = UnitComponent.Instance.GetAround(Unit.Position, 0.2f, Unit.Id);
            if ( other != null)
            {
                //moveComponent.Drag(other);
                ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitKickUnit() { OtherId = other.Id, Dir = Utility.ETVector3FromUnityVector3(Unit.Transform.forward) });
            }            
        }
        private void helpother()
        {
            ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 1 });
        }
    }
}
