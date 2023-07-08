using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitCreator 
{
    public static Unit Create(Enums.UnitType type,WealthDataCollection wealth,Vector3 pos,Enums.TEAM team)
    {
        var data=DataManager.Instance.produceUnitTable.GetItem(type);
        
        if(isEnoughCost(type,wealth,data))
        {
            var unit=GameScene.I.ActorMng.CreateUnit(type, pos, team).GetComponent<Unit>();

            wealth.SubCount(WealthData.WEALTH_TYPE.Food, data.food);
            wealth.SubCount(WealthData.WEALTH_TYPE.Wood, data.wood);
            return unit;
        }

        return null;
    }

    public static bool isEnoughCost(Enums.UnitType type, WealthDataCollection wealth ,ProduceUnitData data)
    {
        if (data == null)
        {
            Debug.Log(" 해당 UnitType의 정보를 확인할 수 없습니다.");
            return false;
        }
        var food = data.food;
        var wood = data.wood;
        var pop = data.Pop;

        if (wealth.GetCount(WealthData.WEALTH_TYPE.Food) >= food &&
            wealth.GetCount(WealthData.WEALTH_TYPE.Wood) >= wood  )
            return true;
        else
            return false;
    }
}
