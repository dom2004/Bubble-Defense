using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarImage;

    [SerializeField] 
    private TMP_Text _hpIndicator;

    public void UpdateHealthBar(HealthController controller)
    {
        if (controller != null)
        {
            _healthBarImage.fillAmount = controller.RemainingHealthPercentage;
            _hpIndicator.SetText($"{controller.CurrentHealth}/{controller.MaxHealth}");
        }
    }
}
