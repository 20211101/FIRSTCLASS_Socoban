using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    bool isGoalPos = false;
    
    public bool Move(Vector3 direction) 
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, 1))
        {
            if (hit.collider.tag == "Wall")
            {
                // 이동 실패
                return false;
            }
            else if (hit.collider.tag == "Ball")
            {
                // 이동 실패
                return false;
            }
            else if (hit.collider.tag == "Target")
            {
                Debug.Log("타깃 감지");
                // 이동 실행
                gameObject.transform.position = gameObject.transform.position + direction;
                if (isGoalPos == false)
                {
                    GameManager.Instance.CurCnt++;
                    isGoalPos = true;
                }
                return true;
            }
        }
        else // 아직까지 남은 경우의 수는 빈 공간 밖에 없음
        {
            // 이동 실행
            if (isGoalPos == true)
            {
                isGoalPos = false;
                GameManager.Instance.CurCnt--;
            }

            gameObject.transform.position = gameObject.transform.position + direction;
            return true;
        }
        Debug.LogError("이게 걸리면 놓친 게 있는거임");
        return true;
    }
}
