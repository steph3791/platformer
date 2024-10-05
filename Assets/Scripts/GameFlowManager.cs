using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinLevelPanel;
    [SerializeField] private GameObject gameFinishedPanel;
    
    public static GameFlowManager Instance;
    private int _currentLevel = 0;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ManageGameOver(Vector3 position)
    {
        Instantiate(gameOverPanel, position, Quaternion.identity);
    }

    public void ManageWinLevel(Vector3 position)
    {
        Instantiate(gameWinLevelPanel, position, Quaternion.identity);
    }

    public void ManageFinishedGame(Vector3 position)
    {
        Instantiate(gameFinishedPanel, position, Quaternion.identity);
    }

    public void LoadNextLevel(Vector3 position)
    {
        _currentLevel += 1;
        if (_currentLevel >= SceneManager.sceneCountInBuildSettings)
        {
            ManageFinishedGame(position);
        }
        else
        {
            SceneManager.LoadSceneAsync(_currentLevel);
        }
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the application if in a build
        Application.Quit();
#endif
    }


}
