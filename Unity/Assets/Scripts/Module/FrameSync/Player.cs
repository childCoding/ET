namespace ETModel
{
	public sealed class Player : Entity
	{
		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();
		}
	}
}