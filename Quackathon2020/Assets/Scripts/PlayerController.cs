using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float verticalInput = 0;
    float horizontalInput = 0;
    float jumpForce = 5;
    [SerializeField]
    float speedHorizontal = 15;

    float boundLeft = -20;
    float boundRight = 20;

    bool isOnGround = true;
    bool spedUp = false;

    Rigidbody rigidbody;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * speedHorizontal * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }

            if (transform.position.y < -20)
            {
                gameManager.GameOver();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            gameManager.UpdateScore(1);
        }

        if (other.gameObject.CompareTag("Speed Up"))
        {
            other.gameObject.SetActive(false);
            if (!spedUp)
            {
                spedUp = true;
                StartCoroutine(SpeedUp());
            }
            
        }

        if (other.gameObject.CompareTag("Duck"))
        {
            other.gameObject.SetActive(false);
            gameManager.UpdateScore(-1);
        }

    }


    private IEnumerator SpeedUp()
    {
        var current = Time.timeScale;
        Time.timeScale = current / 2;
        yield return new WaitForSeconds(3);
        Time.timeScale = current;
        spedUp = false;
    }
}
