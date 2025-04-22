using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField]
    private TextMeshProUGUI gameOverText;  // ���� ���� �ؽ�Ʈ UI
    [SerializeField]
    private Player player;  // �÷��̾� ��ũ��Ʈ ����

    private bool isGameOver = false;

    void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);  // ���� �ÿ��� ���� ���� �ؽ�Ʈ ����
        }
    }

    void Update()
    {
        if (player != null && player.ui != null)
        {
            // �÷��̾��� ü���� 0 ������ �� ���� ���� ó��
            if (player.ui.GetCurrentHealth() <= 0 && !isGameOver)
            {
                GameOverSequence();
            }
        }
    }

    // ���� ���� �� ȣ��Ǵ� �Լ�
    void GameOverSequence()
    {
        isGameOver = true;

        // ���� ���� �ؽ�Ʈ Ȱ��ȭ
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);  // ���� ���� �ؽ�Ʈ ǥ��
        }

        // ���� �Ͻ� ����
        Time.timeScale = 0f;  // ���� �Ͻ� ����
    }
}
