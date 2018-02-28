using BoardGame;

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //[HideInInspector]
    public GameManager Game;

    public UIState State;

    public Sprite[] DiceSprites;

    //Start Screen
    private GameObject StartScreen;
    private Button StartButton;
    private Button LoadButton;
    private Button QuitButton;

    //Player Setup
    private GameObject PlayerSelectScreen;
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

    //GameUI
    private GameObject GameScreen;
    private Text CurrentPlayerText;
    private Button RollDiceButton;
    private Image LastRollImage;

    private void Awake()
    {
        AssignUIComponents();
        UpdateUI(UIState.StartMenu);
    }

    void Start ()
    {
        Game = FindObjectOfType<GameManager>();
    }
	
	private void UpdateUI(UIState State)
    {
        this.State = State;

        switch (State)
        {
            case UIState.StartMenu:
                {
                    PlayerSelectScreen.SetActive(false);
                    BoardSetupScreen.SetActive(false);
                    GameScreen.SetActive(false);
                    StartScreen.SetActive(true);
                    break;
                }
            case UIState.PlayerSetupMenu:
                {
                    StartScreen.SetActive(false);
                    BoardSetupScreen.SetActive(false);
                    GameScreen.SetActive(false);
                    PlayerSelectScreen.SetActive(true);
                    break;
                }
            case UIState.BoardSetupMenu:
                {
                    StartScreen.SetActive(false);
                    PlayerSelectScreen.SetActive(false);
                    GameScreen.SetActive(false);
                    BoardSetupScreen.SetActive(true);
                    break;
                }
            case UIState.GameUI:
                {
                    StartScreen.SetActive(false);
                    PlayerSelectScreen.SetActive(false);
                    BoardSetupScreen.SetActive(false);
                    GameScreen.SetActive(true);
                    break;
                }
        }
    }

    public void RefreshCurrentPlayerText()
    {
        CurrentPlayerText.text = string.Format("{0}'s Turn", Game.CurrentPlayer().PlayerName);
        
    }

    public void RefreshDiceRollText(int DiceRoll)
    {
        print(DiceRoll);
        LastRollImage.sprite = DiceSprites[DiceRoll - 1]; //-1 for 0 based arrays
    }

    void AssignUIComponents()
    {
        //Canvas Components (These are the main UI components that get enabled/disabled)
        StartScreen = transform.Find("StartScreen").gameObject;
        PlayerSelectScreen = transform.Find("PlayerSelectScreen").gameObject;
        BoardSetupScreen = transform.Find("BoardSetupScreen").gameObject;
        GameScreen = transform.Find("GameScreen").gameObject;

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

        //Game Screen UI Components
        CurrentPlayerText = GameScreen.GetComponentInChildren<Text>();
        RollDiceButton = GameScreen.GetComponentInChildren<Button>();
        LastRollImage = GameScreen.transform.Find("DiceRollImage").GetComponent<Image>();

        //Game Screen Button Listeners
        RollDiceButton.onClick.AddListener(RollDiceButtonOnClick);
    }

    #region Button Listeners
    void StartButtonOnClick()
    {
        UpdateUI(UIState.PlayerSetupMenu);
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
        UpdateUI(UIState.BoardSetupMenu);
    }

    void GoToGameButtonOnClick()
    {
        BoardWidthSelected = int.Parse(BoardWidthInputField.text);
        BoardHeightSelected = int.Parse(BoardHeightInputField.text);

        UpdateUI(UIState.GameUI);

        Game.StartGame(BoardWidthSelected, BoardHeightSelected, NumberOfPlayersSelected);
        RefreshCurrentPlayerText(); //after game is initialized, set Game UI components based on game manager state
    }

    void RollDiceButtonOnClick()
    {
        Game.RollDiceAndMovePlayer();
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
