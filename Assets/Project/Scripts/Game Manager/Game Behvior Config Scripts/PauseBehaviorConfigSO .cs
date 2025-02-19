using UnityEngine;

namespace GameManagerSystem.Configuration
{
    [CreateAssetMenu(fileName = "PauseBehaviorConfig", menuName = "Game Manager/Behavior Configs/Pause Behavior Config", order = 5)]
    public class PauseBehaviorConfigSO : ScriptableObject
    {
        [Header("Pause Behavior Configuration")]
        public bool SetTimescaleToZeroOnPause = true;
        public bool DisableCursorLockModeOnPause = true;
        public bool EnableCursorVisibilityOnPause = true;

        [Header("Input Settings")]
        public KeyCode PauseKey = KeyCode.Escape;
    }
}