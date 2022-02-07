using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement playerMovement;
    public GameObject cameraa;
    public Vector3 playerSpawnPoint;

    public PauseMenuScript pauseMenuScript;
    public string gravDirOnSpawn = "down";
    public bool playerAlive = true;
    private void Awake()
    {

        pauseMenuScript = GameObject.FindGameObjectWithTag("PauseMenu").GetComponentInParent<PauseMenuScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        cameraa = GameObject.FindGameObjectWithTag("MainCamera");

        playerSpawnPoint = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        StartCoroutine(initialSpawn());





    }

    public void loadLevel(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void clearLevel()
    {

        Scene currentScene = SceneManager.GetActiveScene();

        int sceneID = currentScene.buildIndex - 2;
        LevelList.updateLevelStatus(sceneID, true);


        SceneManager.LoadScene(1);

    }


    public void EndGame()
    {

        playerAlive = false;
        SoundManager.PlaySound(SoundManager.Sound.PlayerDie);
        player.GetComponent<PlayerMovement>().gravDirection = new Vector2(0, 0);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody2D>().isKinematic = true;

        Animator playerAnimator = player.GetComponent<Animator>();
        
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Debug.Log("END GAME");
        playerAnimator.SetTrigger("Death");
        pauseMenuScript.deathMenu.SetActive(true);
        playerAnimator.SetBool("hasDied", true);
        playerAnimator.SetBool("hasSpawned", false);





    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        player.SetActive(false);
    }

    public IEnumerator initialSpawn()
    {

        yield return new WaitForSeconds(0.3f);
        playerAlive = true;
        player.SetActive(true);
        Animator playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetTrigger("Spawn");
        playerAnimator.SetBool("hasSpawned", true);

    }

    public void updateStarsOnPlayerDeath()
    {
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Coin");
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneID = currentScene.buildIndex - 2;
        for (int i = 0; i < 3; i++)
        {
            CoinPickup coinScript = stars[i].GetComponent<CoinPickup>();
            bool starSecured = coinScript.nextCheckPoint.reached;
            LevelList.updateStarsOfLevel(sceneID, coinScript.coinID, starSecured);
            coinScript.reached = false;

        }
    }

    public void respawn(Vector3 Spawnpoint, string gravDirOnSpawn)
    {

        updateStarsOnPlayerDeath();
        player.SetActive(true);

        Vector3 spawnpoint = Spawnpoint;


        pauseMenuScript.deathMenu.SetActive(false);
        PlayerMovement playerMovementScript = player.GetComponent<PlayerMovement>();
        playerMovementScript.enabled = true;
        player.GetComponent<CapsuleCollider2D>().enabled = true;
        player.GetComponent<Rigidbody2D>().isKinematic = false;


        switch (gravDirOnSpawn)

        {
            case "down":
                playerMovementScript.gravDirection = Vector2.down;
                break;
            case "up":
                playerMovementScript.gravDirection = Vector2.up;
                break;
            case "left":
                playerMovementScript.gravDirection = Vector2.left;
                break;
            case "right":
                playerMovementScript.gravDirection = Vector2.right;
                break;

        }
        player.transform.position = spawnpoint;


        StartCoroutine(initialSpawn());



    }



    public void UpdateSpawnPoint(Vector3 newSpawnPoint, string gravDir)
    {
        this.playerSpawnPoint = newSpawnPoint;
        gravDirOnSpawn = gravDir;
    }

}
