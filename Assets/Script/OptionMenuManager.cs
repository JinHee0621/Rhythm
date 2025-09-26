using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OptionMenuManager : MonoBehaviour
{
    public GameObject menuSet;
    public GameObject menuCursor;
    public GameObject[] menuPage;

    public int cursorPointer;
    public int cursorDepth;

    private float cursorXpos = 0f;
    private float cursorYpos = 0f;
    private float[] cursor1MovePoint = { 85f, -40f, -165f };
    private float[] cursor2MovePoint = { 150f, 37f, -70f, -192f, -285f};
    private float[] cursor3MovePoint = { -270f, -90f, 90f, 270f};

    public bool currentOption;
    public bool currentInGame;

    [Header("VolumeSet")]
    public Text BGMText;
    public Text SFXText;
    public Image BGMUp;
    public Image BGMDown;
    public Image SFXUp;
    public Image SFXDown;
    private bool soundSettingEnabled = false;
    private int soundSettingCase; //0 : BGM , 1 : SFX


    [Header("KeyText")]
    public GameObject[] ButtonTextArr;
    private bool keySettingEnabled = false;

    private GameObject buttonImg;
    private Text buttonText;
    private int keyIndex;


    void Start()
    {
        cursorPointer = 0;
        cursorDepth = 0;
        currentOption = OptionManager.instance.currentOption;
        InitBGMText();
        InitSFXText();
    }

    private void Update()
    {
        if (currentOption)
        {
            if(!keySettingEnabled && !soundSettingEnabled)
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
                else if (Input.GetKeyDown(KeyCode.RightArrow) == true)
                {
                    MoveOptionCursor(cursorDepth, cursorPointer, 2);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
                {
                    MoveOptionCursor(cursorDepth, cursorPointer, 3);
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SelectOption(cursorDepth, cursorPointer);
                }
            }
            else if(keySettingEnabled && !soundSettingEnabled)
            {
                KeyCode keyCode = DetectPressedKeyCode();
                if (keyCode != KeyCode.None && keyCode != KeyCode.Return)
                {
                    buttonText.text = keyCode.ToString();
                    buttonImg.GetComponent<Outline>().enabled = false;
                    keySettingEnabled = false;
                    Change4K(keyIndex, keyCode);
                } else if(keyCode == KeyCode.Return)
                {
                    buttonImg.GetComponent<Outline>().enabled = false;
                    keySettingEnabled = false;
                }
            }
            else if(!keySettingEnabled && soundSettingEnabled)
            {
                //Sound Setting
                if (Input.GetKeyDown(KeyCode.UpArrow) == true)
                {
                    //Sound Up
                    if(soundSettingCase == 0)
                    {
                        OptionManager.instance.ChangeBGMVolume(true);
                        InitBGMText();
                    } else if(soundSettingCase == 1)
                    {
                        OptionManager.instance.ChangeSFXVolume(true);
                        InitSFXText();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) == true)
                {
                    //Sound Down
                    if (soundSettingCase == 0)
                    {
                        OptionManager.instance.ChangeBGMVolume(false);
                        InitBGMText();
                    }
                    else if (soundSettingCase == 1)
                    {
                        OptionManager.instance.ChangeSFXVolume(false);
                        InitSFXText();
                    }
                }

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
                {
                    //Out Sound Setting
                    BGMText.color = new Color(138 / 255f, 138 / 255f, 138 / 255f);
                    SFXText.color = new Color(138 / 255f, 138 / 255f, 138 / 255f);
                    soundSettingEnabled = false;
                }
            }
        }
        else
        {
            if(!keySettingEnabled && !soundSettingEnabled)
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
    }

    public void ShowFirstOptionMenu(bool enable)
    {
        menuSet.SetActive(enable);
        menuPage[0].SetActive(enable);
        menuPage[1].SetActive(false);
        menuPage[2].SetActive(false);
    }

    public void MoveOptionCursor(int depth, int index, int updown)
    {
        int maxIndex = 0;
        float nextPos = 0f;
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
        else if (depth == 2)
        {
            cursorYpos = 100f;
            maxIndex = 3;
            targetPosArr = cursor3MovePoint;
        }

        // 0 : up, 1 : down, 2: right, 3: left
        if (updown == 0)
        {
            if(cursorDepth < 2)
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
                nextPos = targetPosArr[cursorPointer];
                MoveYPos(nextPos);
            } else if(cursorDepth == 2)
            {
                //Move Still
                cursorPointer = 0;
                MovePos(-270f, 100f);
            }
        }
        else if(updown == 1)
        {
            if(cursorDepth < 2)
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
                nextPos = targetPosArr[cursorPointer];
                MoveYPos(nextPos);
            } else if(cursorDepth == 2)
            {
                //Move Still
                cursorPointer = 4;
                MovePos(-130f, -285f);
            }
        }
        else if(updown == 2)
        {
            if (cursorDepth == 2)
            {
                if(cursorPointer != 4)
                {
                    if (cursorPointer < maxIndex)
                    {
                        cursorPointer += 1;
                    }
                    else
                    {
                        cursorPointer = maxIndex;
                    }
                    nextPos = targetPosArr[cursorPointer];
                    MoveXPos(nextPos);
                }
            } else
            {
                //Move Still
            }
        }
        else if(updown == 3)
        {
            if (cursorDepth == 2)
            {
                if(cursorPointer != 4)
                {
                    if (cursorPointer > 0)
                    {
                        cursorPointer -= 1;
                    }
                    else
                    {
                        cursorPointer = 0;
                    }
                    nextPos = targetPosArr[cursorPointer];
                    MoveXPos(nextPos);
                }
            }
            else
            {
                //Move Still
            }
        }
    }

    public void MoveYPos(float nextPos)
    {
        menuCursor.transform.DOLocalMove(new Vector3(cursorXpos, nextPos), 0.25f).SetEase(Ease.OutBack);
    }

    public void MoveXPos(float nextPos)
    {
        menuCursor.transform.DOLocalMove(new Vector3(nextPos, cursorYpos), 0.25f).SetEase(Ease.OutBack);
    }

    public void MovePos(float nextXPos, float nextYPos)
    {
        menuCursor.transform.DOLocalMove(new Vector3(nextXPos, nextYPos), 0.25f).SetEase(Ease.OutBack);
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
            } else if(pointer == 3)
            {
                menuPage[depth].SetActive(false);
                depth += 1;
                cursorDepth = depth;
                cursorPointer = 0;
                menuPage[depth].SetActive(true);
                MoveOptionCursor(cursorDepth, cursorPointer, 3);
            } else if(pointer == 0)
            {
                BGMText.color = new Color(1f,1f,1f);
                soundSettingCase = 0;
                soundSettingEnabled = true;
            } else if(pointer == 1)
            {
                SFXText.color = new Color(1f,1f,1f);
                soundSettingCase = 1;
                soundSettingEnabled = true;
            }


        } else if(depth == 2)
        {
            //4K Setting
            if(pointer == 4)
            {
                menuPage[depth].SetActive(false);
                depth -= 1;
                cursorDepth = depth;
                cursorPointer = 0;
                menuPage[depth].SetActive(true);
                MoveOptionCursor(cursorDepth, cursorPointer, 0);
            } else
            {
                Setting4K(pointer);
            }
        }
    }

    public void Setting4K(int index)
    {
        buttonImg = ButtonTextArr[index].transform.GetChild(0).gameObject;
        buttonText = ButtonTextArr[index].transform.GetChild(1).GetComponent<Text>();

        buttonImg.GetComponent<Outline>().enabled = true;
        keyIndex = index;
        keySettingEnabled = true;
    }

    public void Change4K(int index, KeyCode keyCode)
    {
        int buttonIdx = index + 1;
        if(buttonIdx == 1)
        {
            OptionManager.instance.input4KBtnKey_1 = keyCode;
        }
        else if(buttonIdx == 2)
        {
            OptionManager.instance.input4KBtnKey_2 = keyCode;
        }
        else if (buttonIdx == 3)
        {
            OptionManager.instance.input4KBtnKey_3 = keyCode;
        }
        else if (buttonIdx == 4)
        {
            OptionManager.instance.input4KBtnKey_4 = keyCode;
        }
    }

    private KeyCode DetectPressedKeyCode()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                return kcode;
            }
        }
        return KeyCode.None;
    }

    public void InitBGMText()
    {
        BGMText.text = OptionManager.instance.musicVolume.ToString();
    }

    public void InitSFXText()
    {
        SFXText.text = OptionManager.instance.sfxVolume.ToString();
    }

}
