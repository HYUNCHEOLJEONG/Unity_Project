using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private GameObject weapon; // 총알 프리팹
    [SerializeField]
    private Transform shootTransform; // 드래곤 머리 위치
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

        // 마나가 100 이상일 때만 트리플샷 발동
        if (!isTripleShot && ui.GetCurrentMana() >= 100f)
        {
            isTripleShot = true;
            tripleShotEndTime = Time.time + 10f;  // 10초간 트리플 샷 유지
            ui.UseMana(100f); // 마나 소모
        }

        // 트리플 샷이 활성화되었을 때만 트리플 샷을 발사
        if (isTripleShot)
        {
            if (Time.time <= tripleShotEndTime)
                TripleShoot();  // 트리플 샷 자동 발사
            else
                isTripleShot = false;  // 10초 지나면 트리플 샷 해제
        }

        // 트리플 샷이 아닐 때 마우스 클릭으로만 총알 발사
        if (!isTripleShot && Input.GetMouseButton(0))
        {
            Shoot();  // 기본 총알 발사
        }
    }

    // 플레이어 이동 처리
    void HandleMovement()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        float clampedX = Mathf.Clamp(worldMousePos.x, -15f, 15f);
        float clampedY = Mathf.Clamp(worldMousePos.y, -33f, 30f);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // 트리플 샷 발사
    void TripleShoot()
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            Vector3 basePos = shootTransform.position;

            // 드래곤 머리 방향을 기준으로 발사 각도 설정
            CreateBullet(basePos, shootTransform.rotation.eulerAngles.z + 45f);
            CreateBullet(basePos, shootTransform.rotation.eulerAngles.z);
            CreateBullet(basePos, shootTransform.rotation.eulerAngles.z - 45f);

            lastShotTime = Time.time;
        }
    }

    // 기본 총알 발사 (드래곤 머리 방향으로 발사)
    void Shoot()
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            Vector3 basePos = shootTransform.position;

            // 드래곤 머리 방향을 그대로 사용하여 발사
            Quaternion rotation = shootTransform.rotation;  // 드래곤 머리 회전

            // 회전값을 -90도 회전시켜서 총알이 위쪽으로 발사되도록 수정
            rotation *= Quaternion.Euler(0, 0, 0f);

            GameObject bullet = Instantiate(weapon, basePos, rotation);

            // Weapon의 SetDirection 메서드를 사용하여 총알의 회전 방향을 드래곤 머리 회전으로 설정
            bullet.GetComponent<Weapon>().SetDirection(rotation);

            if (Time.time - lastShotTime > shootInterval)
            {
                // 총알 발사
                lastShotTime = Time.time;
            }
            lastShotTime = Time.time;
        }
    }

    void CreateBullet(Vector3 position, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle); // 각도에 맞게 회전
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
            ui.TakeDamage(20f); // 적과 충돌 시 데미지
        }
        else if (collision.CompareTag("Potion")) // 포션과 충돌 시
        {
            ui.RegainMana(ui.GetMaxMana() * 100f); // 마나 20% 회복
            Destroy(collision.gameObject);         // 포션 제거
        }
    }
}



//Debug.log(Input.mousePosition);
//void Update()
//{
//    // 마우스 위치를 월드 좌표로 변환
//    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

//    // X와 Y 위치를 제한 (Clamp)
//    float clampedX = Mathf.Clamp(mousePos.x, -2.72f, 2.72f);
//    float clampedY = Mathf.Clamp(mousePos.y, -4.5f, 4.5f); // Y 범위는 필요에 따라 조절하세요

//    // Z는 현재 오브젝트의 Z값 유지
//    transform.position = new Vector3(clampedX, clampedY, transform.position.z);
//}