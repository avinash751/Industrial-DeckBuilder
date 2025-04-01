using GameManagerSystem.UI;
using GameManagerSystem.GameBehaviors.Conditions;
using UnityEngine;

namespace GameManagerSystem.GameBehaviors
{
    [System.Serializable]
    public class LoseBehavior : GameBehaviorBase
    {
        [HideInInspector][SerializeField] LoseCondition loseCondition;
        public LoseBehavior(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO, PrimaryMenusUIManager menuUiManager) : base(_gameManager, _behaviourConfigSO, menuUiManager)
        {
            loseCondition = new LoseCondition(_gameManager);
        }

        public override void Enter()
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