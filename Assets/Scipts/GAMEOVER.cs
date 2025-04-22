using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField]
    private TextMeshProUGUI gameOverText;  // 게임 오버 텍스트 UI
    [SerializeField]
    private Player player;  // 플레이어 스크립트 참조

    private bool isGameOver = false;

    void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);  // 시작 시에는 게임 오버 텍스트 숨김
        }
    }

    void Update()
    {
        if (player != null && player.ui != null)
        {
            // 플레이어의 체력이 0 이하일 때 게임 오버 처리
            if (player.ui.GetCurrentHealth() <= 0 && !isGameOver)
            {
                GameOverSequence();
            }
        }
    }

    // 게임 오버 시 호출되는 함수
    void GameOverSequence()
    {
        isGameOver = true;

        // 게임 오버 텍스트 활성화
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);  // 게임 오버 텍스트 표시
        }

        // 게임 일시 정지
        Time.timeScale = 0f;  // 게임 일시 정지
    }
}
