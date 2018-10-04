using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Mirror;

	// =======================================================================================
	// UCE POPUP - CLASS
	// =======================================================================================
	[System.Serializable]
	public class UCE_PopupClass {
	
		public string message = "Default Message";
		[Range(0,255)]public byte iconId;
		[Range(0,255)]public byte soundId;
		
	}
 
	// =======================================================================================

// =======================================================================================