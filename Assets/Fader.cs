using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{

    public Image imageToFade;

    private static Fader Instance;

    public UnityEvent OnFadeComplete;
    public float timer;
    private float time;
    private bool To1;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        FadeTo0(3f);
    }

    public static void FadeTo(float time, int zeroOrOne)
    {
        if(zeroOrOne == 0)
            Instance.FadeTo0(time);
        else
            Instance.FadeTo1(time);
    }


    public void FadeTo1(float time)
    {
        timer = -Time.deltaTime;
        this.time = time;
        To1 = true;
    }

    public void FadeTo0(float time)
    {
        timer = -Time.deltaTime;
        this.time = time;
        To1 = false;

    }
    // Update is called once per frame
    void Update()
    {
        if(timer > time) return;
        
        var color = imageToFade.color;
        timer += Time.deltaTime;
        float proc = Mathf.Clamp01(timer / time);
        if (To1)
        {
            color.a = Mathf.Lerp(0,1,proc);
        }
        else
        {
            color.a = Mathf.Lerp(1,0,proc);
        }
        imageToFade.color = color;
        if (timer > time)
        {
            OnFadeComplete?.Invoke();
        }
    }
}
