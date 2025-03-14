using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // ���� Target ������Ʈ�� ������ �ִ��� ���� ����.
    //(Target�� ��ģ ���¿��� ���� �����̸� Ŭ���� ���θ� �ٽ� �����ؾ� �ϱ� ����)
    bool isGoalPos = false;
    
    public bool Move(Vector3 direction) // �̵� �����ϸ� �̵� �� true, �̵� �� �ϸ� false ��ȯ
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, 1))
        {
            if (hit.collider.tag == "Wall")
            {
                // �̵� ����
                return false;
            }
            else if (hit.collider.tag == "Ball")
            {
                // �̵� ����
                return false;
            }
            else if (hit.collider.tag == "Target")
            {
                Debug.Log("Ÿ�� ����");
                // �̵� ����
                gameObject.transform.position = gameObject.transform.position + direction;
                if (isGoalPos == false)
                {
                    GameManager.Instance.CurCnt++;
                    isGoalPos = true;
                }
                return true;
            }
            else if(hit.collider.tag == "Player")
            {
                // �̵� ����
                if (isGoalPos == true)
                {
                    isGoalPos = false;
                    GameManager.Instance.CurCnt--;
                }
                gameObject.transform.position = gameObject.transform.position + direction;
                return true;
            }
        }
        else // �������� ���� ����� ���� �� ���� �ۿ� ����
        {
            // �̵� ����
            if (isGoalPos == true)
            {
                isGoalPos = false;
                GameManager.Instance.CurCnt--;
            }
            gameObject.transform.position = gameObject.transform.position + direction;
            return true;
        }
        Debug.LogError("�̰� �ɸ��� ��ģ �� �ִ°���");
        return true;
    }
}
