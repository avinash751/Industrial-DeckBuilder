using GameManagerSystem.UI;
using UnityEngine;

namespace GameManagerSystem.GameBehaviors
{
    [System.Serializable]
    public class StartBehavior : GameBehaviorBase
    {
        public StartBehavior(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO,PrimaryMenusUIManager menuUiManager) : base(_gameManager, _behaviourConfigSO, menuUiManager)
        {
        }
        public override void ExecuteBehavior()
        {
            ApplyBehaviorSettings(BehaviourConfigSO, GameBehaviorEventType.Initialized);
        }

        protected override void SetMenuSettings()
        {
            menuUiManager.HideAllGameMenus();
            menuUiManager.ShowStartMenu();
        }
    }
}