using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.STP;

namespace GameManagerSystem
{
    public enum GameBehaviorEventType
    {
        Initialized,
        Started,
        Paused,
        UnPaused,
        Win,
        Lose
    }
    public abstract class GameBehaviorBase : MonoBehaviour
    {
        protected GameManager GameManager { get; private set; }
        [SerializeField] protected BaseGameBehaviourConfigSO BehaviourConfigSO;

        protected virtual void Awake()
        {
            GameManager = GameManager.Instance;
            if (GameManager == null)
            {
                Debug.LogError("GameManager not found in the scene. Make sure GameManager script is present.");
            }
        }

        public abstract void ExecuteBehavior();


        // This needs to be called in Execute Behaviour in every derived class to apply the base behavior settings
        protected virtual void  ApplyBaseBehaviorSettings(BaseGameBehaviourConfigSO config, GameBehaviorEventType eventType)
        {
            if (BehaviourConfigSO == null)
            {
                Debug.LogError("Config So is not assigned to this Game Manager Behaviour.Please assign it in the Inspector.");
               
            }

            Debug.Log("Executing " + GetType().ToString());

            SetTimescale(config.IsTimeZeroOnExecution ? 0f : 1f);
            SetCursorLockMode(config.IsCursorLockedOnExecution);
            SetCursorVisible(config.IsCursorVisibleOnExecution);
            if (config.LoadSceneOnExecution)
            {
                LoadScene(config.SceneToLoad);
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

           InvokeOnBehaviorEvent(eventType);
        }

        protected virtual void SetTimescale(float customScale)
        {
            Time.timeScale = customScale;
        }

        protected virtual void SetCursorLockMode(bool enabled)
        {      
            if(enabled)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        protected virtual void SetCursorVisible(bool visible)
        {
            Cursor.visible = visible;
        }

        protected virtual void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        #region Centralized Behavior Event

        public event Action<GameBehaviorEventType> OnBehaviorEvent;
        protected void InvokeOnBehaviorEvent(GameBehaviorEventType eventType) => OnBehaviorEvent?.Invoke(eventType);

        #endregion
    }
}