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
        }
    }
}
