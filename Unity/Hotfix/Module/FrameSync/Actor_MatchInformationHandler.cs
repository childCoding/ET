using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class Actor_MatchInformationHandler : AMHandler<Actor_MatchInformation>
    {
        protected override void Run(ETModel.Session session, Actor_MatchInformation message)
        {
            UIOperationComponent uiOperationComponent = Game.Scene.GetComponent<UIComponent>().Get(UIType.UIOperation).GetComponent<UIOperationComponent>();
            uiOperationComponent.WeakScore.text = message.WeakScore.ToString();
            uiOperationComponent.StrongScore.text = message.StrongScore.ToString();
        }
    }
}
