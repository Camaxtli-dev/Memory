using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScreen : View {

    [SerializeField]
    private Button returnGameBtn;
    [SerializeField]
    private Button playBtn;
    [SerializeField]
    private Button exitBtn;
    [SerializeField]
    private GameObject gameScreen;
    [SerializeField]
    private GameObject ChoiceGameModePopup;
    
	void Start () {
        returnGameBtn.onClick.AddListener(delegate { ReturnGame(); });
        playBtn.onClick.AddListener(delegate { PlayClick(); });
        exitBtn.onClick.AddListener(delegate { ExitClick(); });
	}

    void Update() {
        if(GameMaster.Instance.isPlayed) {
            returnGameBtn.gameObject.SetActive(true);
        } else {
            returnGameBtn.gameObject.SetActive(false);
        }
    }

    private void ReturnGame() {
        GameMaster.Instance.OpenGameScreen();
    }

    private void PlayClick() {
        Hide();
        ChoiceGameModePopup.GetComponent<View>().Show();
    }

    private void ExitClick() {
        Application.Quit();
    }
}
