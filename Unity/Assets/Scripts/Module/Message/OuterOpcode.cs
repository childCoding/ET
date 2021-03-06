using ETModel;
namespace ETModel
{
	[Message(OuterOpcode.Actor_Test)]
	public partial class Actor_Test : IActorMessage {}

	[Message(OuterOpcode.Actor_TestRequest)]
	public partial class Actor_TestRequest : IActorRequest {}

	[Message(OuterOpcode.Actor_TestResponse)]
	public partial class Actor_TestResponse : IActorResponse {}

	[Message(OuterOpcode.Actor_TransferRequest)]
	public partial class Actor_TransferRequest : IActorRequest {}

	[Message(OuterOpcode.Actor_TransferResponse)]
	public partial class Actor_TransferResponse : IActorResponse {}

	[Message(OuterOpcode.C2G_EnterMap)]
	public partial class C2G_EnterMap : IRequest {}

	[Message(OuterOpcode.G2C_EnterMap)]
	public partial class G2C_EnterMap : IResponse {}

	[Message(OuterOpcode.UnitInfo)]
	public partial class UnitInfo {}

	[Message(OuterOpcode.Actor_CreateUnits)]
	public partial class Actor_CreateUnits : IActorMessage {}

	[Message(OuterOpcode.Frame_ClickMap)]
	public partial class Frame_ClickMap : IFrameMessage {}

	[Message(OuterOpcode.Frame_UnitMove)]
	public partial class Frame_UnitMove : IFrameMessage {}

	[Message(OuterOpcode.Frame_UnitMoveStop)]
	public partial class Frame_UnitMoveStop : IFrameMessage {}

	[Message(OuterOpcode.Frame_UnitRotation)]
	public partial class Frame_UnitRotation : IFrameMessage {}

	[Message(OuterOpcode.Frame_UnitSkillCastItem)]
	public partial class Frame_UnitSkillCastItem : IFrameMessage {}

	[Message(OuterOpcode.Frame_UnitDragUnit)]
	public partial class Frame_UnitDragUnit : IFrameMessage {}

	[Message(OuterOpcode.Frame_UnitKickUnit)]
	public partial class Frame_UnitKickUnit : IFrameMessage {}

	[Message(OuterOpcode.Frame_UnitReliveUnit)]
	public partial class Frame_UnitReliveUnit : IFrameMessage {}

	[Message(OuterOpcode.C2R_Ping)]
	public partial class C2R_Ping : IRequest {}

	[Message(OuterOpcode.R2C_Ping)]
	public partial class R2C_Ping : IResponse {}

	[Message(OuterOpcode.G2C_Test)]
	public partial class G2C_Test : IMessage {}

// 道具信息
	[Message(OuterOpcode.ItemInformation)]
	public partial class ItemInformation {}

	[Message(OuterOpcode.Actor_CreateItems)]
	public partial class Actor_CreateItems : IActorMessage {}

// 删除道具
	[Message(OuterOpcode.Actor_RemoveItems)]
	public partial class Actor_RemoveItems : IActorMessage {}

// 比赛信息
	[Message(OuterOpcode.Actor_MatchInformation)]
	public partial class Actor_MatchInformation : IActorMessage {}

// 吐积分
	[Message(OuterOpcode.C2G_SpitScore)]
	public partial class C2G_SpitScore : IMessage {}

// 吐积分
	[Message(OuterOpcode.Actor_SpitScore)]
	public partial class Actor_SpitScore : IActorMessage {}

// 冲刺技能
	[Message(OuterOpcode.Frame_Sprint)]
	public partial class Frame_Sprint : IFrameMessage {}

// 选择类型
	[Message(OuterOpcode.C2G_ChooseType)]
	public partial class C2G_ChooseType : IMessage {}

// 玩家选择类型
	[Message(OuterOpcode.PlayerChooseType)]
	public partial class PlayerChooseType {}

// 大厅信息
	[Message(OuterOpcode.Actor_HallInformation)]
	public partial class Actor_HallInformation : IActorMessage {}

	[Message(OuterOpcode.G2C_PlayerEnterMap)]
	public partial class G2C_PlayerEnterMap : IMessage {}

}
namespace ETModel
{
	public static partial class OuterOpcode
	{
		 public const ushort Actor_Test = 101;
		 public const ushort Actor_TestRequest = 102;
		 public const ushort Actor_TestResponse = 103;
		 public const ushort Actor_TransferRequest = 104;
		 public const ushort Actor_TransferResponse = 105;
		 public const ushort C2G_EnterMap = 106;
		 public const ushort G2C_EnterMap = 107;
		 public const ushort UnitInfo = 108;
		 public const ushort Actor_CreateUnits = 109;
		 public const ushort Frame_ClickMap = 110;
		 public const ushort Frame_UnitMove = 111;
		 public const ushort Frame_UnitMoveStop = 112;
		 public const ushort Frame_UnitRotation = 113;
		 public const ushort Frame_UnitSkillCastItem = 114;
		 public const ushort Frame_UnitDragUnit = 115;
		 public const ushort Frame_UnitKickUnit = 116;
		 public const ushort Frame_UnitReliveUnit = 117;
		 public const ushort C2R_Ping = 118;
		 public const ushort R2C_Ping = 119;
		 public const ushort G2C_Test = 120;
		 public const ushort ItemInformation = 121;
		 public const ushort Actor_CreateItems = 122;
		 public const ushort Actor_RemoveItems = 123;
		 public const ushort Actor_MatchInformation = 124;
		 public const ushort C2G_SpitScore = 125;
		 public const ushort Actor_SpitScore = 126;
		 public const ushort Frame_Sprint = 127;
		 public const ushort C2G_ChooseType = 128;
		 public const ushort PlayerChooseType = 129;
		 public const ushort Actor_HallInformation = 130;
		 public const ushort G2C_PlayerEnterMap = 128;
	}
}
