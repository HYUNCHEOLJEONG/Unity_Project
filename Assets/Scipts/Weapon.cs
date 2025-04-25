using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float movespeed = 20f; // 총알 속도
    public float damage = 16f;
   
    private void Start()
    {
        Destroy(gameObject, 3f); // 3초 후에 오브젝트 파괴
                                 // 화면 밖으로 나가기 때문문
    }

    private void Update()
    {
        //transform.position(절대적 이동)
        //transform.Translate(상대적 이동)
        transform.Translate(Time.deltaTime * movespeed * Vector3.up);
    }
    public void SetDirection(Quaternion rotation)
    {

        // 아무것도 안 함! 회전은 유니티 에디터에서 설정한 대로만 사용
    }
}