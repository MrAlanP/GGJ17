using UnityEngine;
using System.Collections;

public class Enemy_Investigator : Enemy {

    public Animator animator;

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        ScareState();
    }

    public override void ScareState()
    {
        base.ScareState();
        switch (scareCount)
        {
            case 1:
                animator.SetBool("sad", true);
                Debug.Log("i'm a sad panda");
                // investagtor scared enemy images
                // stuff
                break;

            case 2:
                // investagtor override set alertness value to 0
                // OnFinishExploring();   (?)
                break;
        }
    }
    public override void OnTrapDeath()
    {
        base.OnTrapDeath();
        animator.SetBool("alive", false);
    }
}
