using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskInteraction : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject missedPanel;
    [SerializeField] ParticleSystem particle;
    [SerializeField] AudioSource beatSound;

    public float missedPanelTime = 0.5f;
    public TileColor discColor = TileColor.Red;

    private Coroutine myCoroutine;
    private Coroutine bgColorRoutine;
    private Coroutine shakeRoutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //1. get tile script from the collision
        Tile tile = collision.GetComponent<Tile>();

        //2. check if tile is null
        if (tile == null)
        {
            return;
        }
            
        if (gameManager == null || beatSound == null)
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

            beatSound.Stop();
            beatSound.Play();

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

    //private void ChangeBGColor(TileColor color)
    //{
    //    if (color == TileColor.Red)
    //    {
    //        Camera.main.backgroundColor = new Color(1f, 0.859f, 0.780f) * 0.7f;
    //    }
    //    else
    //    {
    //        Camera.main.backgroundColor = new Color(0.780f, 0.843f, 1f) * 0.7f;
    //    }
    //}

    private void ChangeBGColor(TileColor color)
    {
        Color targetColor;

        if (color == TileColor.Red)
        {
            targetColor = new Color(1f, 0.859f, 0.780f) * 0.7f;
        }
        else
        {
            targetColor = new Color(0.780f, 0.843f, 1f) * 0.7f;
        }

        if (bgColorRoutine != null)
            StopCoroutine(bgColorRoutine);

        bgColorRoutine = StartCoroutine(LerpBackground(targetColor));

        float strength = Random.Range(0.01f, 0.05f);
        ShakeCamera(0.05f, strength);
    }

    private IEnumerator LerpBackground(Color targetColor)
    {
        Color startColor = Camera.main.backgroundColor;
        float duration = 0.5f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            Camera.main.backgroundColor =
                Color.Lerp(startColor, targetColor, time / duration);

            yield return null;
        }

        Camera.main.backgroundColor = targetColor;
    }

    private void ShakeCamera(float duration, float strength)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(Shake(duration, strength));
    }

    private IEnumerator Shake(float duration, float strength)
    {
        Vector3 originalPos = Camera.main.transform.localPosition;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            Camera.main.transform.localPosition =
                originalPos + (Vector3)Random.insideUnitCircle * strength;

            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
    }
}
