using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

    public void FadeScene(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("LoadNextScene");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
