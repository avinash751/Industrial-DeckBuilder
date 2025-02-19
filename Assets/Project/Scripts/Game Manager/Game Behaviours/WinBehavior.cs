using UnityEngine;
using GameManagerSystem.Configuration;

namespace GameManagerSystem.GameBehaviors
{
    public class WinBehavior : GameBehaviorBase
    {
        [Header("Configuration")]
        [SerializeField] private WinBehaviorConfigSO config;

        public override void ExecuteBehavior()
        {
            if (config == null)
            {
                Debug.LogError("WinBehaviorConfigSO is not assigned to WinBehavior. Please assign it in the Inspector.");
                return;
            }

            Debug.Log("Executing Win Behavior");

            SetTimescale(config.SetTimescaleToZeroOnWin ? 0f : 1f);
            SetCursorLockMode(config.DisableCursorLockModeOnWin);
            SetCursorVisible(config.EnableCursorVisibilityOnWin);
            if (config.LoadSceneOnWin)
            {
                LoadScene(config.WinSceneIndex);
            }

            if (GameManager.MenuManager != null)
            {
                GameManager.MenuManager.ShowWinMenu();
                GameManager.MenuManager.HidePauseMenu();
                GameManager.MenuManager.HideStartMenu();
                GameManager.MenuManager.HideLoseMenu();
            }
            else
            {
                Debug.LogWarning("PrimaryMenusUIManager is null. Cannot show win menu.");
            }

            InvokeOnBehaviorEvent(GameBehaviorEventType.Win);
        }
    }
}