using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSetBool : MonoBehaviour
{
    // Helper class to set bool in animator
    public Animator animator;
    public string boolName;

    public void SetBool(bool value)
    {
        animator.SetBool(boolName, value);
    }
}
