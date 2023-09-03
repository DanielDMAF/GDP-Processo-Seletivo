using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bulletCountScript : MonoBehaviour
{
    public TextMeshProUGUI bulletText;

    private void Update()
    {
        UpdateBulletUI(playerMovimento.ammo);
    }
    public void UpdateBulletUI(int ammo)
    {
        bulletText.text = ammo.ToString();
    }
}
