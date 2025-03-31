using GameManagerSystem.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameManagerSystem.GameBehaviors.Conditions;

namespace GameManagerSystem
{

    [System.Serializable]
    public abstract class GameBehaviorBase : IGameBehavior
    {
        [SerializeField][HideInInspector] string behaviorName;
        [HideInInspector][field: SerializeField] protected GameManager gameManager { get; private set; }
        [HideInInspector][field: SerializeField] protected PrimaryMenusUIManager menuUiManager { get; private set; }
        [HideInInspector][SerializeField] protected BaseGameBehaviourConfigSO BehaviourConfigSO;

        public GameBehaviorBase(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO, PrimaryMenusUIManager _menuUiManager)
        {
            gameManager = _gameManager;
            menuUiManager = _menuUiManager;
            BehaviourConfigSO = _behaviourConfigSO;
            behaviorName = _behaviourConfigSO.BehaviorType.ToString() + " Behaviour";
        }


        public virtual void Enter()
        {

        }
        public virtual void OnUpdate()
        {

        }

        public virtual void Exit()
        {

        }

        /// <summary>
        /// This needs to be called in EnterState  in every derived class to apply the base behavior settings
        /// </summary>
        /// <param name="config"></param>
        /// <param name="eventType"></param>
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


        /// <summary>
        /// This needs to be ovverriden in every derived class to set
        /// which menus to enable or disable on entering the state
        /// </summary>

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

        public virtual GameCondition GetGameCondition()
        {
            return null;
        }


        #region Centralized Behavior Event
        public event Action<GameBehaviorEventType> OnGameBehaviorEvent;
        protected void InvokeOnBehaviorEvent(GameBehaviorEventType eventType) => OnGameBehaviorEvent?.Invoke(eventType);
        #endregion
    }
}