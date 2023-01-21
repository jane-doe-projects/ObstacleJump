using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10.0f;
    public float gravityModifier;
    public bool isOnGround = true;
    public int jumpCount = 0;
    public bool speedMode = false;

    public bool gameOver = false;

    public Animator playerAnim;
    private GameManager gameManagerScript;

    // particles
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtSplatterParticle;

    // sounds
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

        // with && isOnGround before instead of jumpCount
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && jumpCount < 2)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            jumpCount++;
            dirtSplatterParticle.Stop();
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCount = 0;
            dirtSplatterParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game over! Your final score is: "+ gameManagerScript.score);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtSplatterParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
