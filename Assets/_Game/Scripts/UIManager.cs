using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button retryVictoryBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button retrySettingBtn;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button settingBtn;


    [SerializeField] private GameObject settingButton;

    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject settingUI;


    public delegate void PlayGameDelegate();
    public static PlayGameDelegate playGameEvent;
    public delegate void RetryLevelDelegate();
    public static RetryLevelDelegate retryLevelEvent;
    public delegate void NextLevelDelegate();
    public static NextLevelDelegate nextLevelEvent;
    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(OnStartGame);
        retryVictoryBtn.onClick.AddListener(OnRetry);
        nextBtn.onClick.AddListener(OnNextLevel);

        retrySettingBtn.onClick.AddListener(OnRetry);
        continueBtn.onClick.AddListener(OnContinue);

        settingBtn.onClick.AddListener(OnSetting);
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
        settingButton.SetActive(true);
        playGameEvent?.Invoke();
    }

    private void OnRetry()
    {
        settingButton.SetActive(true);

        victoryUI.SetActive(false);
        settingUI.SetActive(false);
        Time.timeScale = 1.0f;
        retryLevelEvent?.Invoke();
    }
    private void OnNextLevel()
    {
        settingButton.SetActive(true);
        victoryUI.SetActive(false);
        nextLevelEvent?.Invoke();
    }
    private void OnContinue()
    {
        settingButton.SetActive(true);
        settingUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void OnSetting()
    {
        settingButton.SetActive(false);
        settingUI.SetActive(true);
        Time.timeScale = 0;
    }
    private void FinishEvent()
    {
        settingButton.SetActive(false);
        victoryUI.SetActive(true);
    }
}
