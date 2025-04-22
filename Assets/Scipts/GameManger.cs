using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI readyText;  // READY 텍스트 UI
    [SerializeField] private TextMeshProUGUI startText;  // START 텍스트 UI
    [SerializeField] private float readyTime = 2f;  // READY 텍스트 지속 시간
    [SerializeField] private float startTime = 1f;  // START 텍스트 지속 시간
    [SerializeField] private TextMeshProUGUI roundText; // 라운드 텍스트 UI
    [SerializeField] private float roundTextTime = 1f; // 라운드 표시 시간
    private bool isGameEnded = false;
    private bool isGameStarted = false;
    [Header("Game Settings")]
    [SerializeField] private Player player;  // 플레이어 스크립트 참조
    private bool gameStarted = false;

    void Start()
    {
        isGameStarted = true;
        if (readyText != null && startText != null)
        {
            readyText.gameObject.SetActive(true);  // READY 텍스트 보이기
            startText.gameObject.SetActive(false);  // START 텍스트 숨기기
        }

        if (player != null)
        {
            // 게임 시작 전 플레이어 스크립트를 비활성화
            player.enabled = false;  // 드래곤의 스크립트 비활성화 (이동하지 않도록)
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

        // 게임 시작 후 플레이어의 스크립트 활성화
        if (player != null)
        {
            player.enabled = true;  // 드래곤의 스크립트 활성화
        }

        gameStarted = true;
    }
   
    private IEnumerator ShowRoundCoroutine(string text, System.Action onComplete)
    {
        roundText.text = text;
        roundText.gameObject.SetActive(true);

        yield return new WaitForSeconds(roundTextTime);

        roundText.gameObject.SetActive(false);

        onComplete?.Invoke(); // 끝났으면 콜백 실행
    }
    public void EndGame()
    {
        if (isGameEnded) return; // 중복 방지
        isGameEnded = true;
        Debug.Log("Game Over - WIN");
        // 필요시 씬 전환 또는 버튼 활성화 등 처리
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

