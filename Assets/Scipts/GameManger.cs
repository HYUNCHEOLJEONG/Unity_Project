using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI readyText;  // READY �ؽ�Ʈ UI
    [SerializeField] private TextMeshProUGUI startText;  // START �ؽ�Ʈ UI
    [SerializeField] private float readyTime = 2f;  // READY �ؽ�Ʈ ���� �ð�
    [SerializeField] private float startTime = 1f;  // START �ؽ�Ʈ ���� �ð�
    [SerializeField] private TextMeshProUGUI roundText; // ���� �ؽ�Ʈ UI
    [SerializeField] private float roundTextTime = 1f; // ���� ǥ�� �ð�
    private bool isGameEnded = false;
    private bool isGameStarted = false;
    [Header("Game Settings")]
    [SerializeField] private Player player;  // �÷��̾� ��ũ��Ʈ ����
    private bool gameStarted = false;

    void Start()
    {
        isGameStarted = true;
        if (readyText != null && startText != null)
        {
            readyText.gameObject.SetActive(true);  // READY �ؽ�Ʈ ���̱�
            startText.gameObject.SetActive(false);  // START �ؽ�Ʈ �����
        }

        if (player != null)
        {
            // ���� ���� �� �÷��̾� ��ũ��Ʈ�� ��Ȱ��ȭ
            player.enabled = false;  // �巡���� ��ũ��Ʈ ��Ȱ��ȭ (�̵����� �ʵ���)
        }

        StartCoroutine(GameStartSequence());
    }
    public void ShowRoundText(string text, System.Action onComplete)
    {
        StartCoroutine(ShowRoundCoroutine(text, onComplete));
    }

    private IEnumerator GameStartSequence()
    {
        yield return new WaitForSeconds(readyTime);

        if (readyText != null)
        {
            readyText.gameObject.SetActive(false);
        }

        if (startText != null)
        {
            startText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(startTime);

        if (startText != null)
        {
            startText.gameObject.SetActive(false);
        }

        // ���� ���� �� �÷��̾��� ��ũ��Ʈ Ȱ��ȭ
        if (player != null)
        {
            player.enabled = true;  // �巡���� ��ũ��Ʈ Ȱ��ȭ
        }

        gameStarted = true;
    }
   
    private IEnumerator ShowRoundCoroutine(string text, System.Action onComplete)
    {
        roundText.text = text;
        roundText.gameObject.SetActive(true);

        yield return new WaitForSeconds(roundTextTime);

        roundText.gameObject.SetActive(false);

        onComplete?.Invoke(); // �������� �ݹ� ����
    }
    public void EndGame()
    {
        if (isGameEnded) return; // �ߺ� ����
        isGameEnded = true;
        Debug.Log("Game Over - WIN");
        // �ʿ�� �� ��ȯ �Ǵ� ��ư Ȱ��ȭ �� ó��
    }
    public bool IsGameEnded()
    {
        return isGameEnded;
    }
    public bool IsGameStarted()
    {
        return isGameStarted;
    }

}

