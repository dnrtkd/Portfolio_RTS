using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTiles : MonoBehaviour
{
    float speed;

    //Vector3 des;
    Vector3 moveDir;
    Actor target;
    float damage;
    Queue<Vector3> points=new();
    Vector3 currDes;
    public void SetTarget(Vector3[] points , Actor target, float damage)
    {
        //this.des = _des;
        //transform.LookAt(des);
        //moveDir = (_des - transform.position).normalized;
        currDes = points[1];
        moveDir= (currDes - transform.position).normalized;
        transform.LookAt(currDes);
        for (int i = 2; i < points.Length; ++i)
            this.points.Enqueue(points[i]);
        this.target = target;
        this.damage = damage;
    }

    public void SetTarget(Vector3 dir,float Speed,Actor target,float damage)
    {
        direction = dir;
        this.speed = Speed;
        this.target = target;
        this.damage = damage;
        firePos = this.transform.position;
    }
    float timeElapsed;
    Vector3 firePos;
    Vector3 direction;
    void Update()
    {
        timeElapsed += Time.deltaTime;
        Vector3 nextMove= firePos + direction * speed * timeElapsed;
        Vector3 gravity=Vector3.zero;
        gravity+= Physics.gravity * (timeElapsed * timeElapsed) / 2.0f;
        nextMove += gravity;
        transform.LookAt(nextMove);
        
        transform.position = nextMove;        
        if(transform.position.y<-0.5f)
        {            
            target.Damaged(damage);
            Destroy(this.gameObject);
            return;
        }

        //NextToPoint();
        //transform.position += Time.deltaTime * speed * moveDir;     
    }   

    void NextToPoint()
    {
        Vector3 curDir = (currDes - transform.position).normalized;

        if (Vector3.Dot(moveDir, curDir) >= 0)
            return;

        if (points.Count == 0)
        {
            target.Damaged(damage);
            Destroy(this.gameObject);
            return;
        }

        currDes = points.Dequeue();
        moveDir = (currDes - transform.position).normalized;
        transform.LookAt(currDes);

        NextToPoint();
    }
}
