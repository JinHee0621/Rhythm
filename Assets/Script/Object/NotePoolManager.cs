using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePoolManager : MonoBehaviour
{
    public LoadRecordDataManager loadRecordDataManager;
    public int poolLength;
    [SerializeField]
    GameObject[] noteObjects;
    public GameObject note;
    public Transform init_position;

    private string[] noteData;

    [SerializeField]
    private int current_index = 0;

    public bool isRecord;

    private void Awake()
    {
        if(!isRecord)
        {
            noteObjects = new GameObject[poolLength];
            for (int i = 0; i < poolLength; i++)
            {
                noteObjects[i] = Instantiate(note, init_position);
            }
        }
    }

    public void InitLine()
    {
        noteData = loadRecordDataManager.ReturnLine();
    }

    public void InitNote(Transform line, int lineNum, float pos, float noteLength)
    {
        for(int i = 0; i < poolLength; i++)
        {
            if(noteObjects[i].GetComponentInChildren<NoteManager>().isChecked == false)
            {
                noteObjects[i].transform.parent = line;
                noteObjects[i].transform.localPosition = new Vector3(0f, pos, -3);
                noteObjects[i].GetComponentInChildren<NoteManager>().lineNum = lineNum;
                noteObjects[i].GetComponentInChildren<NoteManager>().noteLength = noteLength;
                noteObjects[i].GetComponentInChildren<NoteManager>().SetNoteState();
                noteObjects[i].GetComponentInChildren<NoteManager>().isChecked = true;
                current_index += 1;
                break;
            }
        }
    }

    /*
    public void ReInitNote(int index, int lineNum, Transform line, float pos, float noteLength)
    {
        noteObjects[index].transform.parent = line;
        noteObjects[index].transform.localPosition = new Vector3(0f, pos, -3);
        noteObjects[index].GetComponentInChildren<NoteManager>().lineNum = lineNum;
        noteObjects[index].GetComponentInChildren<NoteManager>().noteLength = noteLength;
        noteObjects[index].GetComponentInChildren<NoteManager>().SetNoteState();
        noteObjects[index].GetComponentInChildren<NoteManager>().isChecked = true;
    }
    */

    public void Check(GameObject note)
    {
        note.transform.parent = init_position;
        note.transform.localPosition = new Vector3(0f, 0f, 0f);
        note.GetComponentInChildren<NoteManager>().ReInitNote();
        loadRecordDataManager.NextLine(current_index);
    }

    public void IndexCount()
    {
        current_index += 1;
    }

    public int IndexPrint()
    {
        return current_index;
    }
}

