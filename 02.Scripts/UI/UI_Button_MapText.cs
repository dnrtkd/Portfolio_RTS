using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UI_Button_MapText : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI text;
    [SerializeField]
    protected Button button;

    string Name;
    Vector2 size;
    Sprite image;

    TextMeshProUGUI nameText;
    TextMeshProUGUI sizeText;
    Image mapImage;

    public void Set(string Context,Vector2 size,Sprite mapSp, 
        TextMeshProUGUI nameTex, TextMeshProUGUI sizeTex,Image mapImg )
    {
        Name = Context;
        text.text = Context;
        this.size = size;
        this.image = mapSp;
        this.nameText = nameTex;
        this.sizeText = sizeTex;
        this.mapImage = mapImg;
        
        gameObject.SetActive(true);

        button.onClick.AddListener(() => { nameTex.text = Name; sizeTex.text = $"¸Ê Å©±â {size.x} X {size.y}";
            MapFileLoder.MapName = Name; mapImage.sprite = mapSp;
            mapImage.gameObject.SetActive(true);
        });
    }    
}
