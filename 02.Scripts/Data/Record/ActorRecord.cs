[System.Serializable]
public class ActorRecord 
{
    public string name;
    public Enums.ActorType actor;
    public int maxHp;
    public float moveSpeed;
    public int atk;
    public int def;
    public float maxRange;
    public float minRange;
    public float atkTime;
    public float field;
}
[System.Serializable]
public class ActorData
{
    public int id;
    public string name;
    public Enums.ActorType actor;
    public int maxHp;
    public float moveSpeed;
    public int atk;
    public int def;
    public float maxRange;
    public float minRange;
    public float atkTime;  //연사속도
    public float field;
}