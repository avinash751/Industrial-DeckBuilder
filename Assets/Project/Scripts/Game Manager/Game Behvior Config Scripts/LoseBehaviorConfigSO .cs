using UnityEngine;

namespace GameManagerSystem.Configuration
{
    [CreateAssetMenu(fileName = "LoseBehaviorConfig", menuName = "Game Manager/Behavior Configs/Lose Behavior Config", order = 4)]
    public class LoseBehaviorConfigSO : ScriptableObject
    {
        [Header("Lose Behavior Configuration")]
        public bool SetTimescaleToZeroOnLose = false;
        public bool DisableCursorLockModeOnLose = true;
        public bool EnableCursorVisibilityOnLose = true;
        public bool LoadSceneOnLose = false;
        public int LoseSceneIndex = 0;
    }
}