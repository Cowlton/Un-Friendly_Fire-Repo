using System;
using System.Collections.Generic;
using UnityEngine;

public class JoinPromptHandlerScr : MonoBehaviour
{

    [SerializeField] private List<JoinPromptScr> joinPrompts; 
    private int _promptCount = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < joinPrompts.Count; i++)
        {
            if (i != 0)
            {
                joinPrompts[i].gameObject.SetActive(false);
            }
            else
            {
                joinPrompts[i].Appear();
            }
        }
    }

    public void ShowJoinPrompt()
    {
        if (_promptCount < joinPrompts.Count)
        {
            if (joinPrompts[_promptCount] != null)
            {
                joinPrompts[_promptCount].Appear();
                _promptCount ++;
            }
        }
    }

    public void HideJoinPrompt()
    {
        _promptCount--;
        joinPrompts[_promptCount].Leave();
    }
}
