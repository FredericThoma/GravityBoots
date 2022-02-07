using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 6000;
    public LineRenderer lineRenderer;
    public Transform gunTransform;
    public Transform endPoint;
    ManageGame gameManager;
    public bool activated = true;

    private void Awake()
    {
        gunTransform = GetComponent<Transform>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManageGame>();
    }

    private void Update()
    {  
        

        ShootLaser();
        
        
    }
    void ShootLaser()
    {

        if(Physics2D.Raycast(gunTransform.position, transform.right))
        {
            RaycastHit2D hit = Physics2D.Raycast(gunTransform.position, transform.right);
            Draw2DRay(gunTransform.position, hit.point);
        }
        else
        {
            Draw2DRay(gunTransform.position, gunTransform.transform.right * defDistanceRay);
        }


        if (Physics2D.Raycast(gunTransform.position, transform.right, Mathf.Abs(Vector3.Distance(transform.position, Physics2D.Raycast(gunTransform.position, transform.right).point) + 0.01f), 1 << LayerMask.NameToLayer("Player")) && gameManager.playerAlive == true && activated)
        {
            gameManager.EndGame();
        }

        if (!activated)
        {
            Draw2DRay(gunTransform.position, gunTransform.position);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

}
