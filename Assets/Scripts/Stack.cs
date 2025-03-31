using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;


public struct Data// 공 움직임도 저장하도록 공간을 만들어야 한다.

{
    public Vector3 moveDir;
    public Ball targetBall;

    public Data(Vector3 _moveDir, Ball _targerBall = null)
    {
        moveDir = _moveDir;
        targetBall = _targerBall;
    }


    public bool IsballMoving => targetBall != null;
    public class Stack
    {

        Data[] dataArr = new Data[100];
        int cursor = -1;

        public bool Input(Data data)
        {
            if (cursor >= 100)
                return false;
            cursor++;
            dataArr[cursor] = data;
            return true;
        }


        public Data Output()
        {
            if (cursor < 0)
                return new Data(Vector3.zero);
            Data data = dataArr[cursor];
            cursor--;
            return data;
        }
    }
}
