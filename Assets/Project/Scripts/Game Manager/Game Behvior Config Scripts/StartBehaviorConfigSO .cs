using UnityEngine;

namespace GameManagerSystem.Configuration
{
    [CreateAssetMenu(fileName = "StartBehaviorConfig", menuName = "Game Manager/Behavior Configs/Start Behavior Config", order = 1)]
    public class StartBehaviorConfigSO : ScriptableObject
    {
        [Header("Start Behavior Configuration")]
        public bool SetTimescaleToZeroOnStart = true;
        public bool DisableCursorLockModeOnStart = true;
        public bool EnableCursorVisibilityOnStart = true;
        public bool LoadStartSceneOnStart = false;
        public int StartSceneIndex = 0;
    }
} 