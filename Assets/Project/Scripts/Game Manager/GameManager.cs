using GameManagerSystem.Configuration;
using GameManagerSystem.GameBehaviors;
using GameManagerSystem.GameBehaviors.Conditions;
using GameManagerSystem.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagerSystem
{
    public class GameManager : MonoBehaviour
    {

        [Header("References")]
        public static GameManager Instance { get; private set; }
        [SerializeField] PrimaryMenusUIManager menuUIManager;

        [SerializeField] GameManagerConfigSO gameManagerConfigSo;
        private List<GameBehaviorBase> gameBehaviors = new List<GameBehaviorBase>();
        [SerializeReference] List<GameCondition> gameConditions = new List<GameCondition>();
        #region Singleton

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

        private void OnValidate()
        {
            if (gameManagerConfigSo == null)
            {
                Debug.LogWarning("No Game Manager Config found on GameManager. Please assign one.");
                gameBehaviors.Clear();
            }
            else
            {
                gameManagerConfigSo.InitializeGameConfigurations(this, menuUIManager);
                gameManagerConfigSo.GetAllGameConditions(this, gameBehaviors);
                TryGetComponent(out menuUIManager);
            }
        }


        #region Game State Control

        private void Start()
        {
            StartGame();
            gameConditions.ForEach(condition => condition.InitializeCondition());
        }

        private void Update()
        {
            InputToPauseAndUnpauseGame();
        }

        public void StartGame() => ExecuteBehavior<StartBehavior>();

        public void PlayGame() => ExecuteBehavior<PlayBehavior>();

        private void InputToPauseAndUnpauseGame() => ExecuteBehavior<PauseBehavior>();

        public void TogglePause() => GetBehavior<PauseBehavior>().TogglePauseState();

        public void WinGame() => ExecuteBehavior<WinBehavior>();

        public void LoseGame() => ExecuteBehavior<LoseBehavior>();

        public void LoadMainMenu() => SceneManager.LoadScene(gameManagerConfigSo.MainMenuSceneIndex);

        public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        public void QuitGame() => Application.Quit();

        #endregion

        #region Behavior Related Helper Fucntions

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
        public void ClearAllBehaviours() => gameBehaviors.Clear();
        public void AddGameCondition(GameCondition condition)
        {
            if (gameConditions.Count == 0)
            {
                gameConditions.Add(condition);
                return;
            }
            foreach (GameCondition gameCondition in gameConditions)
            {
                if (gameCondition.GetType() == condition.GetType())
                {
                    Debug.LogWarning($"Game Condition of type {condition.GetType().Name} already exists in GameManager.");
                    return;
                }
            }
        }
        public void ClearAllGameConditions() => gameConditions.Clear();

        #endregion
    }
}