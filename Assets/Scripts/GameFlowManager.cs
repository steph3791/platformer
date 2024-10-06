using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinLevelPanel;
    [SerializeField] private GameObject gameFinishedPanel;
    
    public static GameFlowManager Instance;
    private int _currentLevel = 1;
    
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
        StartCoroutine(WaitForSeconds(3, () => SceneManager.LoadScene(0)));
    }

    public void ManageWinLevel(Vector3 position)
    {
        _currentLevel += 1;
        if (_currentLevel >= SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(WaitForSeconds(3, () => ManageFinishedGame(position)));
        }
        else
        {
            Instantiate(gameWinLevelPanel, position, Quaternion.identity);
            StartCoroutine(WaitForSeconds(3, () => SceneManager.LoadSceneAsync(_currentLevel)));
        }
    }
    

    public void ManageFinishedGame(Vector3 position)
    {
        Instantiate(gameFinishedPanel, position, Quaternion.identity);
    }

    public void GoToStartScene()
    {
        SceneManager.LoadSceneAsync(0);
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
    

    private IEnumerator WaitForSeconds(float seconds, Action then)
    {
        yield return new WaitForSeconds(seconds);
        then.Invoke();
    }


}
