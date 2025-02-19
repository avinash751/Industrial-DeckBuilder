using UnityEngine;

namespace GameManagerSystem.Configuration
{
    [CreateAssetMenu(fileName = "PlayBehaviorConfig", menuName = "Game Manager/Behavior Configs/Play Behavior Config", order = 2)]
    public class PlayBehaviorConfigSO : ScriptableObject
    {
        [Header("Play Behavior Configuration")]
        public bool setTimescaleToOneOnPlay = true;
        public bool enableCursorLockModeOnPlay = true;
        public bool disableCursorVisibilityOnPlay = true;
        public bool loadSceneOnPlay = false;
        public int playSceneIndex = 1;
    }
}