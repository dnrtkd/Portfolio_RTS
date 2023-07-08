using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class LobbyScene : BaseScene
{
    [SerializeField]
    GameObject GameSetPannel;

    Button Button_GameStart;
    Button Button_Option;
    Button Button_Quit;
     
    public override void Init()
    {
        Button_GameStart = GameObject.Find("Button_Campain").GetComponent<Button>();
        Button_Option = GameObject.Find("Button_Option").GetComponent<Button>();
        Button_Quit= GameObject.Find("Button_Quit").GetComponent<Button>();           

        if (Button_GameStart == null ||  Button_Option ==null  || Button_Quit == null)
            Debug.LogError("Button 이 씬에 존재하지 않습니다.");

        base.Init();

        Button_GameStart.onClick.AddListener(OnClick_GameStart);
        Button_Option.onClick.AddListener(OnClick_Option);
        Button_Quit.onClick.AddListener(OnClick_Quit);
    }

    public void OnClick_GameStart()
    {
        GameSetPannel.SetActive(true);
    }

    public void OnClick_Option()
    {

    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }
}


//맵 파일을 불러오는 클래스
 public static class MapFileLoder
{
    public static string MapName;

    public static RTS_Map[] RTS_MapLoadAll()
    {
        return Resources.LoadAll<RTS_Map>("Data/MapFolder");
    }

    public static Sprite[] RTS_TextureLoadAll()
    {

        Texture2D[] textures=Resources.LoadAll<Texture2D>("Data/MapFolder");
        List<Sprite> sprites=new();
        for (int i = 0; i < textures.Length; i++)
        {
            Sprite sprite = Sprite.Create(textures[i], 
                new Rect(30, 30, 226, 226), new Vector2(0.5f, 0.5f));
            sprites.Add(sprite);
        }
        return sprites.ToArray();
    }

    public static RTS_Map RTS_MapLoad()
    {
        return RTS_MapLoad(MapName);
    }

    public static RTS_Map RTS_MapLoad(string path)
    {
        return Resources.Load<RTS_Map>("Data/MapFolder/"+path);
    }
}
