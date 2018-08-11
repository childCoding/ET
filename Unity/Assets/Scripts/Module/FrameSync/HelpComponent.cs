using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
	[ObjectSystem]
	public class HelpComponentAwakeSystem : AwakeSystem<HelpComponent>
	{
		public override void Awake(HelpComponent self)
		{
			self.Awake();
		}
	}
    [ObjectSystem]
    public class HelpComponentUpdateSystem : UpdateSystem<HelpComponent>
    {
        public override void Update(HelpComponent self)
        {
            self.Update();
        }
    }
    public class HelpComponent : Component
	{

		public void Awake()
		{
		}
        public void Update()
        {
           
        }
	}
}