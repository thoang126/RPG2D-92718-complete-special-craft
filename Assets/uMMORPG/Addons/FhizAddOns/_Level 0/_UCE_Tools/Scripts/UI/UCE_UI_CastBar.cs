using UnityEngine;
using UnityEngine.UI;

	// ===================================================================================
	// CAST BAR UI
	// ===================================================================================
	public partial class UCE_UI_CastBar : MonoBehaviour {
		
		public GameObject panel;
		public Slider slider;
		public Text nameText;
		public Text progressText;
		
		private float duration;
		private float durationRemaining;
		
		// -----------------------------------------------------------------------------------
		// Update
		// @Client
		// -----------------------------------------------------------------------------------
		void Update() {
		
			var player = Utils.ClientLocalPlayer();
			if (!player) return;

			if (panel.activeSelf) {
			
				if (NetworkTime.time <= durationRemaining) {

					float ratio = (durationRemaining - NetworkTime.time) / duration;
					float remain = durationRemaining - NetworkTime.time;
					slider.value = ratio;
					progressText.text = remain.ToString("F1") + "s";
				
				} else {
					Hide();
				}
				
			}
			
		}
		
		// -----------------------------------------------------------------------------------
		// Show
		// @Client
		// -----------------------------------------------------------------------------------
		public void Show(string labelName, float dura) {
			
			var player = Utils.ClientLocalPlayer();
			if (!player) return;
			
			duration = dura;
			durationRemaining = NetworkTime.time + duration;
			nameText.text = labelName;
			
			panel.SetActive(true);
		}
	
		// -----------------------------------------------------------------------------------
		// Hide
		// @Client
		// -----------------------------------------------------------------------------------
		public void Hide() {
			
			var player = Utils.ClientLocalPlayer();
			if (!player) return;
			
			if (!player.UCE_isBusy())
				panel.SetActive(false);
				
		}
		
		// -----------------------------------------------------------------------------------
	
	}

// =======================================================================================