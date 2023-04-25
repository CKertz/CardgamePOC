using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    private DeckHandler deckHandler = new DeckHandler();

    public void LoadCardScene(string sceneName)
    {
        deckHandler.constructDeck();
        
        SceneManager.LoadScene(sceneName);
    }


    private void SetDataForSceneChange()
    {
        DataManager.Instance.test = 39;
    }
}
