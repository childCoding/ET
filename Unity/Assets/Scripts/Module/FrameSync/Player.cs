namespace ETModel
{
	public sealed class Player : Entity
	{
        public string Account { get; set; }

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