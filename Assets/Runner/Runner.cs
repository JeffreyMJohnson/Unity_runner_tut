using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour
{

    public static float distanceTraveled;
    public float acceleration;
    public Vector3 jumpVelocity, boostVelocity;
    public float gameOverY;

    private bool touchingPlatforms;
    private static int boosts;

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
        boosts = 0;
        GUIManager.SetBoosts(boosts);
        distanceTraveled = 0f;
        GUIManager.SetDistance(distanceTraveled);
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
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (touchingPlatforms)
            {
                rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
                touchingPlatforms = false;
            }
            else if (boosts > 0)
            {
                rigidbody.AddForce(boostVelocity, ForceMode.VelocityChange);
                boosts -= 1;
                GUIManager.SetBoosts(boosts);
            }

        }
        distanceTraveled = transform.localPosition.x;
        GUIManager.SetDistance(distanceTraveled);

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

    internal static void AddBoost()
    {
        boosts += 1;
        GUIManager.SetBoosts(boosts);
    }
}
