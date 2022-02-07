using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RisingLevelManager : MonoBehaviour
{

    public GameObject[] lasers;
    void Awake()
    {
        lasers = GameObject.FindGameObjectsWithTag("Laser").OrderBy(go => go.name).ToArray();

        for(int i = 0; i<lasers.Length; i++)
        {
            Debug.Log(lasers[i].name);
            lasers[i].GetComponentInChildren<Laser>().activated = false;
            if(i>4 && i%5 == 0)
            {
                lasers[i].GetComponentInChildren<Laser>().activated = true;
            }
        }

        StartCoroutine(activeLasersStart());
        StartCoroutine(despawnAnitExploitLasers());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.time);
    }

    public IEnumerator activeLasersStart()
    {
        for(int i=0; i<lasers.Length; i++)
        {
            yield return new WaitForSeconds(1.5f-(i * 0.01f));
            lasers[i].GetComponentInChildren<Laser>().activated = true;

        }
    }

    public IEnumerator despawnAnitExploitLasers()
    {
        for(int i=5; i<lasers.Length; i += 5)
        {
            yield return new WaitForSeconds(3.5f);
            lasers[i].GetComponentInChildren<Laser>().activated = false;
        }
    }

}
