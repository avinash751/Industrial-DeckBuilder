using GameManagerSystem.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagerSystem
{

    [System.Serializable]
    public abstract class GameBehaviorBase
    {
        [SerializeField][HideInInspector] string behaviorName;
        protected GameManager gameManager { get; private set; }
        protected PrimaryMenusUIManager menuUiManager { get; private set; }
        [SerializeField] protected BaseGameBehaviourConfigSO BehaviourConfigSO;



        public GameBehaviorBase(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO, PrimaryMenusUIManager _menuUiManager)
        {
            gameManager = _gameManager;
            menuUiManager = _menuUiManager;
            BehaviourConfigSO = _behaviourConfigSO;
            behaviorName = _behaviourConfigSO.BehaviorType.ToString() + " Behaviour";
        }

        public abstract void ExecuteBehavior();



        // This needs to be called in Execute Behaviour in every derived class to apply the base behavior settings
        protected virtual void ApplyBehaviorSettings(BaseGameBehaviourConfigSO config, GameBehaviorEventType eventType)
        {
            if (BehaviourConfigSO == null)
            {
                Debug.LogError("Config So is not assigned to this Game Manager Behaviour.Please assign it in the Inspector.");

            }

            Debug.Log("Executing " + GetType().ToString());

            SetTimescale(config.IsTimeZeroOnExecution ? 0f : 1f);
            SetCursorLockMode(config.IsCursorLockedOnExecution);
            SetCursorVisible(config.IsCursorVisibleOnExecution);
            SetUISettings();
            SetMenuSettings();
            if (config.LoadSceneOnExecution)
            {
                LoadScene(config.SceneToLoad);
            }

            InvokeOnBehaviorEvent(eventType);
        }

        protected abstract void SetMenuSettings();
        
        protected virtual void SetUISettings()
        {
            if (BehaviourConfigSO.ShowGameUIOnExecution)
            {
                menuUiManager.ShowInGameUI();
            }
            else
            {
                menuUiManager.HideInGameUI();
            }
        }

        protected virtual void SetTimescale(float customScale)
        {
            Time.timeScale = customScale;
        }

        protected virtual void SetCursorLockMode(bool enabled)
        {
            if (enabled)
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