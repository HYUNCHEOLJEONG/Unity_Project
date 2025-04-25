using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI readyText;  // READY text UI
    [SerializeField] private TextMeshProUGUI startText;  // START text UI
    [SerializeField] private float readyTime = 2f;  // READY  표시 시간
    [SerializeField] private float startTime = 1f;  // START  표시 시간
    [SerializeField] private TextMeshProUGUI roundText; // ROUND text UI
    [SerializeField] private float roundTextTime = 1f; // ROUND text 표시 시간
    private bool isGameEnded = false; // 게임이 종료되었는지 체크하는 변수
    private bool isGameStarted = false; // 게임이 시작되었는지 체크하는 변수
    [Header("Game Settings")]
    [SerializeField] private Player player;  // 플레이어 스크립트 참조
    private bool gameStarted = false;

    void Start()
    {
        isGameStarted = true;
        if (readyText != null && startText != null)
        {
            readyText.gameObject.SetActive(true);  
            startText.gameObject.SetActive(false); // 시작할 때는 START 텍스트를 비활성화
        }

        if (player != null)
        {
         
            player.enabled = false;  // 플레이어 스크립트 비활성화
        }

        StartCoroutine(GameStartSequence());
    }
    public void ShowRoundText(string text, System.Action onComplete)
    {
        StartCoroutine(ShowRoundCoroutine(text, onComplete));
    }

    private IEnumerator GameStartSequence()
    {
        yield return new WaitForSeconds(readyTime); // readyTime만큼 대기

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

    
        if (player != null)
        {
            player.enabled = true;  // 플레이어 스크립트 활성화
        }

        gameStarted = true;
    }
   
    private IEnumerator ShowRoundCoroutine(string text, System.Action onComplete)
    {
        roundText.text = text;
        roundText.gameObject.SetActive(true);

        yield return new WaitForSeconds(roundTextTime); // ROUND 텍스트 표시 시간

        roundText.gameObject.SetActive(false);

        onComplete?.Invoke(); // 콜백 함수 실행
    }
    public void EndGame()
    {
        if (isGameEnded) return; // 이미 게임이 종료된 경우 함수 종료
        isGameEnded = true;
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

