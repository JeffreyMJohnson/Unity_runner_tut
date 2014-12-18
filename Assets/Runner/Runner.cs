using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {

    public static float distanceTraveled;
    public float acceleration;
    public Vector3 jumpVelocity;
    public float gameOverY;

    private bool touchingPlatforms;

    private Vector3 startPosition;

    void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        startPosition = transform.localPosition;
        renderer.enabled = false;
        rigidbody.isKinematic = true;
        enabled = false;
    }

    private void GameStart()
    {
        distanceTraveled = 0f;
        transform.localPosition = startPosition;
        renderer.enabled = true;
        rigidbody.isKinematic = false;
        enabled = true;
    }

    private void GameOver()
    {
        renderer.enabled = false;
        rigidbody.isKinematic = true;
        enabled = false;
    }

	// Update is called once per frame
	void Update () {
        if (touchingPlatforms && Input.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
            touchingPlatforms = false;
        }
        distanceTraveled = transform.localPosition.x;

        if (transform.localPosition.y < gameOverY)
        {
            GameEventManager.TriggerGameOver();
        }
	}

    void FixedUpdate()
    {
        if (touchingPlatforms)
        {
            rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
        }
    }

    void OnCollisionEnter()
    {
        touchingPlatforms = true;
    }
    void OnCollisionExit()
    {
        touchingPlatforms = false;
    }
}
