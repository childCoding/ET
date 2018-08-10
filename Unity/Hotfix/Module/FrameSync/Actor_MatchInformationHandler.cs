using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_MatchInformationHandler : AMHandler<Actor_MatchInformation>
    {
        protected override void Run(ETModel.Session session, Actor_MatchInformation message)
        {
        }
    }
}
