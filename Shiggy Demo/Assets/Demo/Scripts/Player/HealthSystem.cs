using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HealthSystem : MonoBehaviour
{
    public float health;
    public float healthMax = 100;
    private float n_HP;
    public bool godMode = false;
    public Slider hp_Slider;
    public Text godModeText;
    public Image redFlash;
    void Start()
    {
        //Set HP to Max
        health = healthMax;
    }

    void Update()
    {
        //Current HP Update (Save, define new hp, reset dmg number)
        float p_HP = health;
        health = health + n_HP;
        n_HP = 0;
        
        hp_Slider.value = health;
        //HealthBar.fillAmount = c_HP / healthMax;

        if (health <= 0 && godMode == false)
        {
            Death();
            SceneManager.LoadScene("Death Screen");
            Cursor.lockState = CursorLockMode.Confined;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            godMode = true;
            godModeText.enabled= true;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            godMode = false;
            godModeText.enabled = false;
        }
    }
    public void Damage(float damageAmount)
    {
        n_HP -= damageAmount;
        hp_Slider.value = health;

    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        hp_Slider.value = health;
        if (health > healthMax)
        {
            health = healthMax;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && godMode == false)
        {
            Damage(10);
            StartCoroutine(ImageOnOf());
        }
    }

    public void Death()
    {
        //youDiedText.text = "You Died";
    }
    public void Restart()
    {
        SceneManager.LoadScene("Scene 1");

    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    IEnumerator ImageOnOf()
    {
        redFlash.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        redFlash.gameObject.SetActive(false);
    }


}
