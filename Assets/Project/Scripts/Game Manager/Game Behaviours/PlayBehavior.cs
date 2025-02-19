using UnityEngine;
using GameManagerSystem.Configuration;

namespace GameManagerSystem.GameBehaviors
{

    public class PlayBehavior : GameBehaviorBase
    {
        [Header("Configuration")]
        [Tooltip("Configuration Scriptable Object for Play Behavior.")]
        [SerializeField] private PlayBehaviorConfigSO config; // Assign PlayBehaviorConfigSO in Inspector

        /// <summary>
        /// Executes the Play Game Behavior logic.
        /// </summary>
        public override void ExecuteBehavior()
        {
            if (config == null)
            {
                Debug.LogError("PlayBehaviorConfigSO is not assigned to PlayBehavior. Please assign it in the Inspector.");
                return; // Exit if config is not assigned to prevent errors
            }

            Debug.Log("Executing Play Behavior");

            // Apply settings DIRECTLY from config Scriptable Object
            SetTimescale(config.setTimescaleToOneOnPlay ? 1f : 1f); 
            SetCursorLockMode(config.enableCursorLockModeOnPlay);
            SetCursorVisible(!config.disableCursorVisibilityOnPlay); 
            if (config.loadSceneOnPlay)
            {
                LoadScene(config.playSceneIndex);
            }

            // Get Menu Manager and hide start menu (if it's still visible)
            if (GameManager.MenuManager != null)
            {
                GameManager.MenuManager.HideStartMenu();
                GameManager.MenuManager.HidePauseMenu();
                GameManager.MenuManager.HideWinMenu();
                GameManager.MenuManager.HideLoseMenu();
            }
            else
            {
                Debug.LogWarning("PrimaryMenusUIManager is null. Cannot hide menus.");
            }

            InvokeOnBehaviorEvent(GameBehaviorEventType.Started);
            InvokeOnBehaviorEvent(GameBehaviorEventType.UnPaused);
        }
    }
}