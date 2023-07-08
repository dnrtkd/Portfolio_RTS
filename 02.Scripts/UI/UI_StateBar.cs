using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_StateBar : MonoBehaviour, IObserver
{
    [SerializeField]
    TextMeshProUGUI WoodText;

    [SerializeField]
    TextMeshProUGUI FoodText;
  
    WealthDataCollection wealth;
    public void Connect(WealthDataCollection wealth)
    {
        this.wealth = wealth;
        wealth.Attach(this);
        Init();
    }

    public void Init()
    {
        WoodText.text = wealth.GetCount(WealthData.WEALTH_TYPE.Wood).ToString();
        FoodText.text = wealth.GetCount(WealthData.WEALTH_TYPE.Food).ToString();        
    }

    public void Notify(ISubject _subject)
    {
        WoodText.text = wealth.GetCount(WealthData.WEALTH_TYPE.Wood).ToString();
        FoodText.text = wealth.GetCount(WealthData.WEALTH_TYPE.Food).ToString();        
    }
}
