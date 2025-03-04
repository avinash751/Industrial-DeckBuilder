using UnityEngine;
using GameManagerSystem;


namespace GameManagerSystem.GameBehaviors.Conditions
{

    [System.Serializable]
    public class LoseCondition : GameCondition
    {
        public LoseCondition(GameManager _gameManager) : base(_gameManager)
        {
            conditionName = "Lose Condition";

        }
        public override void InitializeCondition()
        {
            MoneyManager.Instance.OnMaxDebtAcquired += ExecuteGameCondition;
        }
        public override bool IsGameConditionMet()
        {
            return true;
        }
        public override void ExecuteGameCondition()
        {
            GameManager.Instance.LoseGame();
        }

    }
}

