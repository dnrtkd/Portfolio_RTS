using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UnitMove : MonoBehaviour
{
    Unit m_owner;
    public Vector3 m_target;
    NavMeshAgent agent;
    float speed;
    Vector3 moveDir;    

    public bool isMove;
    public void Init()
    {                
        m_target = Vector3.zero;
        m_owner = GetComponent<Unit>();
        speed = m_owner.ActorInfo.moveSpeed;
        agent = m_owner.GetComponent<NavMeshAgent>();
        //자동회전 기능 끄기
        agent.updateRotation = false;
        //agent.acceleration = 0;       
    }
    public void MoveStop()
    {
        //회피 우선순위
        agent.avoidancePriority--;
        agent.isStopped = true;       
        isMove = false;        
        m_owner.m_Animator.SetBool("isWork",false);        
        //agent.isStopped = true;
    }    
    public void OnUpdate()
    {
        //if (isMove == false)
        //    return;        

        //transform.position += Time.deltaTime * speed * moveDir;
        //var curDir = (m_target - transform.position).normalized;
        //var dot = Vector3.Dot(moveDir, curDir);

        if(agent.desiredVelocity.sqrMagnitude>=0.1f*0.1f)
        {            
            Vector3 dir = agent.desiredVelocity;

            Quaternion targetAngle = Quaternion.LookRotation(dir);

            transform.rotation = targetAngle;
        }        
        if (agent.velocity.sqrMagnitude >= 0.1f*0.1f && agent.remainingDistance<=0.1f)
        {
            MoveStop();
        }        
    }
    public void SetTarget(Vector3 pos)
    {
        agent.avoidancePriority++;       
        agent.isStopped = false;        
        m_target = pos;
        moveDir = Vector3.Normalize(m_target - transform.position);
        transform.LookAt(m_target);        
        agent.SetDestination(m_target);
        m_owner.m_Animator.SetBool("isWork", true);
        isMove = true;
        
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(m_owner.Position, m_target);
    }
    public void SetTarget(Actor actor)
    {

    }
}
