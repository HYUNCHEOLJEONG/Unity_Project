using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private GameObject weapon; // �Ѿ� ������
    [SerializeField]
    private Transform shootTransform; // �巡�� �Ӹ� ��ġ
    [SerializeField]
    private float shootInterval = 0.1f;

    private float lastShotTime = 0f;
    public PlayerUI ui;

    private  bool isTripleShot = false;
    private float tripleShotEndTime = 0f;

    void Start()
    {
        if (Camera.main == null)
            Debug.LogError("Main Camera is not assigned or missing the 'MainCamera' tag.");

        if (weapon == null)
            Debug.LogError("Weapon is not assigned!");

        if (shootTransform == null)
            Debug.LogError("ShootTransform is not assigned!");
    }

    void Update()
    {
        HandleMovement();

        // ������ 100 �̻��� ���� Ʈ���ü� �ߵ�
        if (!isTripleShot && ui.GetCurrentMana() >= 100f)
        {
            isTripleShot = true;
            tripleShotEndTime = Time.time + 10f;  // 10�ʰ� Ʈ���� �� ����
            ui.UseMana(100f); // ���� �Ҹ�
        }

        // Ʈ���� ���� Ȱ��ȭ�Ǿ��� ���� Ʈ���� ���� �߻�
        if (isTripleShot)
        {
            if (Time.time <= tripleShotEndTime)
                TripleShoot();  // Ʈ���� �� �ڵ� �߻�
            else
                isTripleShot = false;  // 10�� ������ Ʈ���� �� ����
        }

        // Ʈ���� ���� �ƴ� �� ���콺 Ŭ�����θ� �Ѿ� �߻�
        if (!isTripleShot && Input.GetMouseButton(0))
        {
            Shoot();  // �⺻ �Ѿ� �߻�
        }
    }

    // �÷��̾� �̵� ó��
    void HandleMovement()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        float clampedX = Mathf.Clamp(worldMousePos.x, -15f, 15f);
        float clampedY = Mathf.Clamp(worldMousePos.y, -33f, 30f);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // Ʈ���� �� �߻�
    void TripleShoot()
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            Vector3 basePos = shootTransform.position;

            // �巡�� �Ӹ� ������ �������� �߻� ���� ����
            CreateBullet(basePos, shootTransform.rotation.eulerAngles.z + 45f);
            CreateBullet(basePos, shootTransform.rotation.eulerAngles.z);
            CreateBullet(basePos, shootTransform.rotation.eulerAngles.z - 45f);

            lastShotTime = Time.time;
        }
    }

    // �⺻ �Ѿ� �߻� (�巡�� �Ӹ� �������� �߻�)
    void Shoot()
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            Vector3 basePos = shootTransform.position;

            // �巡�� �Ӹ� ������ �״�� ����Ͽ� �߻�
            Quaternion rotation = shootTransform.rotation;  // �巡�� �Ӹ� ȸ��

            // ȸ������ -90�� ȸ�����Ѽ� �Ѿ��� �������� �߻�ǵ��� ����
            rotation *= Quaternion.Euler(0, 0, 0f);

            GameObject bullet = Instantiate(weapon, basePos, rotation);

            // Weapon�� SetDirection �޼��带 ����Ͽ� �Ѿ��� ȸ�� ������ �巡�� �Ӹ� ȸ������ ����
            bullet.GetComponent<Weapon>().SetDirection(rotation);

            if (Time.time - lastShotTime > shootInterval)
            {
                // �Ѿ� �߻�
                lastShotTime = Time.time;
            }
            lastShotTime = Time.time;
        }
    }

    void CreateBullet(Vector3 position, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle); // ������ �°� ȸ��
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
            ui.TakeDamage(20f); // ���� �浹 �� ������
        }
        else if (collision.CompareTag("Potion")) // ���ǰ� �浹 ��
        {
            ui.RegainMana(ui.GetMaxMana() * 100f); // ���� 20% ȸ��
            Destroy(collision.gameObject);         // ���� ����
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