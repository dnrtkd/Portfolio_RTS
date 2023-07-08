using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SquaerFormation : FormationPattern
{
    private int m_width;
    private int m_heghit;
    private int m_unitCount;

    public override Vector3 GetDriftOffset(List<SlotAssignment> slotAssignments)
    {
        return new Vector3(1f, 0, 1f);
    }
    public override Vector3 GetSlotLocation(int slotIndex)
    {
        int ROW = slotIndex / m_width;
        int COL = slotIndex % m_width;
        float SIGN = (COL % 2 == 0) ? -1f : 1f;
        float offset = (Mathf.Min(m_width, m_unitCount - ROW * m_width) % 2) == 0 ? -0.5f : 0f;

        float Z = (float)(-ROW);
        float Y = 0f;
        float X = (float)((COL + 1) / 2) * SIGN+offset;

        Vector3 Relative = new Vector3(X, Y, Z) * 1.4f;
        return Relative;
        //int colunm = slotIndex % m_width;
        //int row = slotIndex / m_width;
        //Debug.Log($"{colunm},{row}");
        //Debug.Log($"startPosition: {m_startPosition.ToString()}");
        //return m_startPosition + new Vector3(colunm * 2, 0, -row * 2);
    }
    public override Vector3 GetStartPosition(int _unitCount)
    {
        m_unitCount = _unitCount;
        m_width = (int)Mathf.Round(Mathf.Sqrt(_unitCount));
        m_heghit = (int)Mathf.Round(_unitCount / ((m_width<=0)?1:m_width));
        Debug.Log($"m_widht : {m_width}");
        float startX = -m_width / 2;
        float startZ = -m_heghit / 2;

        return m_startPosition = new Vector3(startX, startZ);
    }
    public override int[,] CalulatePrefer(Vector3[] _unitPos, Vector3[] _desPos,int _unitcount)
    {
        var unit_Dists = new List<Tuple<int, float>>[_unitcount];
        for (int i = 0; i < _unitcount; ++i)
        {
            unit_Dists[i] = new List<Tuple<int, float>>();

            for (int j = 0; j < _unitcount; ++j)
            {
                float dist = Vector3.Distance(_unitPos[i], _desPos[j %m_width]);

                unit_Dists[i].Add(new Tuple<int, float>(j + _unitcount, dist));
            }
        }

        var Format_Dists = new List<Tuple<int, float>>[_desPos.Length];
        for(int i=0;i<_unitcount;++i)
        {
            Format_Dists[i] = new List<Tuple<int, float>>();

            for(int j=0;j<_unitcount;++j)
            {
                float dist = Vector3.Distance(_desPos[i ], _unitPos[j]);

                Format_Dists[i].Add(new Tuple<int, float>(j, dist));
            }
        }

        for (int i = 0; i < _unitcount; ++i)
        {
            //오름차순 정렬
            unit_Dists[i].Sort(delegate (Tuple<int, float> x, Tuple<int, float> y)
            {
                return x.Item2.CompareTo(y.Item2);
            });
        }

        for (int i = 0; i < _unitcount; ++i)
        {
            Format_Dists[i].Sort(delegate (Tuple<int, float> x, Tuple<int, float> y)
            {
                return x.Item2.CompareTo(y.Item2);
            });
        }

        int[,] prefer = new int[_unitcount*2, _unitcount];

        for (int i = 0; i < _unitcount; ++i)
        {
            for (int j = 0; j < _unitcount; ++j)
            {
                //from 0
                prefer[i, j] = unit_Dists[i][j].Item1;
            }
        }
        for (int i = 0; i < _unitcount; ++i)
        {
            for (int j = 0; j < _unitcount; ++j)
            {
                //from N 
                prefer[i + _unitcount, j] = Format_Dists[i][j].Item1;
            }
        }
        return prefer;
    }

}
