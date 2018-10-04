// =======================================================================================
// POPUP - PLAYER
// =======================================================================================

using UnityEngine;
using Mirror;
using System.Collections;
using System;
using System.Linq;

// =======================================================================================
// PLAYER
// =======================================================================================
public partial class Player {
	
	protected UCE_Popup popup;
	
	// -----------------------------------------------------------------------------------
	// UCE_ShowPopup
	// @Server
	// -----------------------------------------------------------------------------------
	[ServerCallback]
	public void UCE_ShowPopup(string message, byte iconId=0, byte soundId=0) {
		Target_UCE_ShowPopup(connectionToClient, message, iconId, soundId);
	}

	// -----------------------------------------------------------------------------------
	// Target_UCE_ShowPopup
	// @Server -> @Client
	// -----------------------------------------------------------------------------------
	[TargetRpc]
    public void Target_UCE_ShowPopup(NetworkConnection target, string message, byte iconId, byte soundId) {
		
		if (popup == null)
			popup = GetComponent<UCE_Popup>();
		
		if (popup != null) {
			popup.Prepare(message, iconId, soundId);
			popup.Show();
		} else {
			Debug.LogWarning("You forgot to assing UCE_Popup component to your player prefab");
		}
		
    }
	
	// -----------------------------------------------------------------------------------
	
}

// =======================================================================================