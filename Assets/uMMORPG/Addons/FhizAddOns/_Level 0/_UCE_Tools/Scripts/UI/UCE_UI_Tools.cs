using UnityEngine;
using Mirror;
using System;
using System.Linq;
using System.Collections;

// =======================================================================================
// UCE UI TOOLS
// =======================================================================================
public static partial class UCE_UI_Tools {

	static UCE_UI_CanvasOverlay canvasOverlayInstance;
	
	// -----------------------------------------------------------------------------------
	// FadeScreen
	// @Client
	// -----------------------------------------------------------------------------------
	public static void FadeScreen() {
        
        Player player = Utils.ClientLocalPlayer();
		if (!player) return;
		
		if (canvasOverlayInstance == null)
			canvasOverlayInstance = GameObject.FindObjectOfType<UCE_UI_CanvasOverlay>();
		
		if (canvasOverlayInstance != null)
			canvasOverlayInstance.Fade();
		else
			Debug.LogWarning("You forgot to add UCE_UI_CanvasOverlay to your canvas!");
		
    }
    
	// -----------------------------------------------------------------------------------
	
}

// =======================================================================================
