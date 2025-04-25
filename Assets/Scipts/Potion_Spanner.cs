using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject potionPrefab;  // 포션 프리팹
    [SerializeField] private float spawnInterval = 3f; // 포션 생성 간격
    [SerializeField] private float spawnY = 35f;   // 포션 생성 위치 Y 좌표

    private float[] arrayPosX = {
        -23f, -19f, -16f, -13f,
        -10f, -9f, -6f, -3f, 0f, 3f, 6f,
        9f, 12f, 15f, 18f, 22f
    };
    // 포션 생성 위치 X 좌표 배열

    private float timer = 0f;
    // 시간 경과 추적 타이머
    // Time.deltaTime을 이용해 매 프레임마다 타이마가증가
    private bool firstSpawnSkipped = false;
    // 첫 번째 포션을 생성할 때 지연 시간을 추가하는 용도

    void Update()
    {
        timer += Time.deltaTime;
        // 타이머를 증가시킴
      
    if (timer >= spawnInterval)
    {
        if (firstSpawnSkipped)
        {
            // 첫 번째 포션 생성이 완료되면
            // 일정 간격으로 포션 생성성
            SpawnPotion();
        }
        else
        {
            
            firstSpawnSkipped = true; 
        }

        timer = 0f; // 타이머 초기화
    }
        
    }

    void SpawnPotion()
    {
        int randomIndex = Random.Range(0, arrayPosX.Length);
        float randomX = arrayPosX[randomIndex];

        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);
        Instantiate(potionPrefab, spawnPosition, Quaternion.identity);
    }
}
