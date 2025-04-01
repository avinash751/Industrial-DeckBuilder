using GameManagerSystem.UI;
using UnityEngine;

namespace GameManagerSystem.GameBehaviors
{
    [System.Serializable]
    public class PlayBehavior : GameBehaviorBase
    {
        public PlayBehavior(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO, PrimaryMenusUIManager menuUiManager)
            : base(_gameManager, _behaviourConfigSO, menuUiManager)
        {

        }

        protected override void SetMenuSettings()
        {
            menuUiManager.HideAllGameMenus();
        }
    }
}