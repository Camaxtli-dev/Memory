using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScreen : View {
    [SerializeField]
    private Text timer;

    public void SetTimer(string s) {
        timer.text = s;
    }
}
