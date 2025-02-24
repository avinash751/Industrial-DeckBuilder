using UnityEngine;
using GameManagerSystem.Configuration;

namespace GameManagerSystem.GameBehaviors
{
    public class LoseBehavior : GameBehaviorBase
    {
        public override void ExecuteBehavior()
        {
           ApplyBaseBehaviorSettings(BehaviourConfigSO, GameBehaviorEventType.Lose);
        }
    }
}