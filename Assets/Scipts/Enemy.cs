using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float movespeed;
    private float minY = -44.1f; // 적이 사라지는 위치
    [SerializeField]
    private float hp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Enemy Start with Speed: " + movespeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * movespeed * Time.deltaTime;
        if(transform.position.y < minY)
        {
            Destroy(gameObject);
        }
      

    }
    public void SetSpeed(float speed)
    {
        this.movespeed = speed;
    }

    public void SetHP(float hp)
    {
        this.hp = hp;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌 감지됨: " + collision.name); // 충돌 오브젝트 이름 출력

        if (collision.gameObject.tag == "Weapon")
        {
            Debug.Log("무기 충돌!"); // 무기인 경우 확인

            Weapon weapon = collision.gameObject.GetComponent<Weapon>();
            if (weapon != null)
            {
                Debug.Log("데미지: " + weapon.damage); // 데미지 값 확인
                hp -= weapon.damage;
                if (hp <= 0)
                {
                    Debug.Log("적 파괴!");
                    Destroy(gameObject);
                }
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.LogWarning("Weapon 스크립트 없음!");
            }
        }
    }


}
