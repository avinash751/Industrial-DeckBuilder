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
        public static GameManager Instance { get; private set; }
        [Header("References")]
        [SerializeField] PrimaryMenusUIManager menuUIManager;
        [SerializeField] GameManagerConfigSO gameManagerConfigSo;
        [field: SerializeField] public IGameBehavior CurrentBehavior { get; private set; }
        [HideInInspector][SerializeReference] private List<GameBehaviorBase> gameBehaviors = new();
        [SerializeReference] List<GameCondition> gameConditions = new();
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
                gameConditions.Clear();
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
            gameConditions.ForEach(condition => condition.InitializeCondition());
            StartGame();
        }

        private void Update()
        {
            if (CurrentBehavior != null)
            {
                CurrentBehavior.OnUpdate();
            }
        }

        public void StartGame() => TransitionTo<StartBehavior>();

        public void PlayGame() => TransitionTo<PlayBehavior>();


        // the way pausign works need to be refactored to work with the satte ans strategy pattern
        // public void TogglePause() => GetBehavior<PauseBehavior>().TogglePauseState();

        public void WinGame() => TransitionTo<WinBehavior>();

        public void LoseGame() => TransitionTo<LoseBehavior>();

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
                    return targetBehavior;
                }
            }
            Debug.LogWarning($"No GameBehavior of type {typeof(T).Name} found on GameManager.");
            return null;
        }


        private void TransitionTo<T>() where T : GameBehaviorBase
        {
            T newBehavior = GetBehavior<T>();
            if (newBehavior == null)
            {
                Debug.LogError($"Could Not Tranasition to {typeof(T).Name} as the the behavior type was not found on GameManager.");
                return;
            }

            if (CurrentBehavior != null) { CurrentBehavior.Exit(); }
            CurrentBehavior = newBehavior;
            CurrentBehavior.Enter();
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
            gameConditions.Add(condition);
        }
        public void ClearAllGameConditions() => gameConditions.Clear();

        #endregion
    }
}