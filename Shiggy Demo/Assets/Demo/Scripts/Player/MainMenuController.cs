using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    public Camera camera;
    Rigidbody _r;
    public float force = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log(hit.collider.gameObject.name);
                //_r = objectHit.GetComponent<Rigidbody>();
                //_r.AddForce(Random.insideUnitCircle.normalized * force);
                if(hit.collider.gameObject.name == "StartCube")
                {
                    Debug.Log("Start Game");
                    SceneManager.LoadScene("Demo Level");
                }
                if (hit.collider.gameObject.name == "QuitCube")
                {
                    Debug.Log("Quit Game");
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #else
                    Application.Quit();
                    #endif
                }
            }            
        }
    }
}
