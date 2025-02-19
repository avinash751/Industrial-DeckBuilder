using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        protected virtual void Awake()
        {
            GameManager = GameManager.Instance;
            if (GameManager == null)
            {
                Debug.LogError("GameManager not found in the scene. Make sure GameManager script is present.");
            }
        }

        public abstract void ExecuteBehavior();

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