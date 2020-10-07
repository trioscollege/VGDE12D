using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager Instance { get; private set; }

    public IState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // seppeku!
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void OnEnable()
    {
        // first state of the game
        NewGameState(new IntroState());
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // scene loaded delegate
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentState.OnSceneLoaded();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateUpdate();
        }
    }

    public void NewGameState(IState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateExit();
        }

        CurrentState = newState;
        CurrentState.OnStateEnter();
    }
}