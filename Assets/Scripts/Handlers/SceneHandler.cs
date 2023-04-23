using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void LoadCardScene(string sceneName)
    {
        SetDataForSceneChange();
        SceneManager.LoadScene(sceneName);
    }

    private void SetDataForSceneChange()
    {
        DataManager.Instance.test = 39;
    }
}
