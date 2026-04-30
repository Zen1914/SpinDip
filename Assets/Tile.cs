using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tile : MonoBehaviour
{
    [SerializeField] Sprite spriteBlue;
    [SerializeField] Sprite spriteRed;

    public TileColor ColorState => colorState;
    public float speed = 2.0f;
    public float sinkDistance = 0.5f;

    private Transform disk;
    private Vector2 dir;
    private Transform tileVisuals;
    private SpriteRenderer sr;
    private TileColor colorState = TileColor.Red;


    private void Awake()
    {
        tileVisuals = transform.GetChild(0);
        sr = tileVisuals.GetComponent<SpriteRenderer>();
        disk = FindAnyObjectByType<SpinDisc>().transform;
        FaceToDisc();
        PickRandomColor();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, disk.position, speed * Time.deltaTime);
        float dist = Vector2.Distance(transform.position, disk.position);

        if (dist < sinkDistance)
        {
            Destroy(this.gameObject);
        }
    }

    private void FaceToDisc()
    {
        dir = transform.position - disk.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void PickRandomColor()
    {
        if (Random.value > 0.5)
        {
            colorState = TileColor.Blue;
            sr.sprite = spriteBlue;
        }
        else
        {
            colorState = TileColor.Red;
            sr.sprite = spriteRed;
        }
    }
}
public enum TileColor
{
    Red,
    Blue
}
