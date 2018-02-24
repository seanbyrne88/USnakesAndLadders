using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [HideInInspector]
    public GameManager Game;

    public UIState State;

    //Start Screen
    public GameObject StartScreen;
    private Button StartButton;
    private Button LoadButton;
    private Button QuitButton;

    //Player Setup
    public GameObject PlayerSelectScreen;
    private Dropdown PlayerSelectDropDown;
    private Button GoToBoardSetupButton;
    private int NumberOfPlayersSelected;
    

    //BoardSetup
    private GameObject BoardSetupScreen;
    private InputField BoardWidthInputField;
    private InputField BoardHeightInputField;
    private Button GoToGameButton;
    private int BoardWidthSelected;
    private int BoardHeightSelected;
    
	
	void Start () {
        AssignUIComponents();

        State = UIState.StartMenu;
        UpdateUI();
    }
	
	void UpdateUI()
    {
        switch (State)
        {
            case UIState.StartMenu:
                {
                    PlayerSelectScreen.SetActive(false);
                    BoardSetupScreen.SetActive(false);
                    StartScreen.SetActive(true);
                    break;
                }
            case UIState.PlayerSetupMenu:
                {
                    StartScreen.SetActive(false);
                    BoardSetupScreen.SetActive(false);
                    PlayerSelectScreen.SetActive(true);
                    break;
                }
            case UIState.BoardSetupMenu:
                {
                    StartScreen.SetActive(false);
                    PlayerSelectScreen.SetActive(false);
                    BoardSetupScreen.SetActive(true);
                    break;
                }
            case UIState.GameUI:
                {
                    StartScreen.SetActive(false);
                    PlayerSelectScreen.SetActive(false);
                    BoardSetupScreen.SetActive(false);
                    break;
                }
        }
    }

    void AssignUIComponents()
    {
        Game = FindObjectOfType<GameManager>();

        StartScreen = transform.Find("StartScreen").gameObject;
        PlayerSelectScreen = transform.Find("PlayerSelectScreen").gameObject;
        BoardSetupScreen = transform.Find("BoardSetupScreen").gameObject;

        //Start Screen UI Components
        StartButton = StartScreen.transform.Find("StartButton").GetComponent<Button>();
        LoadButton = StartScreen.transform.Find("LoadButton").GetComponent<Button>();
        QuitButton = StartScreen.transform.Find("QuitButton").GetComponent<Button>();

        //Start Screen Button Listeners
        StartButton.onClick.AddListener(StartButtonOnClick);
        QuitButton.onClick.AddListener(QuitButtonOnClick);

        //Player Select Screen UI Components
        PlayerSelectDropDown = PlayerSelectScreen.GetComponentInChildren<Dropdown>();
        GoToBoardSetupButton = PlayerSelectScreen.GetComponentInChildren<Button>();

        //Player Select Screen Button Listeners
        GoToBoardSetupButton.onClick.AddListener(GoToBoardSetupButtonOnClick);

        //Board Setup Screen UI Components
        BoardWidthInputField = BoardSetupScreen.transform.Find("BoardWidthInputField").GetComponent<InputField>();
        BoardHeightInputField = BoardSetupScreen.transform.Find("BoardHeightInputField").GetComponent<InputField>();
        BoardWidthInputField.text = 10.ToString();
        BoardHeightInputField.text = 10.ToString();
        GoToGameButton = BoardSetupScreen.GetComponentInChildren<Button>();

        //Board Setup Screen Button Listeners
        GoToGameButton.onClick.AddListener(GoToGameButtonOnClick);
    }

    #region Button Listeners
    void StartButtonOnClick()
    {
        State = UIState.PlayerSetupMenu;
        UpdateUI();
    }

    void QuitButtonOnClick()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    void GoToBoardSetupButtonOnClick()
    {
        NumberOfPlayersSelected = PlayerSelectDropDown.value+2; //+2 because 0 index is 2 players
        State = UIState.BoardSetupMenu;
        UpdateUI();
    }

    void GoToGameButtonOnClick()
    {
        BoardWidthSelected = int.Parse(BoardWidthInputField.text);
        BoardHeightSelected = int.Parse(BoardHeightInputField.text);
        State = UIState.GameUI;
        UpdateUI();
        Game.StartGame(BoardWidthSelected, BoardHeightSelected, NumberOfPlayersSelected);
    }
    #endregion
}

public enum UIState
{
    StartMenu,
    PlayerSetupMenu,
    BoardSetupMenu,
    PauseMenu,
    GameUI
}
