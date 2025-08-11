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
    private float[] cursor2MovePoint = { };
    private float[] cursor3MovePoint = { };

    public bool currentOption;
    public bool currentInGame;

    void Start()
    {
        cursorPointer = 0;
        cursorDepth = 0;
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
                ShowOptionMenu(currentOption);
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

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                currentOption = true;
                ShowOptionMenu(currentOption);
            }
        }
    }

    public void ShowOptionMenu(bool enable)
    {
        menuSet.SetActive(enable);
        menuPage[0].SetActive(enable);
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
                if (depth == 1 && cursorPointer <= 2)
                {
                    cursorXpos = -330f;
                }
            }
            else
            {
                cursorPointer = 0;
            }
        }
        else
        {
            if (cursorPointer < maxIndex)
            {
                cursorPointer += 1;
                if (depth == 1 && cursorPointer > 2)
                {
                    cursorXpos = -150f;
                }
            }
            else
            {
                cursorPointer = maxIndex;
            }
        }

        float nextPos = targetPosArr[index];
        menuCursor.transform.DOLocalMove(new Vector3(cursorXpos, nextPos), 0.25f).SetEase(Ease.OutBack);
    }

    public void SelectOption(int depth, int index)
    {

    }
}
