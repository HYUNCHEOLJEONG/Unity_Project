using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float movespeed;
    private float minY = -44.1f; // 적이 y축 기준으로 -44.1에 도달하면
                                 // 적 오브젝트를 삭제
    [SerializeField]
    private float hp; // 적의 체력 - > UNITY 인스펙터에서 조정 가능

    void Start()
    {

    }
    // 적이 너무 아래로 내려가면-> 화면 밖으로 나가게 되면 파괴
    void Update()
    {
        transform.position += Vector3.down * movespeed * Time.deltaTime;
        if(transform.position.y < minY)
        {
            Destroy(gameObject);
        }
      
    }
    // 설정 함수 1. SetSpeed
    //1 => 적의 속도를 결정
    public void SetSpeed(float speed)
    {
        this.movespeed = speed;
    }
    // 설정 함수 2. SetHP
    //2 => 적의 체력을 결정
    public void SetHP(float hp)
    {
        this.hp = hp;
    }

    // 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Weapon")  // 충돌한게 "Weapon"태그를 가진 오브젝트 일 경우
        {
            Weapon weapon = collision.gameObject.GetComponent<Weapon>(); //충돌한 오브젝트에서 Weapon 컴포넌트를 가져옴
            if (weapon != null)
            {
                hp -= weapon.damage;
                if (hp <= 0)
                {
                   
                    Destroy(gameObject); // 적 오브젝트 파괴
                }
                Destroy(collision.gameObject); // 무기 오브젝트 파괴
            }
          
        }
    }
}
