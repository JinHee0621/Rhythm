using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager4K : InputManager
{
    KeyCode inputBtnKey1 = KeyCode.D;
    KeyCode inputBtnKey2 = KeyCode.F;
    KeyCode inputBtnKey3 = KeyCode.J;
    KeyCode inputBtnKey4 = KeyCode.K;

    [Header("Effect")]
    public SpriteRenderer Effect1;
    public SpriteRenderer Effect2;
    public SpriteRenderer Effect3;
    public SpriteRenderer Effect4;

    [Header("Button")]
    public BoxCollider2D BtnCollider1;
    public BoxCollider2D BtnCollider2;
    public BoxCollider2D BtnCollider3;
    public BoxCollider2D BtnCollider4;

    public Animator Btn1Anim;
    public Animator Btn2Anim;
    public Animator Btn3Anim;
    public Animator Btn4Anim;

    private bool Btn1Hold = false;
    private bool Btn2Hold = false;
    private bool Btn3Hold = false;
    private bool Btn4Hold = false;

    void Update()
    {
        InputBtn1(Input.GetKey(inputBtnKey1));
        InputBtn2(Input.GetKey(inputBtnKey2));
        InputBtn3(Input.GetKey(inputBtnKey3));
        InputBtn4(Input.GetKey(inputBtnKey4));
    }

    public void InputBtn1(bool input)
    {
        if (input)
        {
            Btn1Anim.SetBool("ButtonPush", true);
            Effect1.color = new Color(Effect1.color.r, Effect1.color.g, Effect1.color.b, 1);
            if (!Btn1Hold)
            {
                RayEnabled(BtnCollider1);
                Btn1Hold = true;
            } else
            {
                //LongRayEnabled(BtnCollider1);
            }
        } else
        {
            Btn1Anim.SetBool("ButtonPush", false);
            if(Btn1Hold)
            {
                LongRayDisabled(BtnCollider1);
            }
            Btn1Hold = false;
        }
    }

    public void InputBtn2(bool input)
    {
        if (input)
        {
            Btn2Anim.SetBool("ButtonPush", true);
            Effect2.color = new Color(Effect2.color.r, Effect2.color.g, Effect2.color.b, 1);
            if(!Btn2Hold)
            {
                RayEnabled(BtnCollider2);
                Btn2Hold = true;
            }
        }
        else
        {
            Btn2Anim.SetBool("ButtonPush", false);
            Btn2Hold = false;
        }
    }

    public void InputBtn3(bool input)
    {
        if (input)
        {
            Btn3Anim.SetBool("ButtonPush", true);
            Effect3.color = new Color(Effect3.color.r, Effect3.color.g, Effect3.color.b, 1);
            if (!Btn3Hold)
            {
                RayEnabled(BtnCollider3);
                Btn3Hold = true;
            }
        }
        else
        {
            Btn3Anim.SetBool("ButtonPush", false);
            Btn3Hold = false;
        }
    }


    public void InputBtn4(bool input)
    {
        if (input)
        {
            Btn4Anim.SetBool("ButtonPush", true);
            Effect4.color = new Color(Effect4.color.r, Effect4.color.g, Effect4.color.b, 1);
            if (!Btn4Hold)
            {
                RayEnabled(BtnCollider4);
                Btn4Hold = true;
            }
        }
        else
        {
            Btn4Anim.SetBool("ButtonPush", false);
            Btn4Hold = false;
        }
    }

    public override void HitEffect(int BtnNum)
    {
        if(BtnNum == 1)
        {
            BtnCollider1.gameObject.GetComponent<HitPointManager>().HitEffectRay();
        }
        else if(BtnNum == 2)
        {
            BtnCollider2.gameObject.GetComponent<HitPointManager>().HitEffectRay();
        }
        else if (BtnNum == 3)
        {
            BtnCollider3.gameObject.GetComponent<HitPointManager>().HitEffectRay();
        }
        else if (BtnNum == 4)
        {
            BtnCollider4.gameObject.GetComponent<HitPointManager>().HitEffectRay();
        }
    }
}

