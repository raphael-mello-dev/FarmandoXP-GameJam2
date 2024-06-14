using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private GameObject victoryPanel;

    void Start()
    {
        if(GameManager.Instance.GameStateMachine.currentStateText == "GameOverState")
        {
            defeatPanel.SetActive(true);
        }
        else if (GameManager.Instance.GameStateMachine.currentStateText == "GameWonState")
        {
            victoryPanel.SetActive(true);
        }

        StartCoroutine(OnEndGame());
    }

    private IEnumerator OnEndGame()
    {
        yield return new WaitForSecondsRealtime(8);
        SceneManager.LoadScene(0);
        GameManager.Instance.GameStateMachine.SwitchState<MenuState>();
    }
}
