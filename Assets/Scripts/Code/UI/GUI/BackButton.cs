using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickBackButton);
    }

    void OnClickBackButton()
    {
        SceneManager.LoadScene(0);
    }
}