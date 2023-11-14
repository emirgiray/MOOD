using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text bowlingScoreText;
    public Text ticketText;

    public static int switcher1;
    public static int switcher2;
    public static int switcher3;

    public static int basketballScore;
    public static float bowlingScore = 0f;

    public static int ticketCount = 0;

    private void Start()
    {        
        switcher1 = 0;
        switcher2 = 0;
        switcher3 = 0;
    }
    private void Update()
    {
        bowlingScoreText.text = "" + bowlingScore;
        ticketText.text = ticketCount+" tickets!";
        //Debug.Log(bowlingScore);

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene("Scene 1");
        }
    }
}
