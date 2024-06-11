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
        GameManager.Instance.AudioManager.PlayMenuSFX(SFXs.ButtonClick);
        SceneManager.LoadScene(0);
    }
}