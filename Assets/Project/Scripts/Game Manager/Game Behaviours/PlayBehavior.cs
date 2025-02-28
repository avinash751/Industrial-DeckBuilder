using UnityEngine;

namespace GameManagerSystem.GameBehaviors
{
    [System.Serializable]
    public class PlayBehavior : GameBehaviorBase
    {
        public PlayBehavior(GameManager _gameManager, BaseGameBehaviourConfigSO _behaviourConfigSO) : base(_gameManager, _behaviourConfigSO)
        {
        }
        public override void ExecuteBehavior()
        {
            ApplyBehaviorSettings(BehaviourConfigSO, GameBehaviorEventType.GameStarted);
        }
    }
}