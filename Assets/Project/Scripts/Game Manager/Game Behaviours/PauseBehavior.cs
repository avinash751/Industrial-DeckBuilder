using UnityEngine;
using GameManagerSystem.Configuration;

namespace GameManagerSystem.GameBehaviors
{
    [System.Serializable]
    public class PauseBehavior : GameBehaviorBase
    {
        private PauseBehaviorConfigSO config;

        [Header("Pause State - Managed by PauseBehavior")]
        [field: SerializeField] public bool IsPaused { get; private set; } = false;

        public PauseBehavior(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO) : base(_gameManager, _behaviourConfigSO)
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
            IsPaused = true;
            ApplyBehaviorSettings(config, GameBehaviorEventType.Paused);
        }

        private void ExecuteUnPause()
        {
            if (!IsPaused) return;
            IsPaused = false;
            InvokeOnBehaviorEvent(GameBehaviorEventType.UnPaused);
            gameManager.GetBehavior<PlayBehavior>().ExecuteBehavior();
        }
    }
}