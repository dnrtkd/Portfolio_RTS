using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI_Group���� ������ ������ ���� ��Ÿ��
/// </summary>
public class UI_GroupImage : UI_Image
{
    public int num=0;
    [SerializeField]
    TextMeshProUGUI numText;
    public void Set(Enums.UnitType type)
    {
        Set(ResUtil.EnumToString(type));
    }    

    public void PlusNum()
    {
        num++;
        numText.text = num.ToString();
    }

    public void SubNum()
    {
        num--;
        numText.text = num.ToString();
    }
}
