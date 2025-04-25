using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private GameObject weapon; // 
    [SerializeField]
    private Transform shootTransform; // 
    [SerializeField]
    private float shootInterval = 0.1f;

    private float lastShotTime = 0f;
    public PlayerUI ui;

    private  bool isTripleShot = false;
    private float tripleShotEndTime = 0f;

    void Start()
    {
      
    }

    void Update()
    {
        HandleMovement();

        if (!isTripleShot && ui.GetCurrentMana() >= 100f)
        {
            isTripleShot = true;
            tripleShotEndTime = Time.time + 10f;  // 
            ui.UseMana(100f); // 
        }

        // 
        if (isTripleShot)
        {
            if (Time.time <= tripleShotEndTime)
                TripleShoot();  // 
            else
                isTripleShot = false;  
        }

      
        if (!isTripleShot && Input.GetMouseButton(0))
        {
            Shoot();  
        }
    }

    //
    void HandleMovement()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        float clampedX = Mathf.Clamp(worldMousePos.x, -15f, 15f);
        float clampedY = Mathf.Clamp(worldMousePos.y, -33f, 30f);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    //
    void TripleShoot()
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            Vector3 basePos = shootTransform.position;

            // 
            CreateBullet(basePos, shootTransform.rotation.eulerAngles.z + 45f);
            CreateBullet(basePos, shootTransform.rotation.eulerAngles.z);
            CreateBullet(basePos, shootTransform.rotation.eulerAngles.z - 45f);

            lastShotTime = Time.time;
        }
    }

    //
    void Shoot()
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            Vector3 basePos = shootTransform.position;

          
            Quaternion rotation = shootTransform.rotation;  //

            // 
            rotation *= Quaternion.Euler(0, 0, 0f);

            GameObject bullet = Instantiate(weapon, basePos, rotation);

            // 
            bullet.GetComponent<Weapon>().SetDirection(rotation);

                //
            if (Time.time - lastShotTime > shootInterval)
            {
               
                lastShotTime = Time.time;
            }
            lastShotTime = Time.time;
        }
    }

    void CreateBullet(Vector3 position, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle); // 
        GameObject bullet = Instantiate(weapon, position, rotation);
        if (bullet != null)
        {
            bullet.GetComponent<Weapon>().SetDirection(rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            ui.TakeDamage(20f); // 
        }
        else if (collision.CompareTag("Potion")) // 
        {
            ui.RegainMana(ui.GetMaxMana() * 100f); // 
            Destroy(collision.gameObject);         // 
        }
    }
}



//Debug.log(Input.mousePosition);
//void Update()
//{
//    // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
//    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

//    // X�� Y ��ġ�� ���� (Clamp)
//    float clampedX = Mathf.Clamp(mousePos.x, -2.72f, 2.72f);
//    float clampedY = Mathf.Clamp(mousePos.y, -4.5f, 4.5f); // Y ������ �ʿ信 ���� �����ϼ���

//    // Z�� ���� ������Ʈ�� Z�� ����
//    transform.position = new Vector3(clampedX, clampedY, transform.position.z);
//}