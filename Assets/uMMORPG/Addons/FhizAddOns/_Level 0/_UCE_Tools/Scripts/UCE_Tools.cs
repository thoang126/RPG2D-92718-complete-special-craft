using UnityEngine;
using Mirror;
using System;
using System.Linq;
using System.Collections;

// =======================================================================================
// UCE TOOLS
// =======================================================================================
public partial class UCE_Tools {
	
	const char CONST_DELIMITER = ';';
	
	// -----------------------------------------------------------------------------------
	// IntArrayToString
	// -----------------------------------------------------------------------------------
	public static string IntArrayToString(int[] array) {
		if (array == null || array.Length == 0) return null;
		string arrayString = "";
		for (int i = 0; i < array.Length; i++) {
			arrayString += array[i].ToString();
			if (i < array.Length-1)
				arrayString += CONST_DELIMITER;
		}
		return arrayString;
	}
	
	// -----------------------------------------------------------------------------------
	// IntStringToArray
	// -----------------------------------------------------------------------------------
	public static int[] IntStringToArray(string array) {
		if (Utils.IsNullOrWhiteSpace(array)) return null;
		string[] tokens = array.Split(CONST_DELIMITER);
		int[] arrayInt = Array.ConvertAll<string, int>(tokens, int.Parse);
		return arrayInt;
	}
	
	// -----------------------------------------------------------------------------------
	// FindOnlinePlayerByName
	// -----------------------------------------------------------------------------------
	public static Player FindOnlinePlayerByName(string playerName) {
        if (!Utils.IsNullOrWhiteSpace(playerName)) {
            if (Player.onlinePlayers.ContainsKey(playerName)) {
                return Player.onlinePlayers[playerName].GetComponent<Player>();
            }     
        }       
        return null;
    }

	// -----------------------------------------------------------------------------------
	
}

// =======================================================================================
