using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionText : MonoBehaviour {

    Text thisText;
    RectTransform thisTransform;
    Vector3 originalPosition;

    Vector3 tickMove;

    void Start ()
    {
        thisText = GetComponent<Text>();
        thisTransform = GetComponent<RectTransform>();
        originalPosition = thisTransform.localPosition;

        thisText.text = "";
    }

	void Update () {
        thisTransform.localPosition += tickMove;
	}

    public void Move(string incText, float incTime, Vector3 incTick)
    {
        thisText.text = incText;
        tickMove = incTick;
        StartCoroutine(Reset(incTime));
    }

    IEnumerator Reset(float incTime)
    {
        yield return new WaitForSeconds(incTime);

        tickMove = new Vector3();
        thisText.text = "";
        thisTransform.localPosition = originalPosition;
    }
}
