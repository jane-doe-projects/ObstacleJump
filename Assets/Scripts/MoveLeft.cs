using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 15;
    private PlayerController playerControllerScript;

    private float leftBoud = -15;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            if (Input.GetKey(KeyCode.LeftControl) && playerControllerScript.isOnGround)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * 1.3f);
                // change animation speed
                playerControllerScript.playerAnim.speed = 3f;
                playerControllerScript.speedMode = true;
            } else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                // normal animation speed
                playerControllerScript.playerAnim.speed = 1.5f;
                playerControllerScript.speedMode = false;
            }

        }

        if (transform.position.x < leftBoud && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        
    }
}
