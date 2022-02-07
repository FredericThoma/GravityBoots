
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject levelEndMenu;
    public bool isPaused;
    void Start()
    {
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void resumeGame()
    {

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void respawn()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            SceneManager.LoadScene(7);
        }

        Vector3 playerSpawnPoint = gameManager.GetComponent<ManageGame>().playerSpawnPoint;
        string gravDirOnSpawn = gameManager.GetComponent<ManageGame>().gravDirOnSpawn;
        gameManager.GetComponent<ManageGame>().respawn(playerSpawnPoint, gravDirOnSpawn);
    }

    public void goToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }

    public void clearLevel()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<ManageGame>().clearLevel();
    }

    public void quitApplication()
    {
        Application.Quit();
    }
}
