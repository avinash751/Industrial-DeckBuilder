using UnityEngine;
using GameManagerSystem;

namespace GameManagerSystem.GameBehaviors.Conditions
{
    [System.Serializable]
    public class WinCondition : GameCondition
    {
        [SerializeField] GameProgression gameProgression;
        [SerializeField] string winAudioKey;
        public WinCondition(GameManager _gameManager) : base(_gameManager)
        {
            conditionName = "Win Condition";
        }
        public override void InitializeCondition()
        {
            if (gameProgression == null)
            {
                Debug.LogError("Game Progression is not assigned in Win Condition script");
                return;
            }
            gameProgression.OnAllCardPacksUnlocked += ExecuteGameCondition;
        }
        public override bool IsGameConditionMet()
        {
            return true;
        }
        public override void ExecuteGameCondition()
        {
            GameManager.Instance.WinGame();
            AudioManager.Instance?.PlayAudio(winAudioKey);

        }

    }
}

