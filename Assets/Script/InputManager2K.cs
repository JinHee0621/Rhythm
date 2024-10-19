using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager2K : MonoBehaviour
{

    KeyCode inputBtnKey1 = KeyCode.D;
    KeyCode inputBtnKey2 = KeyCode.F;

    public SpriteRenderer Effect1;
    public SpriteRenderer Effect2;

    public BoxCollider2D BtnCollider1;
    public BoxCollider2D BtnCollider2;

    public Animator Btn1Anim;
    public Animator Btn2Anim;

    //float btn1Time = 0f;
    //float btn2Time = 0f;

    void Update()
    {
        InputBtn1(Input.GetKey(inputBtnKey1));
        InputBtn2(Input.GetKey(inputBtnKey2));
    }

    public void InputBtn1(bool input)
    {
        if (input)
        {
            BtnCollider1.enabled = true;
            Btn1Anim.SetBool("ButtonPush", true);
            Effect1.color = new Color(Effect1.color.r, Effect1.color.g, Effect1.color.b, 1);
            //btn1Time += 1f;
        } else
        {
            BtnCollider1.enabled = false;
            Btn1Anim.SetBool("ButtonPush", false);
            //Debug.Log(btn1Time);
            //btn1Time = 0f;
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
            // btn2Time += 1f;
        }
        else
        {
            BtnCollider2.enabled = false;
            Btn2Anim.SetBool("ButtonPush", false);
            //Debug.Log(btn2Time);
            ///btn2Time = 0f;
            StartCoroutine(ColorDisabled(Effect2));
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

