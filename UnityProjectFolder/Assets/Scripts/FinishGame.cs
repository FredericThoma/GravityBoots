
using UnityEngine;


public class FinishGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

    // Update is called once per frame
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerMovement>().enabled = false;
            SoundManager.PlaySound(SoundManager.Sound.CheckPoint);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenuScript>().levelEndMenu.SetActive(true);

        }

    }
}
