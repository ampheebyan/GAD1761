using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferenceSetup : MonoBehaviour
{
    public MovementHandler movementHandler;
    public InteractionHandler interactionHandler;
    public ExtPlayer extendedPlayer;
    public PlayerWeaponHandler weaponHandler;

    public void Awake() {
        // Don't continue if they're broken.
        if(movementHandler == null || interactionHandler == null || weaponHandler == null || extendedPlayer == null) throw new System.Exception("Something is broken here."); 
        // Set global references.
        GlobalReferences.movementHandler = movementHandler;
        GlobalReferences.interactionHandler = interactionHandler;
        GlobalReferences.localPlayer = extendedPlayer;
        GlobalReferences.localPlayerWeapons = weaponHandler;
    }
}
