using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject[] levelPrefab;

    private int levelCount = 0;

    [SerializeField] GameObject playerInstantiate;
    [SerializeField] GameObject mapInstantiate;

    public delegate void LoadLevelDelegate();
    public static LoadLevelDelegate loadLevelEvent;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        UIManager.playGameEvent += LoadLevel;
        UIManager.retryLevelEvent += RetryLevel;
        UIManager.nextLevelEvent += NextLevel;
    }
    private void OnDisable()
    {
        UIManager.playGameEvent -= LoadLevel;
        UIManager.retryLevelEvent -= RetryLevel;
        UIManager.nextLevelEvent -= NextLevel;
    }

    private void LoadLevel()
    {
        playerInstantiate = Instantiate(playerPrefab);
        playerInstantiate.gameObject.name = "Player";
        Camera.main.GetComponent<CameraFollow>().target = playerInstantiate;
        loadLevelEvent?.Invoke();
        mapInstantiate = Instantiate(levelPrefab[levelCount]);
        mapInstantiate.gameObject.name = "Level";
    }

    private void NextLevel()
    {
        Destroy(playerInstantiate);
        Destroy(mapInstantiate);
        levelCount++;
        LoadLevel();
    }

    private void RetryLevel()
    {
        Destroy(playerInstantiate);
        Destroy(mapInstantiate);
        LoadLevel();
    }

}
