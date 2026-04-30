using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskInteraction : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    public TileColor discColor = TileColor.Red;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tile tile = collision.GetComponent<Tile>();

        if(tile != null)
        {
            tile.GetComponent<Collider2D>().enabled = false;
            
            if(gameManager == null)
            {
                Debug.Log("empty gameManager!");
                return;
            }

            if(tile.ColorState == discColor)
            {
                gameManager.AddHit();
                //Debug.Log("correct color!");
            }
            else
            {
                gameManager.AddMiss();
                //Debug.Log("wrong color!");
            }
        }
    }
}
