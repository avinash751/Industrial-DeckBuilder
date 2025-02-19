using UnityEngine;

namespace GameManagerSystem.Configuration
{
    [CreateAssetMenu(fileName = "WinBehaviorConfig", menuName = "Game Manager/Behavior Configs/Win Behavior Config", order = 3)]
    public class WinBehaviorConfigSO : ScriptableObject
    {
        [Header("Win Behavior Configuration")]
        public bool SetTimescaleToZeroOnWin = false;
        public bool DisableCursorLockModeOnWin = true;
        public bool EnableCursorVisibilityOnWin = true;
        public bool LoadSceneOnWin = false;
        public int WinSceneIndex = 0;
    }
}