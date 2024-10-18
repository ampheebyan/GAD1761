using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIHandler : MonoBehaviour
{
    [System.Serializable]
    public class WeaponImage
    {
        public Image image;
        public string name = "weapon";
    }

    [Header("UIHandler")]
    public Animator heartAnimator;
    public TMP_Text hpText;
    public TMP_Text staminaText;

    public GameObject weaponInfo;
    public TMP_Text ammoText;
    public WeaponImage[] weaponImages;

    private void Start()
    {
        Application.targetFrameRate = Mathf.RoundToInt((float)Screen.currentResolution.refreshRateRatio.value) * 2; // Set target framerate to double monitor refresh rate, just so the target will be smooth no matter what.
    }

    public void UpdateWeaponImage()
    {
        // Iterate through weapon image list, find which one has the current weapon name, enable it and disable others.
        for (int i = 0; i < weaponImages.Length; i++)
        {
            if (weaponImages[i].name == GlobalReferences.localPlayerWeapons.currentWeapon.name) weaponImages[i].image.gameObject.SetActive(true); else weaponImages[i].image.gameObject.SetActive(false);
        }
    }

    public float CalculateNewFloat(Vector2 health)
    {
        if (health.x == 0 || health.y == 0) return 0; // Don't divide by 0 kids.
        return health.y / health.x;
    }

    void Update()
    {
        // Show/hide weaponInfo
        if (GlobalReferences.localPlayerWeapons.currentWeapon != null && GlobalReferences.localPlayerWeapons.currentWeapon.gameObject.activeSelf == true) weaponInfo.SetActive(true); else weaponInfo.SetActive(false);
        // Set values
        heartAnimator.SetFloat("LostHealth",Mathf.Clamp(CalculateNewFloat(GlobalReferences.localPlayer.GetHealth()), 0, 8));
        hpText.SetText($"{System.Math.Round(GlobalReferences.localPlayer.GetHealth().x, 2)}");
        staminaText.SetText($"{System.Math.Round(GlobalReferences.localPlayer.stamina.x, 2)}");
        ammoText.SetText($"{GlobalReferences.localPlayerWeapons.currentWeapon.ammo.x}<size=75%>/{GlobalReferences.localPlayerWeapons.currentWeapon.ammo.y}</size>");
    }


}
