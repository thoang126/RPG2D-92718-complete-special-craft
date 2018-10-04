using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

// =======================================================================================
// InfoBox
// =======================================================================================
public partial class UCE_InfoBox : NetworkBehaviour {

	
	protected UCE_UI_InfoBox instance;
	
	// -----------------------------------------------------------------------------------
	// TargetAddMessage
	// @Server -> @Client
	// -----------------------------------------------------------------------------------
	[TargetRpc]
	public void TargetAddMessage(NetworkConnection target, string message, byte color) {
		if (target != null || message != null)
			AddMessage(message, color);
	}

	// -----------------------------------------------------------------------------------
	// AddMessage
	// @Client
	// -----------------------------------------------------------------------------------
	[Client]
	public void AddMessage(string message, byte color) {
		if (instance == null)
			instance = FindObjectOfType<UCE_UI_InfoBox>();
		instance.AddMsg(new InfoText(message, color));
	}

}

// =======================================================================================
// InfoText
// =======================================================================================
[System.Serializable]
public class InfoText {

	public string content;
	public byte color;
	
	public InfoText(string _info, byte _color) {
		content = _info;
		color = _color;
	}
}

// =======================================================================================