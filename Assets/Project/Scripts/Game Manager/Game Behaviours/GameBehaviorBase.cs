using GameManagerSystem.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameManagerSystem.GameBehaviors.Conditions;
using System.Collections.Generic;
using GameManagerSystem.GameBehaviors;

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

        public void Enter()
        {
            ApplyBaseSettings();
            OnEnter();
        }

        /// <summary>
        ///  If The behavior needs to have custom logic when
        ///  entering the state,then it needs to be overriden in the derived class
        /// </summary>
        protected virtual void OnEnter() { }

        /// <summary>
        /// This is intended to be called every frame when the behavior is active
        /// This needs to be overriden in the derived class if custom logic needs 
        /// to run every frame
        /// </summary>
        public virtual void OnUpdate() { }
        public virtual void Exit() { }
  
        /// <summary>
        /// This needs to be ovverriden in every derived class to set
        /// which menus to enable or disable on entering the state
        /// </summary>
        protected abstract void SetMenuSettings();
       

        private void ApplyBaseSettings()
        {
            if (BehaviourConfigSO == null)
            {
                Debug.LogError("Config So is not assigned to this Game Manager Behaviour.Please assign it in the Inspector.");

            }

            Debug.Log("Executing " + GetType().ToString());

            SetTimescale(BehaviourConfigSO.IsTimeZeroOnExecution ? 0f : 1f);
            SetCursorLockMode(BehaviourConfigSO.IsCursorLockedOnExecution);
            SetCursorVisible(BehaviourConfigSO.IsCursorVisibleOnExecution);
            SetUISettings();
            SetMenuSettings();
            if (BehaviourConfigSO.LoadSceneOnExecution)
            {
                LoadScene(BehaviourConfigSO.SceneToLoad);
            }

            InvokeOnBehaviorEvent(this);
        }

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

        public virtual GameCondition GetGameCondition() => null;
  
        #region Centralized Behavior Event
        public event Action<GameBehaviorBase> OnGameBehaviorEvent;
        protected void InvokeOnBehaviorEvent(GameBehaviorBase behvaiorType) => OnGameBehaviorEvent?.Invoke(behvaiorType);
        #endregion
    }
}