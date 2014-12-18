using UnityEngine;

public class GUIManager : MonoBehaviour {

    public GUIText boostText, distanceText, gameOverText, instructionText, runnerText;

    private static GUIManager instance;

    void Start()
    {
        instance = this;
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        gameOverText.enabled = false;
    }

    public static void SetBoosts(int boosts)
    {
        instance.boostText.text = boosts.ToString();
    }

    public static void SetDistance(float distance)
    {
        instance.distanceText.text = distance.ToString("f0");
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GameEventManager.TriggerGameStart();
        }
    }

    private void GameStart()
    {
        gameOverText.enabled = false;
        instructionText.enabled = true;
        runnerText.enabled = false;
        enabled = false;
    }

    private void GameOver()
    {
        gameOverText.enabled = true;
        instructionText.enabled = true;
        enabled = true;
    }
}
