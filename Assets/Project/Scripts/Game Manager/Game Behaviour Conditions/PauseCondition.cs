using UnityEngine;
using GameManagerSystem.GameBehaviors.Conditions;
using GameManagerSystem.Configuration;
using GameManagerSystem;
using GameManagerSystem.GameBehaviors;

[System.Serializable]
public class PauseCondition : GameCondition
{
    [HideInInspector][SerializeField] PauseBehaviorConfigSO pauseConfig;
    [SerializeField] string pauseAudiokey;
    [SerializeField] string unpauseAudioKey;

    public PauseCondition(GameManager _gameManager, PauseBehaviorConfigSO _pauseConfig) : base(_gameManager)
    {
        conditionName = "Pause Condition";
        pauseConfig = _pauseConfig;
    }

    public override void Initialize()
    {

    }

    public override void OnUpdate(float deltaTime = 0)
    {
        if (Input.GetKeyDown(pauseConfig.PauseKey))
        {
            TriggerGameConditionMet();
        }
    }

    protected override void HandleOnGameConditionMet()
    {
        if (gameManager.CurrentBehavior is PlayBehavior)
        {
            gameManager?.PauseGame();
            AudioManager.Instance?.PlayAudio(pauseAudiokey);
        }
        else if (gameManager.CurrentBehavior is PauseBehavior)
        {
            gameManager?.PlayGame();
            AudioManager.Instance?.PlayAudio(unpauseAudioKey);
        }
    }
}
