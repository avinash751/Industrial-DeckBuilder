using UnityEngine;
using GameManagerSystem.Configuration;

namespace GameManagerSystem.GameBehaviors
{
    public class PauseBehavior : GameBehaviorBase
    {
        [Header("Configuration")]
        [SerializeField] private PauseBehaviorConfigSO config;

        [Header("Pause State - Managed by PauseBehavior")]
        [field: SerializeField] public bool IsPaused { get; private set; } = false;

        private void Update()
        {
            HandlePauseInput();
        }

        public override void ExecuteBehavior()
        {
            TogglePauseState();
        }

        private void HandlePauseInput()
        {
            if (Input.GetKeyDown(config.PauseKey))
            {
                TogglePauseState();                                                                             
            }
        }

        private void TogglePauseState()
        {
            if (IsPaused)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
        }

        private void PauseGame()
        {
            if (IsPaused) return;
            IsPaused = true;
            ExecutePauseBehavior();
        }

        private void UnPauseGame()
        {
            if (!IsPaused) return;
            IsPaused = false;
            ExecuteUnPauseBehavior();
        }


        private void ExecutePauseBehavior()
        {
            if (config == null)
            {
                Debug.LogError("PauseBehaviorConfigSO is not assigned to PauseBehavior. Please assign it in the Inspector.");
                return;
            }

            Debug.Log("Executing Pause Behavior");

            SetTimescale(config.SetTimescaleToZeroOnPause ? 0f : 1f);
            SetCursorLockMode(config.DisableCursorLockModeOnPause);

            if (GameManager.MenuManager != null)
            {
                GameManager.MenuManager.ShowPauseMenu();
                GameManager.MenuManager.HideStartMenu();
                GameManager.MenuManager.HideWinMenu();
                GameManager.MenuManager.HideLoseMenu();
            }
            else
            {
                Debug.LogWarning("PrimaryMenusUIManager is null. Cannot show pause menu.");
            }

            InvokeOnBehaviorEvent(GameBehaviorEventType.Paused);
        }

        private void ExecuteUnPauseBehavior()
        {
            InvokeOnBehaviorEvent(GameBehaviorEventType.UnPaused);
        }
    }
}