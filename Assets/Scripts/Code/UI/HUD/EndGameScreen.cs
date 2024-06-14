using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Image timeFeedback;
    [SerializeField] private float waitTime = 8f;
    void Start()
    {
        if (GameManager.Instance.GameStateMachine.currentStateText == "GameOverState")
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
        float elapsedTime = 0f;
        Vector3 initialScale = timeFeedback.transform.localScale;
        Vector3 targetScale = new Vector3(0, initialScale.y, initialScale.z);

        while (elapsedTime < waitTime)
        {
            timeFeedback.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / waitTime);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        timeFeedback.transform.localScale = targetScale;

        yield return new WaitForSecondsRealtime(waitTime - elapsedTime);

        SceneManager.LoadScene(0);
        GameManager.Instance.GameStateMachine.SwitchState<MenuState>();
    }
}
