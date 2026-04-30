using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskInteraction : MonoBehaviour
{
   public TileColor discColor = TileColor.Red;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tile tile = collision.GetComponent<Tile>();

        if(tile != null)
        {
            if(tile.ColorState == discColor)
            {
                Debug.Log("correct color!");
            }
            else
            {
                Debug.Log("wrong color!");
            }
        }
    }
}
