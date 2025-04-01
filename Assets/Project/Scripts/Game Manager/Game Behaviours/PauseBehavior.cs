using UnityEngine;
using GameManagerSystem.Configuration;
using GameManagerSystem.UI;
using GameManagerSystem.GameBehaviors.Conditions;

namespace GameManagerSystem.GameBehaviors
{
    [System.Serializable]
    public class PauseBehavior : GameBehaviorBase
    {
        [HideInInspector][SerializeField] private PauseBehaviorConfigSO config;
        [HideInInspector][SerializeField] private PauseCondition pauseCondition;
        public PauseBehavior(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO, PrimaryMenusUIManager menuUiManager) : base(_gameManager, _behaviourConfigSO, menuUiManager)
        {
            config = (PauseBehaviorConfigSO)BehaviourConfigSO;
            pauseCondition = new PauseCondition(_gameManager, config);
        }

        protected override void OnEnter()
        {
            AudioManager.Instance?.PlayAudio(config.PauseAudiokey);
        }

        public override void Exit()
        {
            InvokeOnBehaviorEvent(this);
            AudioManager.Instance?.PlayAudio(config.UnPauseAudioKey);
        }

        protected override void SetMenuSettings()
        {
            menuUiManager.HideAllGameMenus();
            menuUiManager.ShowPauseMenu();
        }

        public override GameCondition GetGameCondition()
        {
            return pauseCondition;
        }
    }
}