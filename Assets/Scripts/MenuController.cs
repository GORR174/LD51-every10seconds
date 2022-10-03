using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject[] buttons;

    private int selection = 0;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selection = Math.Clamp(selection + 1, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selection = Math.Clamp(selection - 1, 0, 1);
        }
        
        for (var i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<SpriteRenderer>().color = selection == i ? Color.red : Color.white;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (selection)
            {
                case 0:
                    SceneManager.LoadScene("GameScene");
                    break;
                case 1:
                    Application.Quit();
                    break;
            }
        }
    }
}
