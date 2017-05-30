using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : Singleton<GameMaster> {

    private const float timeToRoundConst = 120;
    private float timeToRound;

    [SerializeField]
    private GameObject card;
    [SerializeField]
    private Sprite[] facesCards;
    
    private List<GameObject> cardInPlane = new List<GameObject>();
    private List<Vector3> transforms = new List<Vector3>();

    public GameObject cardOne;
    public GameObject cardTwo;

    [SerializeField]
    private GameObject gameScreenGO;
    [SerializeField]
    private GameObject mainMenuScreenGO;
    [SerializeField]
    private GameObject restartGamePopupGO;
    [SerializeField]
    private GameObject choiceGameModePopupGO;

    private MainMenuScreen mainMenuScreen;
    private GameScreen gameScreen;
    private RestartGamePopup restartGamePopup;
    private ChoiceGameModePopup choiceGameModePopup;

    private bool inGame = false;
    public bool isPlayed = false;
    public bool gameModeWithTimer = false;

    protected override void Awake() {
        base.Awake();
        mainMenuScreen = mainMenuScreenGO.GetComponent<MainMenuScreen>();
        gameScreen = gameScreenGO.GetComponent<GameScreen>();
        restartGamePopup = restartGamePopupGO.GetComponent<RestartGamePopup>();
        choiceGameModePopup = choiceGameModePopupGO.GetComponent<ChoiceGameModePopup>();
    }

    void Start() {
        OpenMainMenuScreen();
    }

    void Update() {
        if(cardOne != null && cardTwo != null) {
            EqualsCard();
        }
        if(inGame && timeToRound > 0 && gameModeWithTimer) {
            timeToRound -= Time.deltaTime;
            gameScreen.SetTimer(Mathf.Round(timeToRound).ToString());
        } else if(inGame && timeToRound < 0 && gameModeWithTimer) {
            FailGame();
        }

        if(inGame && cardInPlane.Count <= 1) {
            FailGame();
        }
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)) {
            OpenMainMenuScreen();
        }
    }

    public void StartGame(bool gameMode) {
        gameScreen.SetTimer("");
        RemoveAllCard();
        CreateCardAndPositionToScreen();
        SetPositionToScreen();
        AddSpriteToCard();


        gameModeWithTimer = gameMode;
        timeToRound = timeToRoundConst;
        isPlayed = true;

        OpenGameScreen();
    }

    private void CreateCardAndPositionToScreen() {
        int h = 0;
        for(float i = -2; i <= 2; i++) {
            for(float j = -2; j <= 2; j++) {
                cardInPlane.Add(Instantiate(card) as GameObject);
                cardInPlane[h].transform.parent = gameScreenGO.transform;
                transforms.Add(new Vector3(110 * i, 140 * j, 0));
                h++;
            }
        }
    }

    private void SetPositionToScreen() {
        for(int i = 0; i < cardInPlane.Count; i++) {
            int j = Random.Range(0, transforms.Count);
            cardInPlane[i].transform.localPosition = transforms[j];
            cardInPlane[i].transform.localScale = Vector3.one;
            transforms.RemoveAt(j);
        }
    }

    private void AddSpriteToCard() {
        int p = 0;
        for(int i = 0; i < facesCards.Length; i++) {
            cardInPlane[p].GetComponent<Card>().cardID = i;
            cardInPlane[p++].GetComponent<Card>().faceCard = facesCards[i];
            cardInPlane[p].GetComponent<Card>().cardID = i;
            cardInPlane[p++].GetComponent<Card>().faceCard = facesCards[i];
        }
    }

    private void EqualsCard() {
        if(cardOne.GetComponent<Card>().cardID == cardTwo.GetComponent<Card>().cardID) {
            cardOne.GetComponent<Card>().Destroy();
            cardTwo.GetComponent<Card>().Destroy();
            cardInPlane.Remove(cardOne);
            cardInPlane.Remove(cardTwo);
        }
        cardOne = null;
        cardTwo = null;
    }

    public void AddCard(GameObject card) {
        if(cardOne == null)
            cardOne = card;
        else {
            Debug.Log(cardOne != card);
            if(cardOne != card) {
                cardTwo = card;
            }
        }
    }

    private void FailGame() {
        inGame = false;
        for(int i = 0; i < cardInPlane.Count; i++) {
            cardInPlane[i].GetComponent<Card>().Destroy();
        }
        cardInPlane.Clear();

        restartGamePopup.Show();
    }

    private void RemoveAllCard() {
        inGame = false;
        isPlayed = false;
        for(int i = 0; i < cardInPlane.Count; i++) {
            Destroy(cardInPlane[i]);
        }
        cardInPlane.Clear();
    }

    public void OpenMainMenuScreen() {
        inGame = false;
        restartGamePopup.Hide();
        choiceGameModePopup.Hide();
        gameScreen.Hide();
        mainMenuScreen.Show();
    }

    public void OpenGameScreen() {
        inGame = true;
        mainMenuScreen.Hide();
        choiceGameModePopup.Hide();
        restartGamePopup.Hide();
        gameScreen.Show();
    }
}
