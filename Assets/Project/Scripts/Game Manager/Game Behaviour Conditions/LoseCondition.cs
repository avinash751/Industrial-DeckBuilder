using UnityEngine;
using GameManagerSystem;


namespace GameManagerSystem.GameBehaviors.Conditions
{
    [System.Serializable]
    public class LoseCondition : GameCondition
    {
        [SerializeField] string LoseAudioKey;
        public LoseCondition(GameManager _gameManager) : base(_gameManager)
        {
            conditionName = "Lose Condition";
        }
        public override void Initialize()
        {
            MoneyManager.Instance.OnMaxDebtAcquired += HandleOnGameConditionMet;
        }

        protected override void HandleOnGameConditionMet()
        {
            gameManager?.LoseGame();
            AudioManager.Instance?.PlayAudio(LoseAudioKey);
        }
    }
}

