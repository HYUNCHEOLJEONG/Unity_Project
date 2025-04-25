using UnityEngine;
// Unity 엔진 기능들을 가져오는 선언
// MonoBehaviour,Vecotr3,Time,transform 등 유니티 기능을
// 사용하려면 필요 엔진 구조

public class BackgroundScroller : MonoBehaviour
// BackgroundScroller라는 클래스를 정의
{
    [SerializeField] private float moveSpeed = 3f;  
    //배경의 아래로 움직이는 속도
    // SerializeField를 통해 유니티 인스텍터에서 값을 조정할 수 있도록 설정정
    private float resetPositionY = -78f;   // 배경이 내려갈 최하단의 위치 
                                           // y 좌표
    private float repositionY = 156f;      // 배경을 다시 위로 올릴 때 이동시킬
                                           // y 좌표표
    void Update()
    //유니티의 기본 생명주기 함수 중 하나
    //매 프레임마다 호출되는 함수
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        // Vector3.down은 (0,-1,0) 방향으로의 벡터를 나타냄 -> 배경을 아래로 이동
        // Time.deltaTime은 프레임 수에 따라 속도 보정(프레임마다 일정하게 이동하게 함)
        if (transform.position.y <= resetPositionY)
        //trnasform.position.y는 현재 오브젝트의
        //y 좌표표
        {
            transform.position += new Vector3(0f, repositionY, 0f);
        }
    }
}
