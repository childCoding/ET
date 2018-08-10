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
        public Unit Unit = null;
        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            GameObject NormalSkill = rc.Get<GameObject>("NormalSkill");
            //GameObject sendRpcBtn = rc.Get<GameObject>("" + "SendRpc");
            NormalSkill.GetComponent<Button>().onClick.Add(this.OnNormalSkill);
            //sendRpcBtn.GetComponent<Button>().onClick.Add(this.OnSendRpc);


            joystick = rc.Get<GameObject>("Joystick").GetComponent<Joystick>();
            //unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(UnitComponent.Instance.MyUnit.Id);
        }
        private bool IsStop = true;
        private float DetlaTime;
        public void Update()
        {
            DetlaTime += Time.deltaTime;
            if (DetlaTime < 0.1f)
                return;
            DetlaTime = 0;

            Vector3 moveVector = Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical;
            if (moveVector.magnitude > 0.1f)
            {
                IsStop = false;
                ETModel.SessionComponent.Instance.Session.Send(new Frame_UnitMove() { Dir = ETModel.Utility.ETVector3FromUnityVector3(moveVector) });
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
            
        }
    }
}
