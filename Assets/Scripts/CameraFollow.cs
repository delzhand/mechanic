﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    public float verticalOffset;
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    public Vector2 focusAreaSize;

    FocusArea focusArea;

    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    bool lookAheadStopped;

    void Start()
    {
        focusArea = new FocusArea(target.GetComponent<BoxCollider2D>().bounds, focusAreaSize);
    }

    void LateUpdate()
    {
        focusArea.Update(target.GetComponent<BoxCollider2D>().bounds);

        Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;

        if (focusArea.velocity.x != 0)
        {
            lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
            if (Mathf.Sign(Input.GetAxis("Horizontal")) == Mathf.Sign(focusArea.velocity.x) && Input.GetAxis("Horizontal") != 0)
            {
                lookAheadStopped = false;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;
            }
            else
            {
                if (!lookAheadStopped)
                {
                    lookAheadStopped = true;
                    targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
                }
            }
        }


        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        focusPosition += Vector2.right * currentLookAheadX;
        transform.position = (Vector3)focusPosition + Vector3.forward * -10;

        // Constrain Camera to Room
        float cameraSizeY = Camera.main.orthographicSize * 2;
        float cameraSizeX = cameraSizeY * Screen.width / Screen.height;
        Vector3 position = Camera.main.transform.position;
        BoxCollider2D roomCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCameraController>().RoomBoundary.GetComponent<BoxCollider2D>();
        position.x = Mathf.Clamp(position.x, roomCollider.bounds.min.x + cameraSizeX / 2, roomCollider.bounds.max.x - cameraSizeX / 2);
        position.y = Mathf.Clamp(position.y, roomCollider.bounds.min.y + cameraSizeY / 2, roomCollider.bounds.max.y - cameraSizeY / 2);
        Camera.main.transform.position = position;

    }

    struct FocusArea
    {
        public Vector2 center;
        public Vector2 velocity;
        float left, right;
        float top, bottom;


        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }

}