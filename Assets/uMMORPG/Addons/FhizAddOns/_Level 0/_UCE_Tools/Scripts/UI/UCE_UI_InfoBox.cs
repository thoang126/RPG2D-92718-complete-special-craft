using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

// =======================================================================================
// UCE UI INFO BOX
// =======================================================================================
public partial class UCE_UI_InfoBox : MonoBehaviour {

	public KeyCode hotKey = KeyCode.B;
	public GameObject panel;
	public Transform content;
	public ScrollRect scrollRect;
	public GameObject textPrefab;
	[Range(0,300)]public int keepHistory = 100;
	[Range(0,30)]public float displayTime = 3f;
	public Color[] textColors;
	public string messagePrefix;
	
	// -----------------------------------------------------------------------------------
	// Update
	// @Client
	// -----------------------------------------------------------------------------------
	void Update() {
	
		var player = Utils.ClientLocalPlayer();
		if (!player) panel.SetActive(false);
		if (!player) return;
	
		if (Input.GetKeyDown(hotKey) && !UIUtils.AnyInputActive())
			panel.SetActive(!panel.activeSelf);
	
	}

	// -----------------------------------------------------------------------------------
	// AutoScroll
	// @Client
	// -----------------------------------------------------------------------------------
	void AutoScroll() {
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalNormalizedPosition = 0;
	}

	// -----------------------------------------------------------------------------------
	// AddMessage
	// @Client
	// -----------------------------------------------------------------------------------
	public void AddMsg(InfoText info) {
		
		if (content.childCount >= keepHistory)
			Destroy(content.GetChild(0).gameObject);

		var go = Instantiate(textPrefab);
		go.transform.SetParent(content.transform, false);
		go.GetComponent<Text>().text 	= messagePrefix + info.content;
		go.GetComponent<Text>().color 	= textColors[info.color];

		AutoScroll();
		
		LeanTween.alpha(panel.GetComponent<RectTransform>(), 1.0f, 0f).setEase(LeanTweenType.easeInCirc);
		panel.SetActive(true);
		Invoke("FadeHide", displayTime);
		
	}
	
	// -----------------------------------------------------------------------------------
	// FadeHide
	// @Client
	// -----------------------------------------------------------------------------------
	private void FadeHide() {
		LeanTween.alpha(panel.GetComponent<RectTransform>(), 0f, displayTime/4).setEase(LeanTweenType.easeInCirc);
		Invoke("Hide", displayTime/4 + 0.25f);
	}
	
	// -----------------------------------------------------------------------------------
	// Hide
	// @Client
	// -----------------------------------------------------------------------------------
	public void Hide() {
		panel.SetActive(false);
		LeanTween.alpha(panel.GetComponent<RectTransform>(), 1.0f, 0f).setEase(LeanTweenType.easeInCirc);
	}
	
}

// =======================================================================================