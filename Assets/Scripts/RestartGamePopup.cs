using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RestartGamePopup : View {
    [SerializeField]
    private Button yesBtn;
    [SerializeField]
    private Button noBtn;

    void Start() {
        yesBtn.onClick.AddListener(delegate { RestartGame(); });
        noBtn.onClick.AddListener(delegate { OpenMainMenu(); });
    }

    private void RestartGame() {
        GameMaster.Instance.StartGame(GameMaster.Instance.gameModeWithTimer);
    }

    private void OpenMainMenu() {
        GameMaster.Instance.isPlayed = false;
        GameMaster.Instance.OpenMainMenuScreen();
    }
}
