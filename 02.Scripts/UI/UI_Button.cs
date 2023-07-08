using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_Button : MonoBehaviour
{
    [SerializeField]
    protected Image image;
    [SerializeField]
    protected Button button;
    
    public void Set(string ImageName, UnityAction action)
    {
        var sprite = ResUtil.Load<Sprite>("Prefab/UI/" + ImageName);
        image.sprite = GameObject.Instantiate<Sprite>(sprite, transform);

        button.onClick.AddListener(action);
        gameObject.SetActive(true);
    }

    public void SetOff()
    {
        button.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }
}
