using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public enum StatsType
    {
        health,
        stamina,
        stun
    };

    public struct Stats
    {
        public float maxHealth;
        public float health;
        public float maxStamina;
        public float stamina;
        public float maxStun;
        public float stun;
        public float staminaRegenSpeed;
        public float stunDecaySpeed;
    }
    public Stats stats;

    Slider healthSlider;
    Slider staminaSlider;
    Slider stunSlider;

    private void Start()
    {
        GameObject statsBar = transform.Find("Stats Canvas").GetChild(0).gameObject;
        healthSlider = statsBar.transform.GetChild(0).GetComponent<Slider>();
        staminaSlider = statsBar.transform.GetChild(1).GetComponent<Slider>();
        stunSlider = statsBar.transform.GetChild(2).GetComponent<Slider>();

        // Read Character's Stats ------ Could use scriptable object
        stats.maxHealth = 100f;
        stats.health = 80f;
        stats.maxStamina = 100f;
        stats.stamina = 80f;
        stats.maxStun = 100f;
        stats.stun = 0f;
        stats.staminaRegenSpeed = 10f;
        stats.stunDecaySpeed = 10f;
        staminaRegenerate = true;
        stunDecay = true;

        // Assign slider max value
        healthSlider.maxValue = stats.maxHealth;
        staminaSlider.maxValue = stats.maxStamina;
        stunSlider.maxValue = stats.maxStun;

        // Assign slider current value
        healthSlider.value = stats.health;
        staminaSlider.value = stats.stamina;
        stunSlider.value = stats.stun;
    }

    private void Update()
    {
        // Regeneration
        StaminaRegenerate();
        StunDecay();
    }

    public void IncreaseStat(StatsType type, float amount)
    {
        if (type == StatsType.health)
        {
            stats.health += amount;
            if (stats.health > stats.maxHealth)
            {
                stats.health = stats.maxHealth;
            }
            healthSlider.value = stats.health;
        }
        else if (type == StatsType.stamina)
        {
            stats.stamina += amount;
            if (stats.stamina > stats.maxStamina)
            {
                stats.stamina = stats.maxStamina;
            }
            staminaSlider.value = stats.stamina;
        }
        else if (type == StatsType.stun)
        {
            stats.stun += amount;
            if (stats.stun > stats.maxStun)
            {
                stats.stun = stats.maxStun;
            }
            stunSlider.value = stats.stun;

            StunDecayInterupt();
        }
    }

    public void DecreaseStat(StatsType type, float amount)
    {
        if (type == StatsType.health)
        {
            stats.health -= amount;
            if (stats.health < 0f)
            {
                stats.health = 0f;
            }
            healthSlider.value = stats.health;
        }
        else if (type == StatsType.stamina)
        {
            stats.stamina -= amount;
            if (stats.stamina < 0f)
            {
                stats.stamina = 0f;
            }
            staminaSlider.value = stats.stamina;

            StaminaRegenInterupt();
        }
        else if (type == StatsType.stun)
        {
            stats.stun -= amount;
            if (stats.stun < 0f)
            {
                stats.stun = 0f;
            }
            stunSlider.value = stats.stun;
        }
    }


    #region Stamina Regeneration

    bool staminaRegenerate;
    void StaminaRegenerate()
    {
        if (stats.stamina <= stats.maxStamina && staminaRegenerate)
        {
            stats.stamina += stats.staminaRegenSpeed * Time.deltaTime;
            staminaSlider.value = stats.stamina;
            //Debug.Log("REGENERATE");
        }
    }

    Coroutine staminaRegen;
    void StaminaRegenInterupt()
    {
        if (staminaRegen != null)
        {
            StopCoroutine(staminaRegen);
        }
        staminaRegen = StartCoroutine(StaminaRegenTimer());
    }
    IEnumerator StaminaRegenTimer()
    {
        staminaRegenerate = false;
        yield return new WaitForSeconds(1f);
        staminaRegenerate = true;
    }

    #endregion

    #region Stun Decay

    bool stunDecay;
    void StunDecay()
    {
        if (stats.stun >= 0 && stunDecay)
        {
            stats.stun -= stats.stunDecaySpeed * Time.deltaTime;
            stunSlider.value = stats.stun;
        }
    }

    Coroutine stunDecayer;
    void StunDecayInterupt()
    {
        if (stunDecayer != null)
        {
            StopCoroutine(stunDecayer);
        }
        stunDecayer = StartCoroutine(StunDecayTimer());
    }
    IEnumerator StunDecayTimer()
    {
        stunDecay = false;
        yield return new WaitForSeconds(5);
        stunDecay = true;
    }

    #endregion
}
