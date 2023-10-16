using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject loseText;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject resetButton;
    [SerializeField] private GameObject startButton;

    private void Start()
    {
        loseText.SetActive(false);
        winText.SetActive(false);
        resetButton.SetActive(false);
        startButton.SetActive(true);
    }

    public void StartUI(bool isOn)
    {
        startButton.SetActive(isOn);
    }

    public void ShowUI(bool isWin)
    {
        loseText.SetActive(!isWin);
        winText.SetActive(isWin);
        resetButton.SetActive(true);
    }
}
