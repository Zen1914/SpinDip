using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    public void Spawn()
    {
        Instantiate(tilePrefab, transform.position, Quaternion.identity);   
    }
}
