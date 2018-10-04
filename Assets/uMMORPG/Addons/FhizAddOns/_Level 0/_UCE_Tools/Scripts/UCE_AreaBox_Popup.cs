using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Collections;

	// ===================================================================================
	// POPUP AREA - BOX
	// ===================================================================================
	[RequireComponent(typeof(BoxCollider2D))]
	public class UCE_AreaBox_Popup : NetworkBehaviour {

#if _FHIZPVP
		[Tooltip("Show the messages only to members or allies of this realm")]
		public int realmId;
		public int alliedRealmId;
#endif
   		public string messageOnEnter;
    	public string messageOnExit;
		[Range(0,255)]public byte iconId;
		[Range(0,255)]public byte soundId;
		
	    // -----------------------------------------------------------------------------------
    	// OnTriggerEnter
    	// @Client
    	// -----------------------------------------------------------------------------------
		void OnTriggerEnter2D(Collider2D co)
    {

        if (messageOnExit != "") {
				Player player = co.GetComponentInParent<Player>();
				if (player) {
#if _FHIZPVP
					if (player.UCE_getAlliedRealms(realmId, alliedRealmId)) {
#endif
						player.UCE_ShowPopup(messageOnEnter, iconId, soundId);
#if _FHIZPVP
					}
#endif
				}
			}

		}
		
		// -----------------------------------------------------------------------------------
    	// OnTriggerExit
    	// @Client
    	// -----------------------------------------------------------------------------------
		void OnTriggerExit2D(Collider2D co) {

			if (messageOnExit != "") {
				Player player = co.GetComponentInParent<Player>();
				if (player) {
#if _FHIZPVP
					if (player.UCE_getAlliedRealms(realmId, alliedRealmId)) {
#endif
						player.UCE_ShowPopup(messageOnExit, iconId, soundId);
#if _FHIZPVP
					}
#endif
				}
			}

		}
		
		// -----------------------------------------------------------------------------------
		
	}

// =======================================================================================
