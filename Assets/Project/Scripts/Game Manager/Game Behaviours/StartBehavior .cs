using UnityEngine;
using GameManagerSystem.Configuration;

namespace GameManagerSystem.GameBehaviors
{
    public class StartBehavior : GameBehaviorBase
    {
        [Header("Configuration")]
        [SerializeField] private StartBehaviorConfigSO config;

        public override void ExecuteBehavior()
        {
            if (config == null)
            {
                Debug.LogError("StartBehaviorConfigSO is not assigned to StartBehavior. Please assign it in the Inspector.");
                return;
            }

            Debug.Log("Executing Start Behavior");

            SetTimescale(config.SetTimescaleToZeroOnStart ? 0f : 1f);
            SetCursorLockMode(config.DisableCursorLockModeOnStart);
            SetCursorVisible(config.EnableCursorVisibilityOnStart);
            if (config.LoadStartSceneOnStart)
            {
                LoadScene(config.StartSceneIndex);
            }

            if (GameManager.MenuManager != null)
            {
                GameManager.MenuManager.ShowStartMenu();
            }
            else
            {
                Debug.LogWarning("PrimaryMenusUIManager is null. Cannot show start menu.");
            }

            InvokeOnBehaviorEvent(GameBehaviorEventType.Initialized);
        }
    }
}