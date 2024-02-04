using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class MenuButtonController : MonoBehaviour
{
    //Initialisation of variables
    public int index;

    public AudioSource audioSource;

    [SerializeField] private MenuButton[] buttons;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (index <= 0)
                index = buttons.Length - 1;
            else
                index--;
            
            buttons[index].PlayAnimation("selected");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (index >= buttons.Length - 1)
                index = 0;
            else
                index++;
            
            buttons[index].PlayAnimation("selected");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttons[index].PlayAnimation("pressed");
        }
    }
}
