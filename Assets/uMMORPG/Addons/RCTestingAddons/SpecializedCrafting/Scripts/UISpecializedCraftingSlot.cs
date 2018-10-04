////////////////////////
// View Player Behind //
//     Object By:     //
//     RCTesting      //
//       A.K.A.       //
//    (KD, DirtyD)    //
////////////////////////

//////
//Specialized Crafting - version 1.0
//////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpecializedCraftingSlot : MonoBehaviour {
	public UIShowToolTip tooltip;
	public Button button;
	public UIDragAndDropable dragAndDropable;
	public Image image;
	public ScriptableItem toMake;
	public ScriptableItem itemSlotToCraft;
	public List<ScriptableItem> recipeList = new List<ScriptableItem>(6);
	public List<ScriptableItem> recipeListAmount = new List<ScriptableItem>();
	
	public List<ScriptableItem> items = new List<ScriptableItem>(6);
	public int amount;
	public GameObject amountOverlay;
	public Text amountText;
}
