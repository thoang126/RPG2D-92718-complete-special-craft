////////////////////////
//Specialized Crafting//
//         By:        //
//     RCTesting      //
//       A.K.A.       //
//    (KD, DirtyD)    //
////////////////////////

//////
//Specialized Crafting - version 1.1
//////
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SpecializedCrafting : MonoBehaviour {
	public GameObject panel;
	public Button craftButton;
	public UISpecializedCraftingSlot slotPrefab;
	public Transform CraftableList;
	public Transform ingredients;
	public Transform itemToCraft;

	[HideInInspector] public int buyIndex = -1;
	public List<ScriptableRecipe> canBeCrafted = new List<ScriptableRecipe>();
	void Update () {
		///
		Player player = Utils.ClientLocalPlayer();
		if (!player) return;
		UIUtils.BalancePrefabs (slotPrefab.gameObject,canBeCrafted.Count, CraftableList);

		for (int i = 0; i < canBeCrafted.Count; ++i) {
			UISpecializedCraftingSlot slot = CraftableList.GetChild(i).GetComponent<UISpecializedCraftingSlot>();
			ScriptableItem itemSlot = canBeCrafted[i].result;
			slot.recipeList.Clear();
			
			try{for (int j = 0; j < 6; ++j){
					if(canBeCrafted [i].ingredients [j] != null)
					{
					slot.recipeList.Add(canBeCrafted [i].ingredients [j]);
					}else{}
				}
			}catch{}
			
			slot.image.color = Color.white;
			slot.image.sprite = itemSlot.image;
			slot.tooltip.enabled = true;
			slot.tooltip.text = new Item(itemSlot).ToolTip();
			slot.toMake = canBeCrafted[i].result;
			slot.amountOverlay.SetActive(slot.amount > 1);
			
			slot.button.onClick.SetListener(() => {
				for (int j = 0; j < 1; ++j) {
					UIUtils.BalancePrefabs (slotPrefab.gameObject,1, itemToCraft);
				
					UISpecializedCraftingSlot slotItemToCraft = itemToCraft.GetChild(j).GetComponent<UISpecializedCraftingSlot>();
					if(itemSlot != null){
						ScriptableItem itemSlotToCraft = itemSlot;
						slotItemToCraft.image.color = Color.white;
						slotItemToCraft.image.sprite = itemSlotToCraft.image;
					}else{
        		        slotItemToCraft.enabled = false;
            		   	slotItemToCraft.image.sprite = null;
						slotItemToCraft.image.color = Color.clear;
		           	}
				}
				
				for (int k = 0; k < 6; ++k) {
					UIUtils.BalancePrefabs (slotPrefab.gameObject,6, ingredients);
					UISpecializedCraftingSlot slotIngredients = ingredients.GetChild(k).GetComponent<UISpecializedCraftingSlot>();
					
					try{
						slotIngredients.enabled = false;
            		   	slotIngredients.image.sprite = null;
						slotIngredients.image.color = Color.clear;
					if(slot.recipeList[k] != null){
						ScriptableItem itemSlotIngredients = slot.recipeList[k];
						slotIngredients.image.color = Color.white;
						slotIngredients.image.sprite = itemSlotIngredients.image;
						slotIngredients.tooltip.enabled = true;
						slotIngredients.tooltip.text = new Item(itemSlotIngredients).ToolTip();
					}else{
        		        slotIngredients.enabled = false;
            		   	slotIngredients.image.sprite = null;
						slotIngredients.image.color = Color.clear;
		           	}
					}catch{}
						
					craftButton.onClick.SetListener(() =>{
						bool playerHasIngredients = true;

						try{
							var result = slot.recipeList.GroupBy(x => x)
								.ToDictionary(y=>y.Key, y=>y.Count())
								.OrderByDescending(z => z.Value);
							
							foreach (var x in result)
								{
									if (player.InventoryCount(new Item(x.Key)) < x.Value){
										playerHasIngredients = false;
									}
								}
								
								if (playerHasIngredients == true){
									foreach (var x in result)
									{
										player.CmdSpecializedCraftingRemoveItem(new Item(x.Key), x.Value);
									}
									Item SpecializedCraftedItem = new Item(itemSlot);
									player.CmdSpecializedCraftingAddItem(SpecializedCraftedItem);
								}else{}
						}catch{}
						playerHasIngredients = true;
					});
				}
			});
		}
	}
}
