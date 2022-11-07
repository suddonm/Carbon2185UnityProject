using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState 
{
    Menu,
    World,
    Combat
}

public delegate void OnGameStateChangeHandler();

public class GameManager : MonoBehaviour
{
    protected GameManager(){}
    private static GameManager instance = null;

    public GameObject dialogBox;

    public GameState gameState { get; private set; }
    public event OnGameStateChangeHandler OnGameStateChange;

    public static GameManager Instance
    {
        get 
        {
            if (GameManager.instance == null)
            {
                GameManager.instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(GameManager.instance);
            }
        
            return GameManager.instance;
        }
    }

    public void OnApplicationQuit(){
        GameManager.instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Make sure the dialog box isn't visible
        dialogBox.SetActive(false);

        //Set the movement mode to world        
        SetGameState(GameState.World);
    }

    public void SetGameState(GameState state){
        this.gameState = state;
        OnGameStateChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDialogBox()
    {
        dialogBox.SetActive(true);
    }

    public void CloseDialogBox()
    {
        dialogBox.SetActive(false);
    }

    
}
