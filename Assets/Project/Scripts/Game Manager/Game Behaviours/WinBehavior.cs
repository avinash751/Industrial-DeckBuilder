using GameManagerSystem.UI;
using UnityEngine;

namespace GameManagerSystem.GameBehaviors
{
    [System.Serializable]
    public class WinBehavior : GameBehaviorBase
    {
        public WinBehavior(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO, PrimaryMenusUIManager menuUiManager) : base(_gameManager, _behaviourConfigSO, menuUiManager)
        {
        }
        public override void ExecuteBehavior()
        {
            ApplyBehaviorSettings(BehaviourConfigSO, GameBehaviorEventType.Lose);
        }

        protected override void SetMenuSettings()
        {
            menuUiManager.HideAllGameMenus();
            menuUiManager.ShowWinMenu();
        }
    }
}