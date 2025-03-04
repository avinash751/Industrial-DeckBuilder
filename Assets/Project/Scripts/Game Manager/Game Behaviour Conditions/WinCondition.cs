using UnityEngine;
using GameManagerSystem;

namespace GameManagerSystem.GameBehaviors.Conditions
{
    [System.Serializable]
    public class WinCondition : GameCondition
    {
        public WinCondition(GameManager _gameManager) : base(_gameManager)
        {
            conditionName = "Win Condition";
        }
        public override void InitializeCondition()
        {

        }
        public override bool IsGameConditionMet()
        {
            return true;
        }
        public override void ExecuteGameCondition()
        {
            GameManager.Instance.WinGame();
        }

    }
}

