using System.Threading.Tasks;
using ETModel;
using Google.Protobuf;

namespace ETHotfix
{
	[ActorMessageHandler(AppType.Map)]
	public class OneFrameMessageHandler: AMActorHandler<Unit, OneFrameMessage>
    {
	    protected override async Task Run(Session session, Unit entity, OneFrameMessage message)
	    {
		    Game.Scene.GetComponent<ServerFrameComponent>().Add(message);
            // 消息处理
            ushort opcode = (ushort)message.Op;
            object instance = Game.Scene.GetComponent<OpcodeTypeComponent>().GetInstance(opcode);
            byte[] bytes = ByteString.Unsafe.GetBuffer(message.AMessage);
            ETModel.IMessage realMessage = (ETModel.IMessage)Game.Scene.GetComponent<NetOuterComponent>().MessagePacker.DeserializeFrom(instance, bytes, 0, bytes.Length);
            Game.Scene.GetComponent<MessageDispatherComponent>().Handle(session, new MessageInfo((ushort)message.Op, realMessage));
            await Task.CompletedTask;
	    }
    }
}
