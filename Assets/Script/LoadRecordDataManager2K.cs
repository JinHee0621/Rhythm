using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadRecordDataManager2K : MonoBehaviour
{
    public string Song_Name;

    public GameObject Note;
    public GameObject NoteLine_Base;

    public Transform Line1;
    public Transform Line2;

    private int Line_index = 0;

    void Awake()
    {
        string[] lines = File.ReadAllLines(@".\Assets\RecordData\"+ Song_Name + ".txt");
        foreach (string data in lines)
        {
            if(Line_index > 0)
            {
                string[] data_element = data.Split("|");

                Transform parent_line = null;
                if (data_element[0].Equals("Btn1")) parent_line = Line1;
                else if (data_element[0].Equals("Btn2")) parent_line = Line2;

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
                }
            }
            Line_index += 1;
        }
        NoteLine_Base.GetComponent<NoteMoveManager>().running = true;
    }
}
