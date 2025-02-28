using UnityEngine;

namespace GameManagerSystem.GameBehaviors
{
    [System.Serializable]
    public class LoseBehavior : GameBehaviorBase
    {
      
        public LoseBehavior(GameManager _gameManager,BaseGameBehaviourConfigSO _behaviourConfigSO) : base(_gameManager, _behaviourConfigSO)
        {

        }

        public override void ExecuteBehavior()
        {           
            ApplyBehaviorSettings(BehaviourConfigSO, GameBehaviorEventType.Lose);
        }     
    }
}