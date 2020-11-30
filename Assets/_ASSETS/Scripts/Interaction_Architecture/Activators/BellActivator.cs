using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellActivator : Activator
{
    private Coroutine _coroutine;
    [SerializeField] private float _delay;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(StartActions());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SongInput(SongData data)
    {
        //throw new System.NotImplementedException();
    }

    public override void ShowHint()
    {
        //throw new System.NotImplementedException();
    }

    private IEnumerator StartActions()
    {
        yield return new WaitForSeconds(_delay);
        foreach (var action in actions)
        {
            action.Activate();
        }
    }
}
