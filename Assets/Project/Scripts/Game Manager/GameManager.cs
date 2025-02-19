using GameManagerSystem.GameBehaviors;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagerSystem
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            MenuManager = GetComponentInChildren<PrimaryMenusUIManager>();
            if (MenuManager == null)
            {
                Debug.LogError("PrimaryMenusUIManager not found in children of GameManager!");
            }
        }

        #endregion

        [Header("Game Behaviors")]
        [SerializeField] private List<GameBehaviorBase> gameBehaviors = new List<GameBehaviorBase>();

        public PrimaryMenusUIManager MenuManager { get; private set; }

        #region Game State Control

        public void InitializeGame()
        {
            ExecuteBehavior<StartBehavior>();
        }

        public void StartGame()
        {
            ExecuteBehavior<PlayBehavior>();
        }

        public void WinGame()
        {
            ExecuteBehavior<WinBehavior>();
        }

        public void LoseGame()
        {
            ExecuteBehavior<LoseBehavior>();
        }

        public void ExecutePauseBehavior()
        {
            ExecuteBehavior<PauseBehavior>();
        }

        public void ExecuteUnPauseBehavior()
        {
            ExecuteBehavior<PauseBehavior>();
            ExecuteBehavior<PlayBehavior>();
        }

        #endregion

        #region Behavior Locator

        public T GetBehavior<T>() where T : GameBehaviorBase
        {
            foreach (var behavior in gameBehaviors)
            {
                if (behavior is T targetBehavior)
                {
                    return (T)targetBehavior;
                }
            }
            Debug.LogWarning($"No GameBehavior of type {typeof(T).Name} found on GameManager.");
            return null;
        }


        private void ExecuteBehavior<T>() where T : GameBehaviorBase
        {
            T behavior = GetBehavior<T>();
            if (behavior != null)
            {
                behavior.ExecuteBehavior();
            }
        }

        #endregion
    }
}