using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_GameSet : MonoBehaviour
{   
    [SerializeField]
    Button startButton;   
    
    [SerializeField]
    Button backButton;

    [SerializeField]
    RectTransform Content;

    [SerializeField]
    Image mapImage;

    [SerializeField]
    TextMeshProUGUI nameText;

    [SerializeField]
    TextMeshProUGUI sizeText;
       
    RTS_Map[] mapInfos;
    Sprite[] mapTextures;

    int currentMap;

    public void Start()
    {
        mapInfos = MapFileLoder.RTS_MapLoadAll();
        mapTextures = MapFileLoder.RTS_TextureLoadAll();

        backButton.onClick.AddListener(() => 
        { 
            this.gameObject.SetActive(false);
            mapImage.gameObject.SetActive(false);
        });

        startButton.onClick.AddListener(() => 
        { GameManager.Instance.LoadScene(Enums.SceneState.Game); });
        Create();
    }

    void Create()
    {
        for(int i=0;i<mapInfos.Length;++i)
        {
            UI_Button_MapText button=ResUtil.Create
                ("Prefab/UI/UI_MapCatalButton",Content)
                .GetComponent<UI_Button_MapText>();

            button.Set(mapInfos[i].Name, mapInfos[i].MapSize, 
                mapTextures[i], nameText, sizeText, mapImage);
        }
    }

    
}
