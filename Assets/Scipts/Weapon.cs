using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float movespeed = 20f; // 총알 속도
    public float damage = 16f;
   
    private void Start()
    {
        Destroy(gameObject, 3f); // 3초 후 총알 삭제
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * movespeed * Vector3.up);
    }

    // 회전은 유니티에서 설정한 값 그대로 유지하기 위해 SetDirection은 빈 함수로 둠
    public void SetDirection(Quaternion rotation)
    {
       
        // 아무것도 안 함! 회전은 유니티 에디터에서 설정한 대로만 사용
    }
}