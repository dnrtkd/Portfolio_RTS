using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ǹ��� ������ �ٴ� ��ũ��Ʈ
public class Building : Actor
{    
    //���� ���� ���� , �ǹ� ũ�⺸�� ���� ���� �� ����

    private int m_sizeX;
    private int m_sizeZ;    
    
    private float m_processTime; // �ǹ� ���� �ð�    
    private Vector3 m_firstVirtice;
    private GameObject[] m_anotherBuildings = new GameObject[4]; //�Ǽ� ��ô���� ���� 4���� �ǹ� ����
    
    public int SizeX { get { return m_sizeX; } set { m_sizeX = value; }  }
    public int SizeZ { get { return m_sizeZ; } set { m_sizeZ = value; } }
    public float ProcessTime { get { return m_processTime; }  set { m_processTime = value; } }    
    public bool isComplete { get; set; }
    public Enums.BuildingType type;

    public Building_Effect Effect;
    public override void Init()
    {
        base.Init();
        m_firstVirtice = new Vector3(-(SizeX), 0, -(SizeZ)) * 0.5f;
        
        for (int i = 0; i < 4; i++)
        {
            m_anotherBuildings[i] = transform.GetChild(i).gameObject;
        }
        fsm.AddState(new BuildingState_Idle(this));
        fsm.AddState(new BuildingState_Damaged(this));
        fsm.AddState(new BuildingState_Destroy(this,3.0f));
        fsm.SetState(Actor.State.IDLE);

        Effect = GetComponent<Building_Effect>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        fsm.OnUpdate();
    }
    public override ActorCtrl AddCtrlComponent()
    {
        return AddCtrlComponent<BuildingCtrl>();
    }
    public override void Open(Vector3 pos,Enums.TEAM team)
    {
    }
    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(m_firstVirtice);
    }
    public void changeBuildingShpae(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == index)
                m_anotherBuildings[i].SetActive(true);
            else
                m_anotherBuildings[i].SetActive(false);
        }
    }    
}
