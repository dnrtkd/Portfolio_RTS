using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Text : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    public Color TextColor { get { return text.color; } set { text.color = value; } }

    public void Set(string context)
    {
        text.text = context;
    }    
}
