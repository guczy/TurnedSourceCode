using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject unarmedModel;
    public GameObject glockModel;
    public GameObject ak47Model;

    void Start()
    {
        // Ensure only the default weapon is enabled at the start
        EnableWeapon("Unarmed");
    }

    public void EnableWeapon(string weaponName)
    {
        // Disable all weapon models
        unarmedModel.SetActive(false);
        glockModel.SetActive(false);
        ak47Model.SetActive(false);
        // Enable the selected weapon model
        switch (weaponName)
        {
            case "Unarmed":
                unarmedModel.SetActive(true);
                glockModel.SetActive(false);
                ak47Model.SetActive(false);

                break;
            case "Glock":
                glockModel.SetActive(true);
                unarmedModel.SetActive(false);
                ak47Model.SetActive(false);

                break;
            case "AK47":
                ak47Model.SetActive(true);
                glockModel.SetActive(false);
                unarmedModel.SetActive(false);
                break;
            default:
                Debug.LogWarning("Weapon not found: " + weaponName);
                break;
        }
    }
}
