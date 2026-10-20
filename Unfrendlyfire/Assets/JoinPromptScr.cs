using UnityEngine;

public class JoinPromptScr : MonoBehaviour
{

    [SerializeField]private Animator animator;

    private bool _active;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public void Appear()
    {
        
        _active = true;
        gameObject.SetActive(true);
        animator.SetBool("Appear", true);
        
    }

    public void Idle()
    {
        animator.SetBool("Appear", false);
    }

    public void Leave()
    {
        if (_active)
        {
            animator.SetBool("Leave", true);
            _active = false;
        }
    }

    public void Hide()
    {
        if (!_active)
        {
            gameObject.SetActive(false);
        }
        animator.SetBool("Leave", false);
    }
    
}
