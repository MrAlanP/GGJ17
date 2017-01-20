using UnityEngine;
using System.Collections;

public class WaveManager : Singleton<WaveManager>
{

    public enum WaveType
    {
        Investigator,
        Swat
    }

    public InvestigatorManager investigatorManager;
    public SwatManager swatManager;
    public WaveType[] waves;


    private int currentWave = 0;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartWave()
    {
        switch (waves[currentWave])
        {
            case WaveType.Investigator:
                {
                    investigatorManager.StartWave();
                    break;
                }
            case WaveType.Swat:
                {
                    swatManager.StartWave();
                    break;
                }
        }

    }

    public void EndWave()
    {
        currentWave++;

    }
}
