using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinDisc : MonoBehaviour
{
    public float speed = 0.2f;

    private float lastX;
    private bool isDragging;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            CheckIfTouchingDisc(touch);
           
            if (isDragging)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

                Vector2 dir = pos - transform.position;

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }


    private void CheckIfTouchingDisc(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(touch.position);
            Collider2D hit = Physics2D.OverlapPoint(worldPos);

            if (hit != null && hit.transform == transform)
            {
                isDragging = true;
            }
        }
    }
}
