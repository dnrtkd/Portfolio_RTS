using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //ÇöÀç ¾À
    BaseScene currentScene;

    private void Start()
    {
        DataManager.Instance.Init();
        SoundManager.Instance.Init();
        // GameObject obj = new GameObject("@Scene");
        currentScene =GameObject.FindObjectOfType<LobbyScene>();
        currentScene.Init();
        
    }
    private void Update()
    {
        currentScene.OnUpdate();
    }

    public void LoadScene(Enums.SceneState state)
    {
        //currentScene.Clear();
        Destroy(currentScene);
        SceneManager.LoadScene(GetSceneName(state));        

        StartCoroutine(Co_LoadScene(state));
    }

    IEnumerator Co_LoadScene(Enums.SceneState state)
    {
        while(SceneManager.GetActiveScene().name!= GetSceneName(state))
        {
            yield return null;
        }

        switch (state)
        {
            case Enums.SceneState.Lobby:
                currentScene = FindObjectOfType<LobbyScene>();
                break;
            case Enums.SceneState.Game:
                currentScene = FindObjectOfType<GameScene>();
                break;
        }
    }

    string GetSceneName(Enums.SceneState state)
    {
        return System.Enum.GetName(typeof(Enums.SceneState), state);
    }

}
