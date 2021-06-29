using UnityEngine;

public class Ball : MonoBehaviour
{

    // configuration parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPushVelocity = 2f;
    [SerializeField] float yPushVelocity = 15f;
    [SerializeField] float randomBallFactor = 0.2f;
    [SerializeField] AudioClip[] ballSounds;

    // cached component refrences
    AudioSource myAudioSource;
    Rigidbody2D myRigidbody2D;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (!hasStarted)
        {
            LockBallToPaddle();
            LunchBallOnMouseClick();
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }

    private void LunchBallOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidbody2D.velocity = new Vector2(xPushVelocity, yPushVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityChange = new Vector2(
                     Random.Range(0f, randomBallFactor),
                     Random.Range(0f, randomBallFactor));
      
        if (hasStarted)
        {
            AudioClip audioClip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(audioClip);
            myRigidbody2D.velocity += velocityChange;
        }
    }
}



