using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float movespeed;
    private float minY = -44.1f; // ���� ������� ��ġ
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
        Debug.Log("�浹 ������: " + collision.name); // �浹 ������Ʈ �̸� ���

        if (collision.gameObject.tag == "Weapon")
        {
            Debug.Log("���� �浹!"); // ������ ��� Ȯ��

            Weapon weapon = collision.gameObject.GetComponent<Weapon>();
            if (weapon != null)
            {
                Debug.Log("������: " + weapon.damage); // ������ �� Ȯ��
                hp -= weapon.damage;
                if (hp <= 0)
                {
                    Debug.Log("�� �ı�!");
                    Destroy(gameObject);
                }
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.LogWarning("Weapon ��ũ��Ʈ ����!");
            }
        }
    }


}
