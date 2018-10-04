using UnityEngine;
using Mirror;
using System;
using System.Linq;
using System.Collections;

// =======================================================================================
// ENTITY
// =======================================================================================
public partial class Entity {
	
	protected int 					UCE_cachedHealth;
	protected Entity 				lastAggressor;
	[SyncVar] protected string 		lastAggressorName;
	
	// ================================= FUNCTIONS =======================================
	
	// -----------------------------------------------------------------------------------
	// DealDamageAt_UCE
	// -----------------------------------------------------------------------------------
	public void DealDamageAt_UCE(Entity entity, int amount) {
		if (entity == null || amount <= 0) return;
		entity.lastAggressor = this;
		entity.lastAggressorName = this.name;
	}
	
	// -----------------------------------------------------------------------------------
	// Update_UCE
	// -----------------------------------------------------------------------------------
	void Update_UCE() {
		if (UCE_cachedHealth > health || UCE_cachedHealth == 0) {
			OnDamageDealt();
			UCE_cachedHealth = health;
		}
	}
	
	// -----------------------------------------------------------------------------------
	// OnDeath_UCE
	// ----------------------------------------------------------------------------------
	void OnDeath_UCE() {
		UCE_cachedHealth 	= 0;
		target 				= null;
	}
	
	// -----------------------------------------------------------------------------------
	// OnDamageDealt
	// -----------------------------------------------------------------------------------
	public void OnDamageDealt() {
		
		if (this is Player)
        	Utils.InvokeMany(typeof(Player), this, "OnDamageDealt_");
        
        if (this is Monster)
        	Utils.InvokeMany(typeof(Monster), this, "OnDamageDealt_");
        
        if (this is Pet)
        	Utils.InvokeMany(typeof(Pet), this, "OnDamageDealt_");
        	
	}
	
	// -----------------------------------------------------------------------------------
	
}

// =======================================================================================
