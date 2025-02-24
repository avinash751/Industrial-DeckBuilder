using UnityEngine;
using GameManagerSystem.Configuration;

namespace GameManagerSystem.GameBehaviors
{
    public class StartBehavior : GameBehaviorBase
    {
        public override void ExecuteBehavior()
        {
            ApplyBaseBehaviorSettings(BehaviourConfigSO, GameBehaviorEventType.Initialized);
        }
    }
}