using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    bool isMenuActive = false;

    GameManager _GameManager;

    void Awake()
    {
        _GameManager = GameManager.Instance;
        _GameManager.OnGameStateChange += HandleOnGameStateChange;

        isMenuActive = false;
    }

    public void HandleOnGameStateChange()
    {
        //Do Something
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuActive = !isMenuActive;
            Debug.Log("Menu key pressed, isMenuActive:" + isMenuActive);
        }
    }
}
