using UnityEngine;
using UnityEngine.UI;

public class UI_Image : MonoBehaviour
{
    [SerializeField]
    protected Image image;

    //��������Ʈ�� ���� �Ҵ� ��
    public void Set(string fileName)
    {
        var sprite = ResUtil.Load<Sprite>("Prefab/UI/" + fileName);
        image.sprite = GameObject.Instantiate<Sprite>(sprite, transform);
    }
}
