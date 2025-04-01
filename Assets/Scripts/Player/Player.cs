using UnityEngine;


public class Player : MonoBehaviour
{

    void Start()
    {
        
    }

    Stack stack = new Stack();
    private GameObject PlayerBall = null;

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

        else if(Input.GetKeyDown(KeyCode.Q))
        {
            Data data = stack.Output();
            transform.position = transform.position - data.ｄirection;

            if (data.TargetBall != null)  // isBallMove가 공이 움직였는지를 확인하는 코드였음 
            { // TargetBall가 어느 공에 닿아있는지 저장하기도 하지만 이 TargetBall에 값이 저장되는건 플레이어와 공이 닿아있다는 전제 하에 저장이 되는 것이기 때문에 
                Transform BallTransform = data.TargetBall.transform; // TargetBall에 값이 저장되어 초기에 설정한 null이 아니면 공이 함께 움직였다는 뜻이 된다고 생각했습니당
                BallTransform.position = BallTransform.position - data.ｄirection; 
            }
            
        }

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
                    stack.Input(new Data(moveDirection, hit.collider.GetComponent<Ball>()));

                    PlayerBall = hit.collider.gameObject;
                }
            }
            else if (hit.collider.tag == "Target")
            {
                // 이동 허용
                gameObject.transform.position = gameObject.transform.position + moveDirection;
                stack.Input(new Data(moveDirection));
            }
        }
        else// 빈 공간일 때 이동
        {
            if(moveDirection==Vector3.zero) 
            {
                return;
            }
            gameObject.transform.position = gameObject.transform.position + moveDirection;
            stack.Input(new Data(moveDirection));// 그냥 이렇게만 적으면 이게 update 함수 안에 있는 코드라서 오버스택 발생함
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