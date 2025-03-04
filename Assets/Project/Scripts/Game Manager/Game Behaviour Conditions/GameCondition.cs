using UnityEngine;

namespace GameManagerSystem.GameBehaviors.Conditions
{
    [System.Serializable]
    public abstract class GameCondition
    {
        [SerializeField][HideInInspector] protected string conditionName;
        GameManager gameManager;

        public GameCondition(GameManager _gameManager)
        {
            gameManager = _gameManager;
        }

        public abstract void InitializeCondition();
        public abstract bool IsGameConditionMet();
        public abstract void ExecuteGameCondition();
    }
}

