using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public Animator animator;

    private int level_to_load;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel(int level_index) 
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() 
    {
        SceneManager.LoadScene();
    }
}
