using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OptionMenuManager : MonoBehaviour
{
    public GameObject menuSet;
    public GameObject menuCursor;
    public GameObject[] menuPage;

    public int cursorPointer;
    public int cursorDepth;

    private float cursorXpos = 0f;
    private float[] cursor1MovePoint = { 85f, -40f, -165f };
    private float[] cursor2MovePoint = { 150f, 37f, -70f, -192f, -285f};
    private float[] cursor3MovePoint = { };

    public bool currentOption;
    public bool currentInGame;

    void Start()
    {
        cursorPointer = 0;
        cursorDepth = 0;
        currentOption = OptionManager.instance.currentOption;
    }

    private void Update()
    {
        /*
        KeyCode keyCode = DetectPressedKeyCode();
        if (keyCode != KeyCode.None)
        {
            Debug.Log(keyCode);
        }
        */
        if (currentOption)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                currentOption = false;
                OptionManager.instance.currentOption = currentOption;
                ShowFirstOptionMenu(currentOption);
                cursorPointer = 0;
                cursorDepth = 0;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) == true)
            {
                MoveOptionCursor(cursorDepth, cursorPointer, 0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) == true)
            {
                MoveOptionCursor(cursorDepth, cursorPointer, 1);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log(cursorDepth + " : " + cursorPointer);
                SelectOption(cursorDepth, cursorPointer);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                currentOption = true;
                OptionManager.instance.currentOption = currentOption;
                ShowFirstOptionMenu(currentOption);
                MoveOptionCursor(0, 0, 0);
            }
        }
    }

    public void ShowFirstOptionMenu(bool enable)
    {
        menuSet.SetActive(enable);
        menuPage[0].SetActive(enable);
        menuPage[1].SetActive(false);
    }

    public void MoveOptionCursor(int depth, int index, int updown)
    {
        int maxIndex = 0;
        float[] targetPosArr = { };
        if (depth == 0)
        {
            cursorXpos = -150f;
            maxIndex = 2;
            targetPosArr = cursor1MovePoint;
        }
        else if (depth == 1)
        {
            cursorXpos = -330f;
            maxIndex = 4;
            targetPosArr = cursor2MovePoint;
        }

        // 0 : up, 1 : down
        if (updown == 0)
        {
            if (cursorPointer > 0)
            {
                cursorPointer -= 1;
            }
            else
            {
                cursorPointer = 0;
            }

            if (depth == 1 && cursorPointer <= 2)
            {
                cursorXpos = -330f;
            }
            else if (depth == 1 && cursorPointer > 2)
            {
                cursorXpos = -150f;
            }
        }
        else
        {
            if (cursorPointer < maxIndex)
            {
                cursorPointer += 1;
            }
            else
            {
                cursorPointer = maxIndex;
            }

            if (depth == 1 && cursorPointer > 2)
            {
                cursorXpos = -150f;
            }
        }

        float nextPos = targetPosArr[cursorPointer];
        menuCursor.transform.DOLocalMove(new Vector3(cursorXpos, nextPos), 0.25f).SetEase(Ease.OutBack);
    }

    public void SelectOption(int depth, int pointer)
    {
        if(depth == 0)
        {
            //go to setting
            if(pointer == 0)
            {
                menuPage[depth].SetActive(false);
                depth += 1;
                cursorDepth = depth;
                cursorPointer = 0;
                menuPage[depth].SetActive(true);
                MoveOptionCursor(cursorDepth, cursorPointer, 0);
            }
        } else if(depth == 1)
        {
            //go to back
            if(pointer == 4)
            {
                menuPage[depth].SetActive(false);
                depth -= 1;
                cursorDepth = depth;
                cursorPointer = 0;
                menuPage[depth].SetActive(true);
                MoveOptionCursor(cursorDepth, cursorPointer, 0);
            }
        }
    }
}
