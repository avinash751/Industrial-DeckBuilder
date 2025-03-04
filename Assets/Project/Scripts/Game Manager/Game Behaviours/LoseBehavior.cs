using GameManagerSystem.UI;
using GameManagerSystem.GameBehaviors.Conditions;

namespace GameManagerSystem.GameBehaviors
{
    [System.Serializable]
    public class LoseBehavior : GameBehaviorBase
    {
        LoseCondition loseCondition;
        public LoseBehavior(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO, PrimaryMenusUIManager menuUiManager) : base(_gameManager, _behaviourConfigSO, menuUiManager)
        {
            loseCondition = new LoseCondition(_gameManager);
        }

        public override void ExecuteBehavior()
        {
            ApplyBehaviorSettings(BehaviourConfigSO, GameBehaviorEventType.Lose);
        }

        protected override void SetMenuSettings()
        {
            menuUiManager.HideAllGameMenus();
            menuUiManager.ShowLoseMenu();
        }

        public override GameCondition GetGameCondition()
        {
            return loseCondition;
        }
    }
}