using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorVison : MonoBehaviour
{    
    Actor actor;
    SphereCollider coll;    
    public bool isVisor { get; set; }
    public void Init()
    {
        //actor = GetComponentInParent<Actor>();       
        //coll=gameObject.AddComponent<SphereCollider>();
        //gameObject.AddComponent<Rigidbody>().isKinematic = true;
        //coll.center = new Vector3(0, 0, 0);
        //coll.radius = actor.ActorInfo.field*2;
        //coll.isTrigger = true;        
    }

    static List<Actor> actors=new();
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (actor.m_team != Enums.TEAM.PLAYER)
    //        return;         
    //    if ( other.gameObject.layer == LayerMask.NameToLayer("Unit") || other.gameObject.layer == LayerMask.NameToLayer("Building"))
    //    {            
    //        var theOtherActor = other.GetComponent<Actor>();
    //        if (actors.Contains(theOtherActor) != false)
    //            actors.Add(theOtherActor);
    //        //if(theOtherActor.m_team==Enums.TEAM.ENMEY  && theOtherActor.vision.isVisor == false)
    //        //{                
    //        //    theOtherActor.GetComponent<Actor>().vision.RendereOn();           
    //        //}            
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (actor.m_team != Enums.TEAM.PLAYER)
    //        return;
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Unit") || other.gameObject.layer == LayerMask.NameToLayer("Building"))
    //    {
    //        var theOtherActor = other.GetComponent<Actor>();
    //        if (actors.Contains(theOtherActor))
    //            actors.Remove(theOtherActor);                  
    //    }
    //}

    //private void Update()
    //{
    //    foreach(var item in actors)
    //    {
    //        if(item.m_team==Enums.TEAM.ENMEY  && item.vision.isVisor == false)
    //        {
    //            item.GetComponent<Actor>().vision.RendereOn();           
    //        }  
    //        else
    //            item.GetComponent<Actor>().vision.RendereOff();
    //    }
    //}

    //public void RendereOn()
    //{
    //    isVisor = true;
    //    var renderes = actor.renderes;              
    //    actor.hPbar.HpUI.GetComponent<Canvas>().enabled = true;
    //    foreach (var render in renderes)
    //        render.enabled = true;
    //}

    //public void RendereOff()
    //{
    //    isVisor = false;
    //    var renderes = actor.renderes;        
    //    actor.hPbar.HpUI.GetComponent<Canvas>().enabled = false;
    //    foreach (var render in renderes)
    //        render.enabled = false;
    //}

}
