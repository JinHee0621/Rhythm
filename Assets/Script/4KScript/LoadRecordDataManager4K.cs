using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadRecordDataManager4K : MonoBehaviour
{
    public ScoreManager scoreManager;
    public string Song_Name;

    public GameObject Note;
    public GameObject NoteLine_Base;

    public Transform Line1;
    public Transform Line2;
    public Transform Line3;
    public Transform Line4;

    private int Line_index = 0;

    void Awake()
    {
        string[] lines = File.ReadAllLines("./Assets/RecordData/" + Song_Name + ".txt");//(@".\Assets\RecordData\"+ Song_Name + ".txt");
        scoreManager.NoteCountInit((lines.Length - 1));
        foreach (string data in lines)
        {
            if(Line_index > 0)
            {
                string[] data_element = data.Split("|");

                Transform parent_line = null;
                if (data_element[0].Equals("Btn1")) parent_line = Line1;
                else if (data_element[0].Equals("Btn2")) parent_line = Line2;
                else if (data_element[0].Equals("Btn3")) parent_line = Line3;
                else if (data_element[0].Equals("Btn4")) parent_line = Line4;

                float position_y = 0f;
                float.TryParse(data_element[1], out position_y);

                float noteLength = 0f;
                float.TryParse(data_element[2], out noteLength);

                if (parent_line != null)
                {
                    GameObject newNote = Instantiate(Note, parent_line);
                    newNote.transform.localPosition = new Vector3(0f, position_y, -3);
                    newNote.GetComponentInChildren<NoteManager>().noteLength = noteLength;
                    newNote.GetComponentInChildren<NoteManager>().SetNoteState();
                    newNote.GetComponentInChildren<NoteManager>().noteId = Line_index;
                }
            }
            Line_index += 1;
        }
        NoteLine_Base.GetComponent<NoteMoveManager>().running = true;
    }
}
