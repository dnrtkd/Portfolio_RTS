using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 원거리 용 공격 클래스
/// </summary>
public class StandOffAttack : UnitAttack
{    
    ProjectTiles ProjectTile;
    //ProjectTiles가 발사될 시작 위치
    [SerializeField]
    Transform startPos;

    //높이
    [SerializeField]
    float MaxHeight;

    [SerializeField]
    float MinHeight;

    //베지어곡선 꼭짓점 수
    [SerializeField]
    int BezierVertexCount;

    private Vector3[] Points;
    private float Height;

    public void Start()
    {
        Points = new Vector3[BezierVertexCount];
    }
    protected override void Attack(Actor target)
    {
        int ind = Random.Range(0, audioClipKeys.Count - 1);
        SoundManager.Instance.PlayEffect(audioClipKeys[ind], transform.position);

        this.target = target;
        //타겟과 유닛의 거리로 높이값을 조절
        Height = Mathf.Lerp
            (MinHeight, MaxHeight, Actor.Distance(target, unit) / maxRange);      
        ProjectTile=ResUtil.Create("Prefab/ProjectTile/Arrow_A").GetComponent<ProjectTiles>();

        if(ProjectTile == null)
        {
            Debug.Log($"{GetComponent<Actor>().GetType()}/StandOffAttack : ProjectTile이 널입니다.");
        }
        ProjectTile.transform.position = startPos.position;
        ProjectTile.SetTarget(GetFireDirection(startPos.position, target.Position+
            new Vector3(0,target.GetComponent<BoxCollider>().bounds.center.y,0), 9.8f),9.8f,target,attValue);

        //BezierLine();
        //ProjectTile.transform.position = startPos.position;
        //ProjectTile.SetTarget(Points,target,attValue);        
    }
    
    public override void OnUpdate()
    {
        //임시코드




        base.OnUpdate();
        
    }

    Vector3 GetFireDirection(Vector3 startPos,Vector3 endPos,float speed)
    {
        Vector3 direction = Vector3.zero;
        Vector3 delta = endPos - startPos;
        float a = Vector3.Dot(Physics.gravity, Physics.gravity);
        float b = -4 * (Vector3.Dot(Physics.gravity, delta) + speed * speed);
        float c = 4 * Vector3.Dot(delta, delta);
        if (4 * a * c > b * b)
            return direction;
        float time0 = Mathf.Sqrt((-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a));
        float time1 = Mathf.Sqrt((-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a));

        float time;
        if (time0 < 0.0f)
        {
            if (time1 < 0)
                return direction;
            time = time1;
        }
        else
        {
            if (time1 < 0)
                time = time0;
            else
                time = Mathf.Min(time0, time1);
        }
        direction = 2 * delta - Physics.gravity * (time * time);
        direction = direction / (speed * 2 * time);
        return direction;
    }

    public void BezierLine()
    {
        Vector3 temp, dest;
        Vector3 start = startPos.position;
        Vector3 end = new Vector3(target.Position.x,start.y,target.Position.z);
        temp = new Vector3(start.x, Height, start.z);
        dest = new Vector3(end.x, Height, end.z);
        Vector3 temp2 = Vector3.Lerp(temp, dest, 0.1f);
        Vector3 dest2 = Vector3.Lerp(temp, dest, 0.9f);

        Vector3[] nodes = new Vector3[5];

        float ratio = 0.0f;
        Points[0] = start;
        for (int i = 1; i < BezierVertexCount-1; i++)
        {
            ratio = (float)i / (float)BezierVertexCount;            
            nodes[0] = Vector3.Lerp(start, temp2, ratio);
            nodes[1] = Vector3.Lerp(temp2, dest2, ratio);
            nodes[2] = Vector3.Lerp(dest2, end, ratio);
            nodes[3] = Vector3.Lerp(nodes[0], nodes[1], ratio);
            nodes[4] = Vector3.Lerp(nodes[1], nodes[2], ratio);

            Points[i] = Vector3.Lerp(nodes[3], nodes[4], ratio);
            Debug.Log(Points[i]);
        }
        Points[BezierVertexCount - 1] = end;
    }

    //private void OnDrawGizmos()
    //{
    //    for (int i = 1; i < BezierVertexCount; ++i)
    //    {
    //        Gizmos.DrawLine(Points[i-1], Points[i]);            
    //    }
    //}
}
