using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    // Base class for all player types.
    [Header("BaseCharacter Properties")]
    public bool damaging = false;
    public BaseCharacter currentlyDamaging;
}
