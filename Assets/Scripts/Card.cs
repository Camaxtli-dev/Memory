using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public Sprite backCard;
    public Sprite faceCard;

    public int cardID;
    
    private float smoothTime = 0.3F;
    private float yVelocity = 0;

    private bool clickCard = false;
    private bool destr = false;

    void Start() {
        GetComponent<Image>().sprite = backCard;
    }

    void Update() {
        if(clickCard) {
            float newRotation = Mathf.SmoothDamp(transform.localEulerAngles.y, 90, ref yVelocity, smoothTime);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, newRotation, transform.localEulerAngles.z);
        } else {
            float newRotation = Mathf.SmoothDamp(transform.localEulerAngles.y, 0, ref yVelocity, smoothTime);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, newRotation, transform.localEulerAngles.z);
        }
    }

    public void PointerClick() {
        StartCoroutine("CardClick");
        GameMaster.Instance.AddCard(gameObject);
    }

    private IEnumerator CardClick() {
        StartCoroutine("OpenCard");
        yield return new WaitForSeconds(2.5f);
        if(destr == false) {
            StartCoroutine("ClosedCard");
        } else {
            Destroy(gameObject);
        }
    }

    private IEnumerator OpenCard() {
        clickCard = true;
        yield return new WaitForSeconds(1);
        GetComponent<Image>().sprite = faceCard;
        clickCard = false;
    }

    private IEnumerator ClosedCard() {
        clickCard = true;
        yield return new WaitForSeconds(1);
        GetComponent<Image>().sprite = backCard;
        clickCard = false;
    }

    public void Destroy() {
        destr = true;
        StopAllCoroutines();
        StartCoroutine("CardClick");
    }
}
