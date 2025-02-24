using UnityEngine;

namespace GameManagerSystem.GameBehaviors
{
    public class WinBehavior : GameBehaviorBase
    { 
        public override void ExecuteBehavior()
        {
            ApplyBaseBehaviorSettings(BehaviourConfigSO, GameBehaviorEventType.Lose);
        }
    }
}