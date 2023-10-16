using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;

    private UiManager uiManager;

    private LoseWinState loseWinState;

    private void Start()
    {
        loseWinState = LoseWinState.instance;

        uiManager = GetComponent<UiManager>();
        if (uiManager == null)
        {
            Debug.LogError("UiManager not found on GameManager GameObject.");
        }

        loseWinState.GameManagerConector(this);
        loseWinState.stopGame = true;
    }

    public void StartGame()
    {
        uiManager.StartUI(false);
        loseWinState.stopGame = false;
    }

    public void GameOver(bool isWin)
    {
        cameraFollow.enabled = false;
        uiManager.ShowUI(isWin);
        SoundManager.instance.PlayGameOverSound(isWin);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
