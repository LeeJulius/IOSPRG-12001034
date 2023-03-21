
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    private float currentHp;
    [SerializeField] private float hp;
    [SerializeField] private Image healthbar;

    private void Start()
    {
        currentHp = hp;
        healthbar.fillAmount = currentHp / hp;
    }

    public void TakeDamage(int dmg)
    {
        currentHp -= dmg;
        healthbar.fillAmount = currentHp / hp;

        if (currentHp <= 0)
        {
            currentHp = 0;
            this.GetComponent<UnitController>().OnDeath.Invoke();
        }
    }
}
