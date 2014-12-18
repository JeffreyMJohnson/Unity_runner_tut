using UnityEngine;

public class GUIManager : MonoBehaviour {

    public GUIText gameOverText, instructionText, runnerText;

    void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        gameOverText.enabled = false;
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
    }

    private void GameOver()
    {
        gameOverText.enabled = true;
        instructionText.enabled = true;
        enabled = true;
    }
}
