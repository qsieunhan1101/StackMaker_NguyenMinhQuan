using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject[] levelPrefab;

    [SerializeField] private int levelCount = 0;

    [SerializeField] GameObject playerInstantiate;
    [SerializeField] GameObject mapInstantiate;

    public delegate void LoadLevelDelegate();
    public static LoadLevelDelegate loadLevelEvent;

    private int levelSave;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PlayerPrefs.DeleteAll();
            Debug.Log("Level" + PlayerPrefs.GetInt("level_save"));
        }
    }
    private void OnEnable()
    {
        UIManager.playGameEvent += LoadLevel;
        UIManager.retryLevelEvent += RetryLevel;
        UIManager.nextLevelEvent += NextLevel;

        UIManager.continueEvent += LevelContinue;
    }
    private void OnDisable()
    {
        UIManager.playGameEvent -= LoadLevel;
        UIManager.retryLevelEvent -= RetryLevel;
        UIManager.nextLevelEvent -= NextLevel;

        UIManager.continueEvent -= LevelContinue;

    }

    private void LoadLevel(int index)
    {
        playerInstantiate = Instantiate(playerPrefab);
        playerInstantiate.gameObject.name = "Player";
        Camera.main.GetComponent<CameraFollow>().target = playerInstantiate;
        loadLevelEvent?.Invoke(); //setup CAMERA
        mapInstantiate = Instantiate(levelPrefab[index]);
        mapInstantiate.gameObject.name = "Level";
    }

    private void NextLevel()
    {
        Destroy(playerInstantiate);
        Destroy(mapInstantiate);

        int lv = PlayerPrefs.GetInt("level_save");
        lv = lv + 1;
        LoadLevel(lv);
        PlayerPrefs.SetInt("level_save", lv);
    }

    private void RetryLevel()
    {
        Destroy(playerInstantiate);
        Destroy(mapInstantiate);
        int lv = PlayerPrefs.GetInt("level_save");
        LoadLevel(lv);
    }

    public void LevelContinue()
    {
        int lv = PlayerPrefs.GetInt("level_save");
        LoadLevel(lv);
    }

    
}
