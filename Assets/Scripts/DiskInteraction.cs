using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskInteraction : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject missedPanel;
    [SerializeField] ParticleSystem particle;

    public float missedPanelTime = 0.5f;
    public TileColor discColor = TileColor.Red;

    private Coroutine myCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //1. get tile script from the collision
        Tile tile = collision.GetComponent<Tile>();

        //2. check if tile is null
        if (tile == null)
        {
            return;
        }
            
        if (gameManager == null)
        {
            Debug.Log("empty gameManager!");
            return;
        }

        //?
        Vector2 hitPoint = collision.ClosestPoint(transform.position);

        //3. if tile/collision color state is equal to this game object/disc color hit
        if (tile.ColorState == discColor)
        {
            if (particle == null)
            {
                Debug.Log("empty particle!");
                return;
            }

            Instantiate(particle, hitPoint, Quaternion.identity);
            ChangeBGColor(tile.ColorState);
            gameManager.AddHit();
        }
        //miss
        else
        {
            gameManager.AddMiss();

            if (missedPanel == null)
            {
                Debug.Log("empty missedPanel!");
                return;
            }
            if (myCoroutine != null)
            {
                StopCoroutine(myCoroutine);
            }

            myCoroutine = StartCoroutine(MissedPanel());
        }
        collision.enabled = false;
    }

    private IEnumerator MissedPanel()
    {
        missedPanel.SetActive(true);

        yield return new WaitForSeconds(missedPanelTime);

        missedPanel.SetActive(false);
    }

    private void ChangeBGColor(TileColor color)
    {
        if (color == TileColor.Red)
        {
            Camera.main.backgroundColor = new Color(1f, 0.859f, 0.780f) * 0.7f;
        }
        else
        {
            Camera.main.backgroundColor = new Color(0.780f, 0.843f, 1f) * 0.7f;
        }
    }
}
