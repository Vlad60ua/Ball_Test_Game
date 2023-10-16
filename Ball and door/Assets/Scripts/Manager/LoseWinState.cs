using UnityEngine;

public class LoseWinState : MonoBehaviour
{
    public static LoseWinState instance;

    public bool stopGame;

    public bool winJump;

    private GameManager gameManager;

    private void Awake()
    {
        instance = this;
    }

    public void Win()
    {
        winJump = true;
        stopGame = true;
        if (gameManager != null)
        {
            gameManager.GameOver(true);
        }
        else
        {
            Debug.LogError("GameManager is not assigned to LoseWinState.");
        }
    }

    public void Lose()
    {
        stopGame = true;
        if (gameManager != null)
        {
            gameManager.GameOver(false);
        }
        else
        {
            Debug.LogError("GameManager is not assigned to LoseWinState.");
        }
    }

    public void GameManagerConector(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
