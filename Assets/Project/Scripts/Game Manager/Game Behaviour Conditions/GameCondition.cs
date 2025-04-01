using System;
using UnityEngine;

namespace GameManagerSystem.GameBehaviors.Conditions
{
    [System.Serializable]
    public abstract class GameCondition
    {
        [SerializeField][HideInInspector] protected string conditionName;
        [SerializeField][HideInInspector] protected GameManager gameManager;
        public Action OnGameCondtionMet;

        public GameCondition(GameManager _gameManager)
        {
            gameManager = _gameManager;
        }
        public abstract void Initialize();
        public virtual void OnUpdate(float deltaTime = 0) { }
        public virtual void CleanUp() { }
        protected void TriggerGameConditionMet()
        {
            OnGameCondtionMet?.Invoke();
            HandleOnGameConditionMet();
        }
        protected abstract void HandleOnGameConditionMet();
    }
}