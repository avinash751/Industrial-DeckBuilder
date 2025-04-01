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
        public override void Initialize()
        {
            if (gameProgression == null)
            {
                Debug.LogError("Game Progression is not assigned in Win Condition script");
                return;
            }
            gameProgression.OnAllCardPacksUnlocked += HandleOnGameConditionMet;
        }

        protected override void HandleOnGameConditionMet()
        {
            gameManager?.WinGame();
            AudioManager.Instance?.PlayAudio(winAudioKey);
        }

        public override void CleanUp()
        {
            gameProgression.OnAllCardPacksUnlocked -= HandleOnGameConditionMet;
        }
    }
}

