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
        private Unit unit = null;
        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            GameObject NormalSkill = rc.Get<GameObject>("NormalSkill");
            //GameObject sendRpcBtn = rc.Get<GameObject>("" + "SendRpc");
            NormalSkill.GetComponent<Button>().onClick.Add(this.OnNormalSkill);
            //sendRpcBtn.GetComponent<Button>().onClick.Add(this.OnSendRpc);


            joystick = rc.Get<GameObject>("Joystick").GetComponent<Joystick>();
            unit = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(PlayerComponent.Instance.MyPlayer.UnitId);
        }
        public void Update()
        {
            Vector3 moveVector = Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical;
            if (moveVector.magnitude > 0.1f)
            {
                Log.Debug($"dir : {moveVector}");
                SessionComponent.Instance.Session.Send(new Frame_UnitMove() { Dir = ETModel.Utility.ETVector3FromUnityVector3(moveVector) });
            }
        }
        
        private void OnNormalSkill()
        {
            
        }
    }
}
