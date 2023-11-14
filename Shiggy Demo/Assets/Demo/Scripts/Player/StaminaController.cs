using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    public double stamina;
    public int maxStamina;
   
    public Slider staminaSlider;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);

    //public GravityGun gravityGun;

    public static StaminaController instance;
    private Coroutine regen;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        stamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }
    public void UseStamina(double amount)
    {
        if (stamina - amount >= 0)
        {
            stamina -= amount;
            staminaSlider.value = (int)stamina;

            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            Debug.Log("not enough stamina");
        }
    }
    IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2f);

        while (stamina < maxStamina)
        {
            stamina += maxStamina / 100;
            staminaSlider.value = (int)stamina;
            yield return regenTick;
        }
        regen = null;
    }

    void Update()
    {
        
    }
    //public void ReduceStamina()
    //{
    //    stamina--;
    //    staminaSlider.value = stamina;
    //}

    //public void IncreaseStamina()
    //{

    //    StartCoroutine(IncreaseStaminaTimer());

    //}
    //IEnumerator IncreaseStaminaTimer()
    //{

    //    yield return new WaitForSeconds(3f);
    //    if (stamina < staminaSlider.maxValue)
    //    {
    //        stamina++;
    //        staminaSlider.value = stamina;
    //    }
       
    //}
}
