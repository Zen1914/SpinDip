using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    public void Spawn(bool isRed)
    {
        GameObject tile = Instantiate(tilePrefab, transform.position, Quaternion.identity);
        Tile tileScript = tile.GetComponent<Tile>();

        if(tileScript == null)
        {
            return;
        }

        tileScript.PickRandomColor(isRed);
    }
}
