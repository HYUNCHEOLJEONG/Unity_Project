using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI winText; // ✅ 추가
    [SerializeField] private GameManager gameManager;

    private float[] arrayPosX = {
        -23f, -19f, -16f, -13f, -10f, -9f, -6f, -3f, 0f, 3f, 6f,
        9f, 12f, 15f, 18f, 22f
    };

    private float spawnInterval = 2f;
    private int spawnRound = 1;
    private int enemyIndex = 0;

    void Start()
    {
        StartCoroutine(WaitForGameStart());
    }

    IEnumerator WaitForGameStart()
    {
        yield return new WaitUntil(() => gameManager != null && gameManager.IsGameStarted());
        StartCoroutine(EnemyRoutine());
    }

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (spawnRound <= 100)
        {
            if (spawnRound % 10 == 0)
            {
                yield return StartCoroutine(ShowRoundText());
            }

            int spawnCountThisRound = 4 + spawnRound / 10;

            List<int> availablePositions = new List<int>(arrayPosX.Length);
            for (int i = 0; i < arrayPosX.Length; i++) availablePositions.Add(i);

            for (int i = 0; i < spawnCountThisRound; i++)
            {
                int randIndex = Random.Range(0, availablePositions.Count);
                float posX = arrayPosX[availablePositions[randIndex]];
                availablePositions.RemoveAt(randIndex);
                SpawnEnemy(posX, enemyIndex);
            }

            // ✅ WIN 처리
            if (spawnRound == 80 && !gameManager.IsGameEnded())
            {
                ShowWinAndEndGame();
                yield break;
            }

            spawnRound++;
            AdjustEnemyIndex();
            AdjustSpawnInterval();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator ShowRoundText()
    {
        if (roundText != null)
        {
            int displayRound = (spawnRound + 10) / 10;
            roundText.text = "ROUND " + displayRound;
            roundText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            roundText.gameObject.SetActive(false);
        }
    }

    void SpawnEnemy(float posX, int index)
    {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if (Random.Range(0, 10) == 0) index++;

        if (index >= enemies.Length) index = enemies.Length - 1;

        Quaternion enemyRotation = Quaternion.Euler(0, 0, 270);
        GameObject enemyObj = Instantiate(enemies[index], spawnPos, enemyRotation);

        Enemy enemy = enemyObj.GetComponent<Enemy>();
    }

    private void AdjustSpawnInterval()
    {
        spawnInterval = Mathf.Max(0.7f, 2f - spawnRound * 0.03f);
    }

    private void AdjustEnemyIndex()
    {
        enemyIndex = Mathf.Min((spawnRound / 10), enemies.Length - 1);
    }

    // ✅ Win 처리 함수
    private void ShowWinAndEndGame()
    {
        if (winText != null)
        {
            winText.text = "YOU WIN!";
            winText.gameObject.SetActive(true);
        }

        if (gameManager != null)
        {
            gameManager.EndGame();
        }

        Time.timeScale = 0f;
    }
}
