using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{

    public Transform ScrollContent;
    public Transform Scroll;
    public InputField Input;
    public Text DebugText;
    public TilemapCollider2D Tilemap;

    Transform FindRec(Transform root, System.Predicate<Transform> predicate)
    {
        if (predicate(root))
            return root;
        if (root.childCount > 0)
        {
            for (int i = 0; i < root.childCount; i++)
            {
                var f = FindRec(root.GetChild(i), predicate);
                if (f)
                    return f;
            }
        }
        return null;
    }

    void Start()
    {

        if (Scroll)
            Scroll.gameObject.SetActive(false);
    }

    void Update()
    {

    }

    public void OnClick()
    {
        if (Scroll)
            Scroll.gameObject.SetActive(!Scroll.gameObject.activeInHierarchy);

        Print("Tilemap is" + Tilemap.enabled.ToString());
    }

    public void Submit()
    {
        if (!string.IsNullOrEmpty(Input.text))
        {
            var text = Instantiate(DebugText, ScrollContent);
            text.text = Input.text;
            Input.text = "";
            CommandInterpr(text.text);
        }
    }

    void CommandInterpr(string comm)
    {
        if (comm[0] == '\'')
        {
            comm = comm.Substring(1);
            switch (comm)
            {
                case "kek":
                Application.OpenURL("https://geektimes.ru/post/294881/.com[perevod]-mayning-efiriuma-za-5-minut");
                    break;
                    
            }
        }
    }

    public void Print(string message)
    {
        var text = Instantiate(DebugText, ScrollContent);
        text.text = message;
    }
}
