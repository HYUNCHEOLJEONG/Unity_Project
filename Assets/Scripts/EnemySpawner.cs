using System.Collections;     //IEnumerator, WaitForSeconds, WaitUntil,yeild return 등 사용
using System.Collections.Generic; // List<T> 사용
using TMPro;   // TextMeshProUGUI 사용
using UnityEngine; // UnityEngine 기능들을 가져오는 선언

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    // 적 오브젝트를 담는 배열
    // 유니티 인스펙터에서 적 오브젝트를 드래그 앤 드롭으로 넣을 수 있도록 설정
    [SerializeField] private TextMeshProUGUI roundText;
    // 라운드 텍스틀 표시하기 위한 변수
    [SerializeField] private TextMeshProUGUI winText; 
    // 승리 텍스트 표시하기 위한 변수
    [SerializeField] private GameManager gameManager;
    // 외부에서 게임이 시작되었는지, 끝났는지를 알려주는 게임 메니저 클라스와 연결결
    
    private float[] arrayPosX = {
        -23f, -19f, -16f, -13f, -10f, -9f, -6f, -3f, 0f, 3f, 6f,
        9f, 12f, 15f, 18f, 22f
    };
    // 적 스폰 위치 설정

    private float spawnInterval = 2f;
    // 적 생성 간격
    private int spawnRound = 1;
    // 현재 라운드 내부적 번호
    private int enemyIndex = 0;
    // 현재 생성할 적 인덱스

    void Start()
    {
        StartCoroutine(WaitForGameStart());
    }
    // WaitForGameStart() 코루틴을 시작하여 게임이 시작될 때까지 대기
    IEnumerator WaitForGameStart()
    {
        yield return new WaitUntil(() => gameManager != null && gameManager.IsGameStarted());
        StartCoroutine(EnemyRoutine());
    }
    // EnemyRoutine() 코루틴을 시작하여 적 스폰을 시작
    // GameManger가 존재하고 게임이 시작됬을 때 까지 기다리기
    // 적 스폰 시작

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(1f);
        // 1초 대기 후 시작작

        while (spawnRound <= 100)
        {
            if (spawnRound % 10 == 0)
            {
                yield return StartCoroutine(ShowRoundText());
            }

            int spawnCountThisRound = 4 + spawnRound / 10;
            // 기본은 4마리, spwanRound가 10의 배수 일때마다 1마리씩 증가가

            List<int> availablePositions = new List<int>(arrayPosX.Length);
            // 적 스폰 위치 배열을 리스트로 변환환
            for (int i = 0; i < arrayPosX.Length; i++) availablePositions.Add(i);

            for (int i = 0; i < spawnCountThisRound; i++)
            {
                int randIndex = Random.Range(0, availablePositions.Count);
                float posX = arrayPosX[availablePositions[randIndex]];
                availablePositions.RemoveAt(randIndex);
                SpawnEnemy(posX, enemyIndex);
            }

            // WIN 처리
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
        // 인덱스가 enemies 배열의 길이를 초과하지 않도록 설정

        Quaternion enemyRotation = Quaternion.Euler(0, 0, 270);
        // z축으로 270도 회전 => 적이 아래로 향하기 위해서서
        GameObject enemyObj = Instantiate(enemies[index], spawnPos, enemyRotation);
         // Instantiate를 통해 적 오브젝트 생성
        Enemy enemy = enemyObj.GetComponent<Enemy>();
    }

    private void AdjustSpawnInterval()
    {
        spawnInterval = Mathf.Max(0.7f, 2f - spawnRound * 0.03f);
    }
    //0.7f보다 작아지지 않도록 설정
    // spawnRound가 증가할수록 spawnInterval이 감소하도록 설정

    private void AdjustEnemyIndex()
    {
        enemyIndex = Mathf.Min((spawnRound / 10), enemies.Length - 1);
    }
    // 적의 인덱스를 조정하는 함수
    // spawnRound가 10의 배수일 때마다 enemyIndex 증가

    //  Win 처리 함수
    private void ShowWinAndEndGame()
    {
        if (winText != null)
        {   
            winText.text = "YOU WIN!"; // 게임 승리 메시지를 표시
            winText.gameObject.SetActive(true); // 승리 메시지 UI를 화면에 표시
        }

        if (gameManager != null)
        {
            gameManager.EndGame(); // 게임 메니저에 게임 종료 요청
        }

        Time.timeScale = 0f; // 게임 시간을 멈춤
    }
}
