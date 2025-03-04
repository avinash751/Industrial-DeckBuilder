using GameManagerSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseGameBehaviourConfigSO", menuName = "Game Manager/Behavior Configs/BaseGameBehaviourConfigSO")]
public class BaseGameBehaviourConfigSO : ScriptableObject
{
    [Header("Base Behavior Configuration")]
    public GameBehaviorType BehaviorType;
    public bool IsTimeZeroOnExecution = true;
    public bool IsCursorLockedOnExecution = true;
    public bool IsCursorVisibleOnExecution = true;
    public bool LoadSceneOnExecution = false;
    public bool ShowGameUIOnExecution = false;
    public int SceneToLoad = 0;
}
