using GameManagerSystem.UI;
using GameManagerSystem.GameBehaviors.Conditions;
using UnityEngine;

namespace GameManagerSystem.GameBehaviors
{
    [System.Serializable]
    public class WinBehavior : GameBehaviorBase
    {

        [HideInInspector][SerializeField] WinCondition winCondition;
        public WinBehavior(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO, PrimaryMenusUIManager menuUiManager) : base(_gameManager, _behaviourConfigSO, menuUiManager)
        {
            winCondition = new WinCondition(_gameManager);
        }

        protected override void SetMenuSettings()
        {
            menuUiManager.HideAllGameMenus();
            menuUiManager.ShowWinMenu();
        }

        public override GameCondition GetGameCondition()
        {
            return winCondition;
        }
    }
}