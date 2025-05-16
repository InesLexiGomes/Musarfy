using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using System.IO;

public class ChangeScene : MonoBehaviour
{
    public string selectedScene;

    public void LoadSelectedScene()
    {
        if (!string.IsNullOrEmpty(selectedScene))
        {
            SceneManager.LoadScene(selectedScene);
        }
        else
        {
            Debug.LogWarning("Nenhuma cena selecionada.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<RatMovement>() != null)
            LoadSelectedScene();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ChangeScene))]
public class SceneSelectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChangeScene script = (ChangeScene)target;

        List<string> sceneNames = new List<string>();
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = Path.GetFileNameWithoutExtension(path);
            sceneNames.Add(name);
        }

        int currentIndex = Mathf.Max(0, sceneNames.IndexOf(script.selectedScene));
        int newIndex = EditorGUILayout.Popup("Selected Scene", currentIndex, sceneNames.ToArray());

        if (newIndex >= 0 && newIndex < sceneNames.Count)
        {
            string newScene = sceneNames[newIndex];
            if (newScene != script.selectedScene)
            {
                Undo.RecordObject(script, "Change Scene");
                script.selectedScene = newScene;
                EditorUtility.SetDirty(script);
            }
        }

        DrawDefaultInspector();
    }
}
#endif

