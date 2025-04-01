using UnityEngine;
using GameManagerSystem.Configuration;
using GameManagerSystem.UI;
using GameManagerSystem.GameBehaviors.Conditions;

namespace GameManagerSystem.GameBehaviors
{
    [System.Serializable]
    public class PauseBehavior : GameBehaviorBase
    {
        [HideInInspector][SerializeField]private PauseBehaviorConfigSO config;
        [HideInInspector][SerializeField]private PauseCondition pauseCondition;
        [Header("Pause State - Managed by PauseBehavior")]
        [field: SerializeField] public bool IsPaused { get; private set; } = false;

        public PauseBehavior(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO, PrimaryMenusUIManager menuUiManager) : base(_gameManager, _behaviourConfigSO, menuUiManager)
        {
            config = (PauseBehaviorConfigSO)BehaviourConfigSO;
            pauseCondition = new PauseCondition(_gameManager, config);
        }

        protected override void OnEnter()
        {
            if (IsPaused)
            {
                SetMenuSettings();
            }
        }

        public override void Exit()
        {
            if (!IsPaused) return;
            SetMenuSettings();
            IsPaused = false;
        }

        protected override void SetMenuSettings()
        {
            if (!IsPaused)
            {
                menuUiManager.HideAllGameMenus();
                menuUiManager.ShowPauseMenu();
            }
        }

        public override GameCondition GetGameCondition()
        {
            return pauseCondition;
        }
    }
}