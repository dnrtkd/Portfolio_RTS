using UnityEngine;
using UnityEngine.UI;

//유닛 생산 슬라이더 UI
public class UI_CurrentUnitBar : MonoBehaviour, IObserver
{
    Unit_Creator creator;

    [SerializeField]
    Slider slider;

    public void Connet(Unit_Creator creator)
    {
        this.creator = creator;
        creator.Attach(this);
    }

    public void Init()
    {
        //slider.
        SetValue(creator.Ratio);
    }

    public void Notify(ISubject _subject)
    {
        SetValue(creator.Ratio);        
    }

    void SetValue(float ratio) => slider.value = creator.Ratio;
}
