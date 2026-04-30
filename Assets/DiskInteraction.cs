using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskInteraction : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] ParticleSystem particle;


    public TileColor discColor = TileColor.Red;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tile tile = collision.GetComponent<Tile>();

        if (tile == null)
            return;

        if (gameManager == null)
        {
            Debug.Log("empty gameManager!");
            return;
        }

        Vector2 hitPoint = collision.ClosestPoint(transform.position);
        if (tile.ColorState == discColor)
        {
            if (particle == null)
            {
                Debug.Log("empty particle!");
                return;
            }

            Instantiate(particle, hitPoint, Quaternion.identity);
            gameManager.AddHit();
        }
        else
        {
            gameManager.AddMiss();
        }
        collision.enabled = false;
    }
}
