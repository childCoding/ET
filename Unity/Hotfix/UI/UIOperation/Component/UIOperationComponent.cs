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

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            GameObject NormalSkill = rc.Get<GameObject>("NormalSkill");
            GameObject NormalSkill0 = rc.Get<GameObject>("NormalSkill0");
            GameObject NormalSkill1 = rc.Get<GameObject>("NormalSkill1");
            GameObject NormalSkill2 = rc.Get<GameObject>("NormalSkill2");

            NormalSkill.GetComponent<Button>().onClick.Add(this.OnNormalSkill);
            NormalSkill0.GetComponent<Button>().onClick.Add(this.OnNormalSkill0);
            NormalSkill1.GetComponent<Button>().onClick.Add(this.OnNormalSkill1);
            NormalSkill2.GetComponent<Button>().onClick.Add(this.OnNormalSkill2);
  


            joystick = rc.Get<GameObject>("Joystick").GetComponent<Joystick>();
            //unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(UnitComponent.Instance.MyUnit.Id);
            this.WeakScore = rc.Get<GameObject>("Weak").GetComponent<Text>();
            this.StrongScore = rc.Get<GameObject>("Strong").GetComponent<Text>();
        }
        public void SetUnit(Unit u)
        {
            Unit = u;
            MoveC = Unit.GetComponent<MoveComponent>();
        }
        private bool IsStop = true;
        private float DetlaTime;
        public void Update()
        {
            if (MoveC != null && MoveC.IsByDrag)
                return;
            DetlaTime += Time.deltaTime;
            if (DetlaTime < 0.1f)
                return;
            DetlaTime = 0;

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
                ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitMoveStop() { Pos = ETModel.Utility.ETVector3FromUnityVector3(pos),Dir = ETModel.Utility.ETVector3FromUnityVector3(dir) });
            }

            //Log.Debug($"dir : {moveVector} {Time.frameCount}");
        }

        private void OnNormalSkill()
        {
            ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 0   });
        }
        private void OnNormalSkill0()
        {
            ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 1   });
        }
        private void OnNormalSkill1()
        {
            ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 2 });
        }
        private void OnNormalSkill2()
        {
            ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitSkillCastItem() { Index = 3 });
        }
    }
}
