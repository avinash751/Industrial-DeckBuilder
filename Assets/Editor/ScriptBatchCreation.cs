using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class ScriptBatchCreation : EditorWindow
{
    public enum ScriptTemplateType
    {
        PlainClass,
        MonoBehaviour,
        ScriptableObject,
        Interface,
        Custom
    }

    public enum ScriptKind
    {
        Class,
        Interface,
        ScriptableObject
    }

    [System.Serializable]
    public class ScriptEntry
    {
        public string scriptName = "NewScript";
        public ScriptTemplateType templateType = ScriptTemplateType.MonoBehaviour;

        // Fields for Custom Template
        public ScriptKind scriptKind = ScriptKind.Class;
        public string inheritsFrom = "";
    }

    [System.Serializable]
    public class ScriptBatch
    {
        public DefaultAsset folder; // Reference to the folder
        public List<ScriptEntry> scripts = new List<ScriptEntry>(); // List of script entries
    }

    // List to hold multiple script batches
    public List<ScriptBatch> scriptBatches = new List<ScriptBatch>();

    // Add menu item to open the window
    [MenuItem("Tools/Script Batch Creation")]
    public static void ShowWindow()
    {
        GetWindow<ScriptBatchCreation>("Script Batch Creation");
    }

    private Vector2 scrollPos;

    private void OnGUI()
    {
        GUILayout.Label("Script Batch Creation", EditorStyles.boldLabel);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        // Iterate over each script batch
        for (int i = 0; i < scriptBatches.Count; i++)
        {
            EditorGUILayout.BeginVertical("box");
            ScriptBatch batch = scriptBatches[i];

            // Subheading for the script batch
            GUILayout.Label($"Script Batch {i + 1}", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();

            batch.folder = (DefaultAsset)EditorGUILayout.ObjectField("Folder", batch.folder, typeof(DefaultAsset), false);

            if (GUILayout.Button("Remove Batch"))
            {
                scriptBatches.RemoveAt(i);
                break;
            }
            EditorGUILayout.EndHorizontal();

            // Divider after folder selection
            GUILayout.Space(5);
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            GUILayout.Space(2);

            // "Scripts to Create" heading
            GUILayout.Space(10);
            GUILayout.Label("Scripts to Create:", EditorStyles.boldLabel);
            GUILayout.Space(5);

            // Iterate over each script entry
            for (int j = 0; j < batch.scripts.Count; j++)
            {
                EditorGUILayout.BeginVertical("box");

                // Divider between scripts
                if (j > 0)
                {
                    GUIStyle dividerStyle = new GUIStyle();
                    dividerStyle.normal.background = EditorGUIUtility.whiteTexture;
                    dividerStyle.margin = new RectOffset(0, 0, 4, 4);
                    dividerStyle.fixedHeight = 3;
                    Color originalColor = GUI.color;
                    GUI.color = Color.grey;
                    GUILayout.Box(GUIContent.none, dividerStyle, GUILayout.ExpandWidth(true));
                    GUI.color = originalColor;
                }

                EditorGUILayout.BeginHorizontal();

                GUIStyle boldColoredLabel = new GUIStyle(GUI.skin.label);
                boldColoredLabel.fontStyle = FontStyle.Bold;
                boldColoredLabel.normal.textColor = Color.grey;
                GUILayout.Label("Script " +j.ToString(),boldColoredLabel, GUILayout.Width(50));


                // Script Name Field
                batch.scripts[j].scriptName = EditorGUILayout.TextField(batch.scripts[j].scriptName);

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();

                // "Template Type" label
                GUILayout.Label("Template Type", GUILayout.Width(100));

                // Template Type Dropdown
                batch.scripts[j].templateType = (ScriptTemplateType)EditorGUILayout.EnumPopup(
                    batch.scripts[j].templateType, GUILayout.Width(120));

                // Remove Script Button
                if (GUILayout.Button("Remove Script"))
                {
                    batch.scripts.RemoveAt(j);
                    break;
                }

                EditorGUILayout.EndHorizontal();

                // Additional Fields for Custom Template
                if (batch.scripts[j].templateType == ScriptTemplateType.Custom)
                {
                    EditorGUILayout.Space();

                    // Script Kind Dropdown
                    batch.scripts[j].scriptKind = (ScriptKind)EditorGUILayout.EnumPopup("Script Type",
                        batch.scripts[j].scriptKind);

                    // Inherits From Field
                    batch.scripts[j].inheritsFrom = EditorGUILayout.TextField("Inherits From",
                        batch.scripts[j].inheritsFrom);

                    EditorGUILayout.Space();
                }

                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("Add Script"))
            {
                batch.scripts.Add(new ScriptEntry());
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Add Script Batch"))
        {
            scriptBatches.Add(new ScriptBatch());
        }

        if (GUILayout.Button("Create Scripts"))
        {
            CreateScripts();
        }
    }

    private void CreateScripts()
    {
        foreach (var batch in scriptBatches)
        {
            if (batch.folder == null)
            {
                Debug.LogWarning("No folder assigned for a script batch. Skipping this batch.");
                continue;
            }

            string folderPath = AssetDatabase.GetAssetPath(batch.folder);

            foreach (var scriptEntry in batch.scripts)
            {
                string scriptName = scriptEntry.scriptName;

                // Validate script name
                if (string.IsNullOrEmpty(scriptName))
                {
                    Debug.LogWarning("Script name is empty. Skipping this script.");
                    continue;
                }
                if (!IsValidScriptName(scriptName))
                {
                    Debug.LogWarning($"Invalid script name '{scriptName}'. Skipping this script.");
                    continue;
                }

                string scriptPath = Path.Combine(folderPath, scriptName + ".cs");

                if (File.Exists(scriptPath))
                {
                    Debug.LogWarning($"Script '{scriptName}' already exists in '{folderPath}'. Skipping.");
                    continue;
                }

                string scriptContent = GenerateScriptContent(scriptEntry);
                File.WriteAllText(scriptPath, scriptContent);
                AssetDatabase.ImportAsset(scriptPath);
                Debug.Log($"Created script '{scriptName}' in '{folderPath}'.");
            }
        }

        AssetDatabase.Refresh();
    }

    private string GenerateScriptContent(ScriptEntry scriptEntry)
    {
        string scriptName = scriptEntry.scriptName;
        ScriptTemplateType templateType = scriptEntry.templateType;

        switch (templateType)
        {
            case ScriptTemplateType.PlainClass:
                return
    $@"public class {scriptName}
{{
    // Your code here
}}";

            case ScriptTemplateType.MonoBehaviour:
                return
    $@"using UnityEngine;

public class {scriptName} : MonoBehaviour
{{
    // Start is called before the first frame update
    void Start()
    {{

    }}

    // Update is called once per frame
    void Update()
    {{

    }}
}}";

            case ScriptTemplateType.ScriptableObject:
                return
    $@"using UnityEngine;

[CreateAssetMenu(fileName = ""{scriptName}"", menuName = ""ScriptableObjects/{scriptName}"", order = 1)]
public class {scriptName} : ScriptableObject
{{
    // Your code here
}}";

            case ScriptTemplateType.Interface:
                return
    $@"public interface {scriptName}
{{
    // Define interface methods and properties here
}}";

            case ScriptTemplateType.Custom:
                return GenerateCustomScriptContent(scriptEntry);

            default:
                return string.Empty;
        }
    }

    private string GenerateCustomScriptContent(ScriptEntry scriptEntry)
    {
        string scriptName = scriptEntry.scriptName;
        ScriptKind scriptKind = scriptEntry.scriptKind;
        string inheritsFrom = scriptEntry.inheritsFrom;

        string inheritanceClause = "";

        if (!string.IsNullOrEmpty(inheritsFrom))
        {
            inheritanceClause = $" : {inheritsFrom}";
        }

        switch (scriptKind)
        {
            case ScriptKind.Class:
                return
    $@"public class {scriptName}{inheritanceClause}
{{
    // Your code here
}}";

            case ScriptKind.Interface:
                return
    $@"public interface {scriptName}{inheritanceClause}
{{
    // Define interface methods and properties here
}}";

            case ScriptKind.ScriptableObject:
                return
    $@"using UnityEngine;

[CreateAssetMenu(fileName = ""{scriptName}"", menuName = ""ScriptableObjects/{scriptName}"", order = 1)]
public class {scriptName}{inheritanceClause}
{{
    // Your code here
}}";

            default:
                return string.Empty;
        }
    }

    private bool IsValidScriptName(string scriptName)
    {
        // Simple validation to check if the script name starts with a letter or underscore
        if (string.IsNullOrEmpty(scriptName))
            return false;

        if (!char.IsLetter(scriptName[0]) && scriptName[0] != '_')
            return false;

        // Further validation can be added here if necessary

        return true;
    }
}
