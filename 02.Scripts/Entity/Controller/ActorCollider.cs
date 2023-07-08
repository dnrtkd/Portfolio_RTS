using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorCollider : MonoBehaviour
{
    Actor actor;
    BoxCollider coll;    
    public void Init()
    {
        actor = GetComponent<Actor>();
        coll = GetComponent<BoxCollider>();
        gameObject.AddComponent<Rigidbody>().isKinematic = true;        
    }



    private void OnCollisionEnter(Collision collision)
    {
        
        
    }
}
