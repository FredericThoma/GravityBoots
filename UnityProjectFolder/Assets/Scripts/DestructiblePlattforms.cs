using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructiblePlattforms : MonoBehaviour
{
    public Sprite[] dpSprites = new Sprite[3];
    public SpriteRenderer platform;


    public void Awake()
    {
        platform = this.GetComponent<SpriteRenderer>();
        
        this.GetComponent<BoxCollider2D>().enabled = true;
        platform.sprite = dpSprites[0];

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        platform.sprite = dpSprites[1];
        StartCoroutine(destroy());

    }

    public IEnumerator destroy()
    {
        yield return new WaitForSeconds(1);
        platform.sprite = dpSprites[2];
        this.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        this.GetComponent<SpriteRenderer>().enabled = true;
        platform.sprite = dpSprites[0];
        this.GetComponent<BoxCollider2D>().enabled = true;


    }
}
