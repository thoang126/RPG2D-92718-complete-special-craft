using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Collections;
using System;

	// ===================================================================================
	// POPUP
	// ===================================================================================
	[Serializable]
	public class UCE_Popup : MonoBehaviour {
	
		
		protected UCE_UI_Popup instance;
		
		[Tooltip("[Optional] Add sounds + icons here and use their Index numbers to use them when showing a popup")]
		public Sprite[] availableIcons;
		public AudioClip[] availableSounds;
		
		protected string popupLabel;
    	protected Sprite popupIcon;
    	protected AudioClip popupSoundEffect;
    	protected Image popupBackground;
    	
 	    // -----------------------------------------------------------------------------------
    	// Awake
    	// -----------------------------------------------------------------------------------
    	void Awake() {
    		if (instance == null)
    			instance = FindObjectOfType<UCE_UI_Popup>();
    	}
    	
 	    // -----------------------------------------------------------------------------------
    	// Prepare
    	// -----------------------------------------------------------------------------------
    	public void Prepare(string message, byte iconId=0, byte soundId=0) {
    	
    		popupLabel 			= message;
    		
    		if (availableIcons.Length > 0 && availableIcons.Length >= iconId) 
    			popupIcon 			= availableIcons[iconId];
    			
    		if (availableSounds.Length > 0 && availableSounds.Length >= soundId) 
    			popupSoundEffect 	= availableSounds[soundId];
    		
    	}
    	
	    // -----------------------------------------------------------------------------------
    	// ShowPopup
    	// -----------------------------------------------------------------------------------
		public void Show() {
			
			if (instance != null) {
				
				if (popupLabel != null)
					instance.popupText.text = popupLabel;
			
				if (popupSoundEffect != null)
					instance.popupSoundEffect = popupSoundEffect;
				
				if (popupBackground != null)
					instance.popupBackground = popupBackground;
				
				if (popupIcon != null)
					instance.popupIcon.sprite = popupIcon;
				
				instance.Show();
		
			} else {
				Debug.LogWarning("UCE Popup not found!");
			}
			
		}
		
		// -----------------------------------------------------------------------------------
		
	}


// =======================================================================================
