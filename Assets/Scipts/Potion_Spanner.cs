using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private float spawnInterval = 3f;  // 20�ʸ��� ����
    [SerializeField] private float spawnY = 35f;         // ���� ���� Y ��ġ

    private float[] arrayPosX = {
        -23f, -19f, -16f, -13f,
        -10f, -9f, -6f, -3f, 0f, 3f, 6f,
        9f, 12f, 15f, 18f, 22f
    };

    private float timer = 0f;
    private bool firstSpawnSkipped = false;

    void Update()
    {
        timer += Time.deltaTime;

        // ù ��° ���� ������ 20�� �Ŀ� �����ϵ��� ����
        if (!firstSpawnSkipped)
        {
            if (timer >= spawnInterval)
            {
                firstSpawnSkipped = true;
                timer = 0f;
            }
            return;
        }

        if (timer >= spawnInterval)
        {
            SpawnPotion();
            timer = 0f;
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
