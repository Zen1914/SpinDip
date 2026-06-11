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
}
