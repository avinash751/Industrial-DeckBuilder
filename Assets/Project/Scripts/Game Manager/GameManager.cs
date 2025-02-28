using GameManagerSystem.GameBehaviors;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagerSystem
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton & Intialization

        public static GameManager Instance { get; private set; }

        private void OnEnable()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        #endregion

        [Header("Game Behaviors")]
        [SerializeField] private List<GameBehaviorBase> gameBehaviors = new List<GameBehaviorBase>();


        #region Game State Control

        private void Start()
        {
            ExecuteBehavior<StartBehavior>();
        }

        private void Update()
        {
            ExecuteBehavior<PauseBehavior>();
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

        public void AddGameBehaviour(GameBehaviorBase behavior) => gameBehaviors.Add(behavior);
       

        #endregion
    }
}