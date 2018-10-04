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
using UnityEngine;
using Mirror;
using UnityEngine.AI;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class Player : Entity {

	[Command]
    public void CmdSpecializedCraftingAddItem(Item itemToAdd)
    {
		InventoryAdd(itemToAdd, 1);
    }
	
	[Command]
	public void CmdSpecializedCraftingRemoveItem(Item itemToRemove,int amount)
    {
		InventoryRemove(itemToRemove, amount);
	}
}
