using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBoundary : MonoBehaviour {

    private BoxCollider2D roomCollider;
    private GameObject player;

    private void Awake()
    {
        roomCollider = this.GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

	void LateUpdate () {
		if (player.GetComponent<CircleCollider2D>().IsTouching(roomCollider))
        {
            if (player.GetComponent<PlayerCameraController>().RoomBoundary != this)
            {
                player.GetComponent<PlayerCameraController>().RoomBoundary = this;
            }

            float cameraSizeY = Camera.main.orthographicSize * 2;
            float cameraSizeX = cameraSizeY * Screen.width / Screen.height;

            Vector3 position = Camera.main.transform.position;
            position.x = Mathf.Clamp(position.x, roomCollider.bounds.min.x + cameraSizeX/2, roomCollider.bounds.max.x - cameraSizeX/2);
            position.y = Mathf.Clamp(position.y, roomCollider.bounds.min.y + cameraSizeY/2, roomCollider.bounds.max.y - cameraSizeY/2);
            Camera.main.transform.position = position;


        }
    }
}
