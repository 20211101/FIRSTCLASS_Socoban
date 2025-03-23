using UnityEngine;


public class Player : MonoBehaviour
{
<<<<<<< Updated upstream
    void Start()
    {
        
    }
=======
    Stack stack = new Stack();
    private GameObject PlayerBall = null;
>>>>>>> Stashed changes

    void Update()
    {
        RaycastHit hit;
        Vector3 UP = transform.forward;
        Vector3 DOWN = -transform.forward;
        Vector3 RIGHT = transform.right;
        Vector3 LEFT = -transform.right;

        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W))
            moveDirection = UP;
        else if (Input.GetKeyDown(KeyCode.A))
            moveDirection = LEFT;
        else if (Input.GetKeyDown(KeyCode.S))
            moveDirection = DOWN;
        else if (Input.GetKeyDown(KeyCode.D))
            moveDirection = RIGHT;
<<<<<<< Updated upstream
=======
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            Data data = stack.Output();
            transform.position = transform.position - data.ｄirection;

            if (PlayerBall != null)
            {
                PlayerBall.transform.position = PlayerBall.transform.position - data.ｄirection;
            }
        }
>>>>>>> Stashed changes

        if (Physics.Raycast(transform.position, moveDirection, out hit, 1))
        {
            if (hit.collider.tag == "Wall")
            {
                // NOTHING
            }
            else if (hit.collider.tag == "Ball")
            {
                // 공에게 이동 신호 전달
                // bool 값 리턴 받아서 이동 성공 여부 확인 후 성공이면 공 있던 위치로 이동
   
                bool isBallMove = hit.collider.GetComponent<Ball>().Move(moveDirection);
                if (isBallMove)
                {
                    gameObject.transform.position = gameObject.transform.position + moveDirection;
<<<<<<< Updated upstream
=======
                    stack.Input(new Data(moveDirection));

                    PlayerBall = hit.collider.gameObject;
>>>>>>> Stashed changes
                }
            }
            else if (hit.collider.tag == "Target")
            {
                // 이동 허용
                gameObject.transform.position = gameObject.transform.position + moveDirection;
            }
        }
        else// 빈 공간일 때 이동
        {
            gameObject.transform.position = gameObject.transform.position + moveDirection;
        }
    }
}

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    if (Physics.Raycast(transform.position, UP, out hit, 1))
        //    {
        //        if (hit.collider.tag == "Wall")
        //        {
        //            // NOTHING
        //        }
        //        else if(hit.collider.tag == "Ball")
        //        {
        //            // 공에게 이동 신호 전달
        //            // bool 값 리턴 받아서 이동 성공 여부 확인 후 성공이면 공 있던 위치로 이동
        //            bool isBallMove = hit.collider.GetComponent<Ball>().Move(UP);
        //            if(isBallMove) 
        //            {
        //                gameObject.transform.position = gameObject.transform.position + UP;
        //            }
        //        }
        //        else if(hit.collider.tag == "Target")
        //        {
        //            // 이동 허용
        //            gameObject.transform.position = gameObject.transform.position + UP;
        //        }
        //    }
        //    else// 빈 공간일 때 이동
        //    {
        //        gameObject.transform.position = gameObject.transform.position + UP;
        //    }
        //}
        //else if(Input.GetKeyDown(KeyCode.A))
        //{
        //    if (Physics.Raycast(transform.position, LEFT, out hit, 1))
        //    {
        //        if (hit.collider.tag == "Wall")
        //        {
        //            // NOTHING
        //        }
        //        else if (hit.collider.tag == "Ball")
        //        {
        //            // 공에게 이동 신호 전달
        //            // bool 값 리턴 받아서 이동 성공 여부 확인 후 성공이면 공 있던 위치로 이동
        //            bool isBallMove = hit.collider.GetComponent<Ball>().Move(LEFT);
        //            if (isBallMove)
        //            {
        //                gameObject.transform.position = gameObject.transform.position + LEFT;
        //            }
        //        }
        //        else if (hit.collider.tag == "Target")
        //        {
        //            // 이동 허용
        //            gameObject.transform.position = gameObject.transform.position + LEFT;
        //        }
        //    }
        //    else
        //    {
        //        gameObject.transform.position = gameObject.transform.position + LEFT;
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    if (Physics.Raycast(transform.position, DOWN, out hit, 1))
        //    {
        //        if (hit.collider.tag == "Wall")
        //        {
        //            // NOTHING
        //        }
        //        else if (hit.collider.tag == "Ball")
        //        {
        //            // 공에게 이동 신호 전달
        //            // bool 값 리턴 받아서 이동 성공 여부 확인 후 성공이면 공 있던 위치로 이동
        //            bool isBallMove = hit.collider.GetComponent<Ball>().Move(DOWN);
        //            if (isBallMove)
        //            {
        //                gameObject.transform.position = gameObject.transform.position + DOWN;
        //            }
        //        }
        //        else if (hit.collider.tag == "Target")
        //        {
        //            // 이동 허용
        //            gameObject.transform.position = gameObject.transform.position + DOWN;
        //        }
        //    }
        //    else
        //    {
        //        gameObject.transform.position = gameObject.transform.position + DOWN;
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    if (Physics.Raycast(transform.position, RIGHT, out hit, 1))
        //    {
        //        if (hit.collider.tag == "Wall")
        //        {
        //            // NOTHING
        //        }
        //        else if (hit.collider.tag == "Ball")
        //        {
        //            // 공에게 이동 신호 전달
        //            // bool 값 리턴 받아서 이동 성공 여부 확인 후 성공이면 공 있던 위치로 이동
        //            bool isBallMove = hit.collider.GetComponent<Ball>().Move(RIGHT);
        //            if (isBallMove)
        //            {
        //                gameObject.transform.position = gameObject.transform.position + RIGHT;
        //            }
        //        }
        //        else if (hit.collider.tag == "Target")
        //        {
        //            // 이동 허용
        //            gameObject.transform.position = gameObject.transform.position + RIGHT;
        //        }
        //    }
        //    else
        //    {
        //        gameObject.transform.position = gameObject.transform.position + RIGHT;
        //    }
        //}