using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager :Singleton<UIManager> {

    public bool inObject;
    public bool useable;
    public Image Echo;
    public Image Foxtrot;

    void Start() {

    }

    void Update() {
        if (inObject)
        {
            Echo.GetComponent<Image>().enabled = true;
        }
        else
        {
            Echo.GetComponent<Image>().enabled = false;
        }
        if (useable)
        {
            Foxtrot.GetComponent<Image>().enabled = true;
        }
        else
        {
            Foxtrot.GetComponent<Image>().enabled = false;
        }
    }










        /*
        GameObject echo;
        GameObject foxtrot;
        TrapManager[] traps;
        bool yFoxtrot;
        bool yEcho;

        // Use this for initialization
        void Start () {
            //echo = GetComponentInChildren<>().enabled;
            //foxtrot = GetComponentInChildren<>(Foxt).enabled;
            foreach (TrapManager trap in traps)
            {
                traps[i] = GameObject.GetComponent<TrapManager>();
            }
        }

        // Update is called once per frame
        void Update () {
            yFoxtrot = false;
            yEcho = false;
            foreach (TrapManager trap in traps)
            {
                if (trap.inObject && trap.useable)
                {
                    echo.enabled = true;
                    foxtrot.enabled = true;
                    yFoxtrot = true;
                    yEcho = true;
                }
                else if (trap.inObject)
                {
                    echo.enabled = true;
                    foxtrot.enabled = false;
                    yEcho = true;
                }
                else if (!yEcho && !yFoxtrot)
                {
                    echo.enabled = false;
                    foxtrot.enabled = false;
                }
            }
        } */
    }
