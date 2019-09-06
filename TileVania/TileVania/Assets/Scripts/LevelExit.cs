using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadingTime = 2f;
    [SerializeField] float levelExitSlowMo = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        FindObjectOfType<SceenPersist>().ExitToNextLevel();
        Time.timeScale = levelExitSlowMo;
        yield return new WaitForSecondsRealtime(loadingTime);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
