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
        if (currentWave < waves.Length)
        {
            int numberOfEnemies = Mathf.Clamp(currentWave, 0, 3);

            switch (waves[currentWave])
            {
                case WaveType.Investigator:
                    {
                        StartCoroutine(investigatorManager.SpawnUnits(numberOfEnemies + 1, SpawnType.Investigator));
                        break;
                    }
                case WaveType.Swat:
                    {
                        StartCoroutine(investigatorManager.SpawnUnits(numberOfEnemies + 2, SpawnType.Swat));
                        break;
                    }
            }
        }
    }

    public void EndWave()
    {
        currentWave++;
    }
}
