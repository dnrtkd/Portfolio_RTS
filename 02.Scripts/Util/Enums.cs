using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums 
{
    public enum UnitType
    {
        farmer,
        sword,
        archer,
        lance,

        Paladin,
    }

    public enum MouseEvent
    {
        LeftClick,
        RightClick,
        DragStart,
        Drag,
        DragEnd,
        OnCurssor,
    }

    public enum GameEvent
    {
        START,
        PAUSE,
        RESTART,
        QUIT
    }

    public enum SceneState
    {
        Lobby,
        Game,
        Result
    }

    public enum BuildingType
    {
        castle,
        Barrack,
        Archery,
        Farm,
        Tower_1,
        Wall_1,
        Wall_2,
        WallCorner,        
        
    }

    public enum SelectedType
    {
        Unit,
        Building
    }
    public enum UI_Icon
    {
        sword,
        farmer,
    }

    public enum TEAM
    {
        NONE,
        PLAYER,
        ENMEY,        
    }

    public enum ActorType
    {
        CHARACTER,
        BUILDING,
        NEUTRALITY,
        RESOURCE
    }

    public enum MapPartType
    {
        Block = 1,
        environment = 101,
        Actor = 201,
    }
}
