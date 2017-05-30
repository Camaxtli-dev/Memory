using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChoiceGameModePopup : View {

    [SerializeField]
    private Button yesBtn;
    [SerializeField]
    private Button noBtn;

    void Start() {
        yesBtn.onClick.AddListener(delegate { RestartGame(true); });
        noBtn.onClick.AddListener(delegate { RestartGame(false); });
    }

    private void RestartGame(bool gameMode) {
        GameMaster.Instance.StartGame(gameMode);
    }
}
