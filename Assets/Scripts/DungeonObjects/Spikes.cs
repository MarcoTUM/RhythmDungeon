using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Enemy {

    bool _up;
    public int DownTime, UpTime, Offset;
    public Sprite spikeDown, spikeUp;
    private SpriteRenderer renderer;
    private int counter = 0;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        _up = false;
        renderer.sprite = spikeDown;


        for (int i = 0; i < Offset; i++)
        {
            action();
        }
    }

    public override void doReset()
    {
        StopAllCoroutines();
        _up = false;
        renderer.sprite = spikeDown;
        counter = 0;
        for (int i = 0; i < Offset; i++)
        {
            action();
        }
    }
    public override void action()
    {
        counter++;
        if(counter == (_up ? UpTime : DownTime))
        {
            _up = !_up;
            renderer.sprite = _up ? spikeUp : spikeDown;
            counter = 0;
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag.Equals("Player") && col.GetComponent<PlayerBehaviour>().getStanding())
            if (_up)
                col.GetComponent<PlayerBehaviour>().TakeDamage(1);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.Equals("Player") && col.GetComponent<PlayerBehaviour>().getStanding())
            if (_up)
                col.GetComponent<PlayerBehaviour>().TakeDamage(1);
    }
}
