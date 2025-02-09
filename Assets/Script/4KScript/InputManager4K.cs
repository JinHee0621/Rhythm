using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager4K : MonoBehaviour
{

    KeyCode inputBtnKey1 = KeyCode.D;
    KeyCode inputBtnKey2 = KeyCode.F;
    KeyCode inputBtnKey3 = KeyCode.J;
    KeyCode inputBtnKey4 = KeyCode.K;

    public SpriteRenderer Effect1;
    public SpriteRenderer Effect2;
    public SpriteRenderer Effect3;
    public SpriteRenderer Effect4;

    public BoxCollider2D BtnCollider1;
    public BoxCollider2D BtnCollider2;
    public BoxCollider2D BtnCollider3;
    public BoxCollider2D BtnCollider4;


    public Animator Btn1Anim;
    public Animator Btn2Anim;
    public Animator Btn3Anim;
    public Animator Btn4Anim;

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
            BtnCollider1.enabled = true;
            Btn1Anim.SetBool("ButtonPush", true);
            Effect1.color = new Color(Effect1.color.r, Effect1.color.g, Effect1.color.b, 1);
        } else
        {
            BtnCollider1.enabled = false;
            Btn1Anim.SetBool("ButtonPush", false);
            StartCoroutine(ColorDisabled(Effect1));
        }
    }

    public void InputBtn2(bool input)
    {
        if (input)
        {
            BtnCollider2.enabled = true;
            Btn2Anim.SetBool("ButtonPush", true);
            Effect2.color = new Color(Effect2.color.r, Effect2.color.g, Effect2.color.b, 1);
        }
        else
        {
            BtnCollider2.enabled = false;
            Btn2Anim.SetBool("ButtonPush", false);
            StartCoroutine(ColorDisabled(Effect2));
        }
    }

    public void InputBtn3(bool input)
    {
        if (input)
        {
            BtnCollider3.enabled = true;
            Btn3Anim.SetBool("ButtonPush", true);
            Effect3.color = new Color(Effect3.color.r, Effect3.color.g, Effect3.color.b, 1);
        }
        else
        {
            BtnCollider3.enabled = false;
            Btn3Anim.SetBool("ButtonPush", false);
            StartCoroutine(ColorDisabled(Effect3));
        }
    }


    public void InputBtn4(bool input)
    {
        if (input)
        {
            BtnCollider4.enabled = true;
            Btn4Anim.SetBool("ButtonPush", true);
            Effect4.color = new Color(Effect4.color.r, Effect4.color.g, Effect4.color.b, 1);
        }
        else
        {
            BtnCollider4.enabled = false;
            Btn4Anim.SetBool("ButtonPush", false);
            StartCoroutine(ColorDisabled(Effect4));
        }
    }



    IEnumerator ColorDisabled(SpriteRenderer target)
    {
        yield return new WaitForSeconds(0.25f);
        if(target.color.a > 0)
        {
            target.color = new Color(target.color.r,target.color.g, target.color.b, target.color.a - 0.05f );
            StartCoroutine(ColorDisabled(target));
        } else
        {
            target.color = new Color(target.color.r, target.color.g, target.color.b, 0);
        }
    }
}

