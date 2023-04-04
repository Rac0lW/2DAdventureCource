using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthBar;
    public Image redBar;

    public Image powerBar;

    private void Update() {
        if(redBar.fillAmount != healthBar.fillAmount)
            redBar.fillAmount = Mathf.Lerp(redBar.fillAmount, healthBar.fillAmount, Time.deltaTime * 5);
    }
    /// <summary>
    /// Update the health bar
    /// </summary>
    /// <param name="persent">The persent of the health</param>

    public void OnHealthChange(float persent)
    {
        healthBar.fillAmount = persent;
    }
}
