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

        public override void OnUpdate()
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
                Enter();
            }
            else
            {
                Exit();
            }  
        }

        public override void Enter()
        {
            if (IsPaused) return;
            SetMenuSettings();
            ApplyBehaviorSettings(config, GameBehaviorEventType.Paused);
            IsPaused = true;
        }


        public override void Exit()
        {
            if (!IsPaused) return;
            SetMenuSettings();
            ApplyBehaviorSettings(config, GameBehaviorEventType.UnPaused);
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