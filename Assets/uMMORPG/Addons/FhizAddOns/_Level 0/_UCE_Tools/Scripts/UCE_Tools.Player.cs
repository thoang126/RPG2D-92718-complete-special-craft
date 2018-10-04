using UnityEngine;
using Mirror;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// =======================================================================================
// PLAYER
// =======================================================================================
public partial class Player {
	
	protected float 			UCE_timer;
	protected bool				UCE_timerRunning;
	protected int 				UCE_activeTasks;
	protected UCE_InfoBox 		UCE_infobox;
	protected UCE_UI_CastBar 	UCE_castbar;
	protected UIPopup			UCE_popup;
	
	// ================================== COMMON UI ======================================
	
	// -----------------------------------------------------------------------------------
	// UCE_PopupShow
	// -----------------------------------------------------------------------------------
	public void UCE_PopupShow(string message) {
		if (message == "") return;
		if (UCE_popup == null)
			UCE_popup = FindObjectOfType<UIPopup>();
		UCE_popup.Show(message);
	}
	
	// -----------------------------------------------------------------------------------
	// UCE_CastbarShow
	// -----------------------------------------------------------------------------------
	public void UCE_CastbarShow(string message, float duration) {
		if (duration <= 0) return;
		if (UCE_castbar == null)
			UCE_castbar = FindObjectOfType<UCE_UI_CastBar>();
		UCE_castbar.Show(message, duration);
	}
	
	// -----------------------------------------------------------------------------------
	// UCE_CastbarHide
	// -----------------------------------------------------------------------------------
	public void UCE_CastbarHide() {
		if (UCE_castbar == null)
			UCE_castbar = FindObjectOfType<UCE_UI_CastBar>();
		UCE_castbar.Hide();
	}
	
	// -----------------------------------------------------------------------------------
	// UCE_TargetAddMessage
	// -----------------------------------------------------------------------------------
	public void UCE_TargetAddMessage(string message, byte color=0) {
		if (message == "") return;
		if (UCE_infobox == null)
			UCE_infobox = GetComponent<UCE_InfoBox>();
		if (UCE_infobox) {
			UCE_infobox.TargetAddMessage(connectionToClient, message, color);
		} else {
			Debug.LogWarning("You forgot to assing UCE_InfoBox component to your player prefab");
		}
	}
	
    // ================================= MISC FUNCS ======================================
    
	// -----------------------------------------------------------------------------------
	// UCE_getSkillLevel
	// -----------------------------------------------------------------------------------
	public int UCE_getSkillLevel(ScriptableSkill skill) {
		return skills.FirstOrDefault(s => s.name == skill.name).level;
	}
	
	// -----------------------------------------------------------------------------------
	// UCE_checkHasSkill
	// -----------------------------------------------------------------------------------
	public bool UCE_checkHasSkill(ScriptableSkill skill, int level) {
		if (skill == null || level <= 0) return true;
		return skills.Any(s => s.name == skill.name && s.level >= level );
	}
	
	// -----------------------------------------------------------------------------------
	// UCE_checkHasEquipment
	// -----------------------------------------------------------------------------------
    public bool UCE_checkHasEquipment(ScriptableItem item) {
        if (item == null) return true;
        
        foreach (ItemSlot slot in equipment)
        	if (slot.amount > 0 && slot.item.data == item) return true;
        
        return false;
    }
    
 	// -----------------------------------------------------------------------------------
	// UCE_getBuffCount
	// -----------------------------------------------------------------------------------
    public int UCE_getBuffCount() {
    	int count = 0;
    	for (int i = 0; i < buffs.Count; ++i)
                if (buffs[i].BuffTimeRemaining() > 0 && buffs[i].data != offenderBuff && buffs[i].data != murdererBuff)
                    count++;
    	return count;
    }
    
	// -----------------------------------------------------------------------------------
	// UCE_CleanupBuffs
	// -----------------------------------------------------------------------------------
    public void UCE_CleanupBuffs() {
        for (int i = 0; i < buffs.Count; ++i) {
            if (buffs[i].data != offenderBuff && buffs[i].data != murdererBuff)  {
                buffs.RemoveAt(i);
                --i;
            }
        }
    }
	
	// ==================================== TIMER ========================================
	
	// -----------------------------------------------------------------------------------
	// UCE_setTimer
	// -----------------------------------------------------------------------------------
	public void UCE_setTimer(float duration) {
		if (duration > 0) {
			UCE_timer = NetworkTime.time + duration;
			UCE_timerRunning = true;
		}
	}
	
	// -----------------------------------------------------------------------------------
	// UCE_checkTimer
	// -----------------------------------------------------------------------------------
	public bool UCE_checkTimer() {
		if (UCE_timerRunning && NetworkTime.time > UCE_timer)
			return true;
		return false;
	}
	
	// -----------------------------------------------------------------------------------
	// UCE_stopTimer
	// -----------------------------------------------------------------------------------
	public void UCE_stopTimer() {
		UCE_timer = 0;
		UCE_timerRunning = false;
	}
	
	// ==================================== TASKS ========================================
	
	// -----------------------------------------------------------------------------------
	// UCE_isBusy
	// -----------------------------------------------------------------------------------
	public bool UCE_isBusy() {
		return UCE_activeTasks > 0;
	}
	
	// -----------------------------------------------------------------------------------
	// UCE_addTask
	// -----------------------------------------------------------------------------------
	public void UCE_addTask() {
		UCE_activeTasks++;
	}
	
	// -----------------------------------------------------------------------------------
	// UCE_removeTask
	// @Client
	// -----------------------------------------------------------------------------------
	public void UCE_removeTask() {
		if (UCE_activeTasks > 0)
			UCE_activeTasks--;
		if (UCE_activeTasks < 1) {
			UCE_timer = 0;
			if (UCE_castbar == null)
				UCE_castbar = FindObjectOfType<UCE_UI_CastBar>();
			if (UCE_castbar != null)
				UCE_castbar.Hide();
		}
	}
	
	// ==================================== EVENTS =======================================
	
	// -----------------------------------------------------------------------------------
	// OnDeath_UCE
	// ----------------------------------------------------------------------------------
	void OnDeath_UCE() {
		UCE_cachedHealth = 0;
		target = null;
	}
	
	// ================================== SPAWNING =======================================
	
	// -----------------------------------------------------------------------------------
	// UCE_getSpawnDestination
	// -----------------------------------------------------------------------------------
	public Vector3 UCE_getSpawnDestination {
       	get {
       	
			Vector3 spawnPosition = Vector3.zero;
		
			Bounds bounds = collider.bounds;
			spawnPosition = transform.position + transform.forward * (bounds.size.x + 2f);
			
			bool pass = false;
			int i = 0;
			
			while (!pass) {
				
				i++;
			
				UnityEngine.AI.NavMeshHit hit;
		
				if (UnityEngine.AI.NavMesh.SamplePosition(spawnPosition, out hit, 1f, UnityEngine.AI.NavMesh.AllAreas)) {
					spawnPosition = hit.position;
				}
		
				if (!Physics.Raycast(spawnPosition, Vector3.down, 0)) {
					return spawnPosition;
				}
			
				if (i > 100) {
					break; 			//emergency break in case of nothing found after 100 passes
				}
			
			}
		
			return spawnPosition;
		
        }
    }
    
    // -----------------------------------------------------------------------------------
	// UCE_checkSpawnDestination
	// -----------------------------------------------------------------------------------
	public bool UCE_checkSpawnDestination(LayerMask doNotSpawnAt) {
       	
		Vector3 spawnPosition = Vector3.zero;
		Bounds bounds = collider.bounds;
		spawnPosition = transform.position + transform.forward * (bounds.size.x + 2f);
		
		int i = 0;
		bool pass = false;
		
		while (!pass) {
			
			i++;
		
			UnityEngine.AI.NavMeshHit hit;
	
			if (UnityEngine.AI.NavMesh.SamplePosition(spawnPosition, out hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
				spawnPosition = hit.position;
	
			if (!Physics.Raycast(spawnPosition, Vector3.down, 0, doNotSpawnAt))
				return true;
		
			if (i > 100)
				return false;
		
		}
	
		return false;
		
    }
    
	// -----------------------------------------------------------------------------------
	
}

// =======================================================================================
