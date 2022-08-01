using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif

public class MainMenuScript : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        QApplication.Quit();
#endif
    }
}   
