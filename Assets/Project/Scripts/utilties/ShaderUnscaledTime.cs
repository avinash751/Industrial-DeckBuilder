using UnityEngine;

public class ShaderUnscaledTime : MonoBehaviour
{
    
    void Update()
    {
        Shader.SetGlobalFloat("_unscaledTime", Time.unscaledTime);
    }
}
