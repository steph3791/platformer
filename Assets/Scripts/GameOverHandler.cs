using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private float fadeInDuration = 2f;
    
    
    private CanvasRenderer canvasGroup;
    
    private void OnEnable()
    {
        canvasGroup = gameObject.GetComponent<CanvasRenderer>();
        canvasGroup.SetAlpha(0f);
        StartCoroutine(GameOver());
    }
    
    
    private IEnumerator GameOver()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.SetAlpha(Mathf.Clamp01(elapsedTime / fadeInDuration));
            yield return null;
        }
        canvasGroup.SetAlpha(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
