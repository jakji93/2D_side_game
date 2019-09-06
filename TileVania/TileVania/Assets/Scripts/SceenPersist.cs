using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceenPersist : MonoBehaviour
{
    private void Awake()
    {
        var num = FindObjectsOfType<SceenPersist>().Length;
        if(num > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        } 
    }
    
    public void ExitToNextLevel()
    {
        Destroy(gameObject);
    }
}
