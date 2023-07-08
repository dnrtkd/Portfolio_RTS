using UnityEngine;
using UnityEngine.UI;

public class UI_Image : MonoBehaviour
{
    [SerializeField]
    protected Image image;

    //스프라이트를 동적 할당 함
    public void Set(string fileName)
    {
        var sprite = ResUtil.Load<Sprite>("Prefab/UI/" + fileName);
        image.sprite = GameObject.Instantiate<Sprite>(sprite, transform);
    }
}
