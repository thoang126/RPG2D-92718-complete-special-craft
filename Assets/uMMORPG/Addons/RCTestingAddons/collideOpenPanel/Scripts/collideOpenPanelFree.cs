////////////////////////
// Collide Open Panel //
//         By:        //
//     RCTesting      //
//       A.K.A.       //
//    (KD, DirtyD)    //
////////////////////////

//////
//Collide Open Panel - Free version 1.1
//////
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class collideOpenPanelFree : MonoBehaviour{
	Player player;
	[Header("Panel To Open")]
    public GameObject panel;
	
	private BoxCollider2D collider;

 
	 public void OnTriggerEnter2D(Collider2D other){
		  player = Utils.ClientLocalPlayer();
		  if (!player || player.name != other.name) return;
		 try{
			 panel.SetActive(true);
			 Debug.Log(panel.name + "is set");
		 }catch{
			 Debug.Log("Something went wrong! Did you set the panel to open?");
		 }
	}
	
	public void OnTriggerExit2D(Collider2D other){
		  player = Utils.ClientLocalPlayer();
		  if (!player || player.name != other.name) return;
		 try{
			 panel.SetActive(false);
			 Debug.Log(panel.name + "is unset");
		 }catch{
			 Debug.Log("Something went wrong! Did you set the panel to open?");
		 }
	}
	
	 public void OnTriggerEnter(Collider other){
		  player = Utils.ClientLocalPlayer();
		  if (!player || player.name != other.transform.root.name) return;
		 try{
			 panel.SetActive(true);
			 Debug.Log(panel.name + "is set");
		 }catch{
			 Debug.Log("Something went wrong! Did you set the panel to open?");
		 }
	}
	
	public void OnTriggerExit(Collider other){
		  player = Utils.ClientLocalPlayer();
		  if (!player || player.name != other.transform.root.name) return;
		 try{
			 panel.SetActive(false);
			 Debug.Log(panel.name + "is unset");
		 }catch{
			 Debug.Log("Something went wrong! Did you set the panel to open?");
		 }
	}
}
