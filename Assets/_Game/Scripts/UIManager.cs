using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button retryVictoryBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button retrySettingBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button continueBtn;

    [SerializeField] private GameObject pauseButton;

    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject settingUI;


    public delegate void PlayGameDelegate(int level);
    public static PlayGameDelegate playGameEvent;
    public delegate void RetryLevelDelegate();
    public static RetryLevelDelegate retryLevelEvent;
    public delegate void NextLevelDelegate();
    public static NextLevelDelegate nextLevelEvent;


    public delegate void ContinueDelegate();
    public static ContinueDelegate continueEvent;
    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(OnStartGame);
        retryVictoryBtn.onClick.AddListener(OnRetry);
        nextBtn.onClick.AddListener(OnNextLevel);

        retrySettingBtn.onClick.AddListener(OnRetry);
        backBtn.onClick.AddListener(OnBackToGame);

        pauseButton.GetComponent<Button>().onClick.AddListener(OnPause);

        continueBtn.onClick.AddListener(OnContinueLevel);
    }


    private void OnEnable()
    {
        PlayerController.finishEvent += FinishEvent;
    }
    private void OnDisable()
    {
        PlayerController.finishEvent -= FinishEvent;

    }


    private void OnStartGame()
    {
        menuUI.SetActive(false);
        pauseButton.SetActive(true);
        playGameEvent?.Invoke(0);
        PlayerPrefs.SetInt("level_save", 0);
    }

    private void OnRetry()
    {
        pauseButton.SetActive(true);
        victoryUI.SetActive(false);
        settingUI.SetActive(false);
        Time.timeScale = 1.0f;
        retryLevelEvent?.Invoke();
    }
    private void OnNextLevel()
    {
        pauseButton.SetActive(true);
        nextLevelEvent?.Invoke();
        victoryUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
    private void OnBackToGame()
    {
        pauseButton.SetActive(true);
        settingUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void OnPause()
    {
        pauseButton.SetActive(false);
        settingUI.SetActive(true);
        Time.timeScale = 0;
    }


    private void OnContinueLevel()
    {
        menuUI.SetActive(false);
        pauseButton.SetActive(true);
        continueEvent?.Invoke();
    }

    private void FinishEvent()
    {
        StartCoroutine(FinishEventWaitTime());
        /*pauseButton.SetActive(false);
        victoryUI.SetActive(true);
        Time.timeScale = 0;*/
    }
    private IEnumerator FinishEventWaitTime()
    {
        yield return new WaitForSeconds(2f);
        pauseButton.SetActive(false);
        victoryUI.SetActive(true);
        Time.timeScale = 0;
    }

}
