using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public struct Data
{
    public Vector3 ｄirection;

    public Data(Vector3 _ｄirection)
    {
        ｄirection = _ｄirection;
    }
}


public class Stack : MonoBehaviour
{
    private Data[] data = new Data[100];// 포인터
    private int cursor = -1;
    public bool Input(Data _data)
    {
        if (cursor >= 100) return false;

        cursor++;
        data[cursor] = _data;

        return true;
    }

    public Data Output()
    {// 플레이어의 위치
        // 이동 방향
        if (cursor <= -1)
            return new Data(new Vector3(0,0,0));

        Data temp = data[cursor];
        cursor--;
        return temp;
    }
}
