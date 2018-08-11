using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
	[ObjectSystem]
	public class BoneComponentAwakeSystem : AwakeSystem<BoneComponent>
	{
		public override void Awake(BoneComponent self)
		{
			self.Awake();
		}
	}

	public class BoneComponent : Component
	{
        private enum BoneType{ LeftHandler, RightHandler, LeftFooter, RightFooter, Spine , LeftWeapon, RightWeapon }
        private static string[] Bones = new string[]{ "Bone006", "Bone013", "Bip001 L Foot", "Bip001 R Foot", "Bip001 Spine1", "Bone007", "Bone014" };

        public GameObject GRootBone { get { return gRootBone; } private set { } }
        private GameObject gRootBone;

        public Transform TLeftHandler { get { return tLeftHandler; } private set {  } }
        private Transform tLeftHandler; 
        public Transform TRightHandler { get { return tRightHandler; } private set { } }
        private Transform tRightHandler;
        public Transform TLeftFooter { get { return tLeftFooter; } private set { } }
        private Transform tLeftFooter;
        public Transform TRightFooter { get { return tRightFooter; } private set { } }
        private Transform tRightFooter;
        public Transform TSpine { get { return tLeftHandler; } private set { } }
        private Transform tSpine;
        public Transform TLeftWeapon { get { return tLeftWeapon; } private set { } }
        private Transform tLeftWeapon;
        public Transform TRightWeapon { get { return tRightWeapon; } private set { } }
        private Transform tRightWeapon; 


		public void Awake()
		{
            gRootBone = this.GetParent<Unit>().GameObject;
            tLeftHandler = ETModel.Utility.SearchChildRecurvese(gRootBone.transform, Bones[(int)BoneType.LeftHandler]);
            tRightHandler = ETModel.Utility.SearchChildRecurvese(gRootBone.transform, Bones[(int)BoneType.RightHandler]);
            tLeftFooter =  ETModel.Utility.SearchChildRecurvese(gRootBone.transform, Bones[(int)BoneType.LeftFooter]);
            tRightFooter =  ETModel.Utility.SearchChildRecurvese(gRootBone.transform, Bones[(int)BoneType.RightFooter]);
            tSpine =  ETModel.Utility.SearchChildRecurvese(gRootBone.transform, Bones[(int)BoneType.Spine]);
            tLeftWeapon =  ETModel.Utility.SearchChildRecurvese(gRootBone.transform, Bones[(int)BoneType.LeftWeapon]);
            tRightWeapon = ETModel.Utility.SearchChildRecurvese(gRootBone.transform, Bones[(int)BoneType.RightWeapon]);

        }

	}
}