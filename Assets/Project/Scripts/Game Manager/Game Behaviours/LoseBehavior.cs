using UnityEngine;
using GameManagerSystem.Configuration;

namespace GameManagerSystem.GameBehaviors
{
    public class LoseBehavior : GameBehaviorBase
    {
        [Header("Configuration")]
        [SerializeField] private LoseBehaviorConfigSO config;

        public override void ExecuteBehavior()
        {
            if (config == null)
            {
                Debug.LogError("LoseBehaviorConfigSO is not assigned to LoseBehavior. Please assign it in the Inspector.");
                return;
            }

            Debug.Log("Executing Lose Behavior");

            SetTimescale(config.SetTimescaleToZeroOnLose ? 0f : 1f);
            SetCursorLockMode(config.DisableCursorLockModeOnLose);
            SetCursorVisible(config.EnableCursorVisibilityOnLose);
            if (config.LoadSceneOnLose)
            {
                LoadScene(config.LoseSceneIndex);
            }

            if (GameManager.MenuManager != null)
            {
                GameManager.MenuManager.ShowLoseMenu();
                GameManager.MenuManager.HidePauseMenu();
                GameManager.MenuManager.HideStartMenu();
                GameManager.MenuManager.HideWinMenu();
            }
            else
            {
                Debug.LogWarning("PrimaryMenusUIManager is null. Cannot show lose menu.");
            }

            InvokeOnBehaviorEvent(GameBehaviorEventType.Lose);
        }
    }
}