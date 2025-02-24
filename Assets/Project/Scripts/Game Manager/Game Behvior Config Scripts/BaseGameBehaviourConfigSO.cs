using UnityEngine;

[CreateAssetMenu(fileName = "BaseGameBehaviourConfigSO", menuName = "Game Manager/Behavior Configs/Scriptable Objects/BaseGameBehaviourConfigSO")]
public class BaseGameBehaviourConfigSO : ScriptableObject
{
    [Header("Base Behavior Configuration")]
    public bool IsTimeZeroOnExecution = true;
    public bool IsCursorLockedOnExecution = true;
    public bool IsCursorVisibleOnExecution = true;
    public bool LoadSceneOnExecution = false;
    public int SceneToLoad = 0;
}
