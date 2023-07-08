using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�� ������ ��Ī�����ִ� �˰���
public static class StableMarriage_Algoritm 
{
    static bool wPrefersOriginOverNew(int[,] prefer,int wIndex, int mIndex,int mOriginal)
    {
        int count = prefer.GetLength(1);

        for (int i = 0; i < count; i++)
        {
            //������ ���� ��Ʈ�ʿ� ���� ��ȣ���� �� ũ�ٸ� true ��ȯ
            if (prefer[wIndex, i] == mOriginal)
                return true;

            //���ο� ���� ��Ʈ�ʿ� ���� ��ȣ���� �� ũ�ٸ� flase ��ȯ
            if (prefer[wIndex, i] == mIndex)
                return false;
        }
        return false;
    }
    
    public static int[] Calculate(int[,] prefer)
    {
        int count = prefer.GetLength(1);

        //���� ��Ʈ�� �迭�� ���� ��Ʈ�� �迭 �ʱ�ȭ
        int[] wPartner = new int[count];
        bool[] mFree = new bool[count];

        for (int i = 0; i < count; i++)
            wPartner[i] = -1; 

        //��Ī�� ������ ��
        int freeCount = count;

        while(freeCount>0)
        {
            //��Ī���� ���� ���ڸ� �����ϴ� ����
            int mIndex=0;
            for (; mIndex < count; mIndex++)
                if (mFree[mIndex] == false)
                    break;

            //������Ʈ�ʰ� �������ִ� ����� ����ŭ �ݺ�
            //
            for(int i=0;i<count&&mFree[mIndex]==false;i++)
            {
                int wIndex = prefer[mIndex, i];

                //������Ʈ�ʿ��� �ƹ���Ʈ�ʵ� ���ٸ�
                //��Ī ��Ŵ
                if(wPartner[wIndex-count]==-1)
                {
                    wPartner[wIndex - count] = mIndex;
                    mFree[mIndex] = true;
                    freeCount--;
                }
                //���� ��Ʈ�ʿ��� ��Ʈ�ʰ� �ִٸ�
                else
                {
                    int mOriginal = wPartner[wIndex - count];

                    //������Ʈ���� ��ȣ���� ������ ���ں���
                    //���ο� ���ڰ� ���� ���
                    //���� ��Ʈ�ʿ� ���ο� ���ڸ� ��Ī��Ű��
                    //���� ������ ��Ī���θ� �ȵ����� �����Ѵ�.
                    if(wPrefersOriginOverNew
                        (prefer,wIndex,mIndex,mOriginal)==false)
                    {
                        wPartner[wIndex - count] = mIndex;
                        mFree[mIndex] = true;
                        mFree[mOriginal] = false;
                    }
                }
            }
        }
        return wPartner;
    }
}
