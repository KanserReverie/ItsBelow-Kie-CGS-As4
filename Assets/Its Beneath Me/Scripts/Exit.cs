using UnityEngine;

public class Exit : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }

    public void QuitGame()
    {
    #region Quit on ESC
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif

    #endregion
    }
}