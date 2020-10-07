public interface IState
{
    void OnSceneLoaded();
    void OnStateEnter();
    void OnStateExit();
    void OnStateUpdate();
}