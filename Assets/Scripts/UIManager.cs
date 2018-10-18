using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public void LoadLevel(int LevelToLoad)
	{
		SceneManager.LoadScene(LevelToLoad);
	}

    public void ExitGame()
    {
        Application.Quit();
    }
}
