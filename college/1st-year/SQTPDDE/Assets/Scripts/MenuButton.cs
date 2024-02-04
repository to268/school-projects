using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string animation)
    {
        animator.SetBool(animation, true);
        animator.Play(animation);
        animator.SetBool(animation, false);
    }
}
