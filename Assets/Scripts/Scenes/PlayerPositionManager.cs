using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    public static PlayerPositionManager Instance;

    public Vector3 lastPosition;
    public string lastScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePosition(Vector3 position, string sceneName)
    {
        lastPosition = position;
        lastScene = sceneName;
    }

    public Vector3 GetSavedPosition(string sceneName)
    {
        if (sceneName == lastScene)
            return lastPosition;

        return Vector3.zero;
    }
}
