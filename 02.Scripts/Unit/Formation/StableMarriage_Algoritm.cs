using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//두 집단을 매칭시켜주는 알고리즘
public static class StableMarriage_Algoritm 
{
    static bool wPrefersOriginOverNew(int[,] prefer,int wIndex, int mIndex,int mOriginal)
    {
        int count = prefer.GetLength(1);

        for (int i = 0; i < count; i++)
        {
            //기존의 남자 파트너에 대한 선호도가 더 크다면 true 반환
            if (prefer[wIndex, i] == mOriginal)
                return true;

            //새로운 남자 파트너에 대한 선호도가 더 크다면 flase 반환
            if (prefer[wIndex, i] == mIndex)
                return false;
        }
        return false;
    }
    
    public static int[] Calculate(int[,] prefer)
    {
        int count = prefer.GetLength(1);

        //여자 파트너 배열과 남자 파트너 배열 초기화
        int[] wPartner = new int[count];
        bool[] mFree = new bool[count];

        for (int i = 0; i < count; i++)
            wPartner[i] = -1; 

        //매칭된 남자의 수
        int freeCount = count;

        while(freeCount>0)
        {
            //매칭되지 않은 남자를 지정하는 로직
            int mIndex=0;
            for (; mIndex < count; mIndex++)
                if (mFree[mIndex] == false)
                    break;

            //남자파트너가 가지고있는 경우의 수만큼 반복
            //
            for(int i=0;i<count&&mFree[mIndex]==false;i++)
            {
                int wIndex = prefer[mIndex, i];

                //여자파트너에게 아무파트너도 없다면
                //매칭 시킴
                if(wPartner[wIndex-count]==-1)
                {
                    wPartner[wIndex - count] = mIndex;
                    mFree[mIndex] = true;
                    freeCount--;
                }
                //여자 파트너에게 파트너가 있다면
                else
                {
                    int mOriginal = wPartner[wIndex - count];

                    //여자파트너의 선호도가 기존의 남자보다
                    //새로운 남자가 높을 경우
                    //여자 파트너와 새로운 남자를 매칭시키고
                    //기존 남자의 매칭여부를 안됨으로 설정한다.
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
