/*
 * General UI for game over etc
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject gameLoseUI;
    [SerializeField] private GameObject gameWinUI;
    
    private bool _gameIsOver;

    // Start is called before the first frame update
    private void Start()
    {
        ChaserMovement.OnChaserHasSpottedPlayer += ShowGameLoseUI;
        FindObjectOfType<ThirdPersonMovement>().OnReachedEndOfLevel += ShowGameWinUI;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_gameIsOver) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }

    // Show win UI when game over
    private void ShowGameWinUI()
    {
        OnGameOver(gameWinUI);
    }

    // Show lose UI when game over
    private void ShowGameLoseUI()
    {
        OnGameOver(gameLoseUI);
    }

    // Actions when game over
    private void OnGameOver(GameObject gameOverUI)
    {
        gameOverUI.SetActive(true);
        _gameIsOver = true;
        ChaserMovement.OnChaserHasSpottedPlayer -= ShowGameLoseUI;
        FindObjectOfType<ThirdPersonMovement>().OnReachedEndOfLevel -= ShowGameWinUI;
    }
}
