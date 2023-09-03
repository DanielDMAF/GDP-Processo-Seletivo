using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoVoa : Inimigo
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(Mathf.Abs(alvoDistance) < attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, alvo.transform.position, vel_opp * Time.deltaTime);
        }
    }
}
