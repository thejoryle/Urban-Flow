using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource jump0;
    public AudioSource jump1;
    public AudioSource jump2;
    public AudioSource metalBang0;
    public AudioSource metalBang1;
    public AudioSource fall;
    public AudioSource win;

    public void PlayRandomJump()
    {
        int choice = Random.Range(0, 3);
        switch(choice)
        {
            case 0: jump0.Play(); break;
            case 1: jump1.Play(); break;
            case 2: jump2.Play(); break;
        }
    }

    public void PlayRandomMetalBang()
    {
        int choice = Random.Range(0, 2);
        switch(choice)
        {
            case 0: metalBang0.Play(); break;
            case 1: metalBang1.Play(); break;
        }
    }

    public void PlayFall()
    {
        fall.Play();
    }

    public void PlayWin()
    {
        win.Play();
    }
}
