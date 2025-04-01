using UnityEngine;

namespace GameManagerSystem.Configuration
{
    [CreateAssetMenu(fileName = "PauseBehaviorConfig", menuName = "Game Manager/Behavior Configs/Pause Behavior Config", order = 5)]
    public class PauseBehaviorConfigSO :BaseGameBehaviourConfigSO
    {
        [Header("Pause Behaviour Settings")]
        public KeyCode PauseKey = KeyCode.Escape;
        public string PauseAudiokey;
        public string UnPauseAudioKey;
    }
}