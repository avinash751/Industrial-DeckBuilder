using UnityEngine;
using GameManagerSystem.Configuration;
using GameManagerSystem.UI;

namespace GameManagerSystem.GameBehaviors
{
    [System.Serializable]
    public class PauseBehavior : GameBehaviorBase
    {
        [HideInInspector][SerializeField]private PauseBehaviorConfigSO config;

        [Header("Pause State - Managed by PauseBehavior")]
        [field: SerializeField] public bool IsPaused { get; private set; } = false;

        public PauseBehavior(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO, PrimaryMenusUIManager menuUiManager) : base(_gameManager, _behaviourConfigSO, menuUiManager)
        {
            config = (PauseBehaviorConfigSO)BehaviourConfigSO;
        }

        public override void ExecuteBehavior()
        {
            HandlePauseInput();
        }

        private void HandlePauseInput()
        {
            if (Input.GetKeyDown(config.PauseKey))
            {
                TogglePauseState();
            }
        }

        public void TogglePauseState()
        {
            if (IsPaused)
            {
                ExecuteUnPause();
            }
            else
            {
                ExecutePause();
            }
          
        }

        private void ExecutePause()
        {
            if (IsPaused) return;
            SetMenuSettings();
            ApplyBehaviorSettings(config, GameBehaviorEventType.Paused);
            IsPaused = true;
        }

        private void ExecuteUnPause()
        {
            if (!IsPaused) return;
            InvokeOnBehaviorEvent(GameBehaviorEventType.UnPaused);
            gameManager.GetBehavior<PlayBehavior>().ExecuteBehavior();
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
    }
}