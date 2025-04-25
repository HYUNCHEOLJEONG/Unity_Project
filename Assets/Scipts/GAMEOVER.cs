using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField]
    private TextMeshProUGUI gameOverText;  // Game Over text UI
    [SerializeField]
    private Player player;  // // 플레이어 스크립트 참조

    private bool isGameOver = false;

    void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);  
        }      
    }

    void Update()
    {
        if (player != null && player.ui != null)
        {
          
            if (player.ui.GetCurrentHealth() <= 0 && !isGameOver)
            {
                GameOverSequence();
            }
        }
    }

    void GameOverSequence() // 게임 오버 상태가 되면 호출되는 함수수
    {
        isGameOver = true;

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);  // Game Over 텍스트 활성화
        }

        Time.timeScale = 0f;  // 시간의 흐름을 멈춤
    }
}
