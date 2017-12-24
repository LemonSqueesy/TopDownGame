using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{
    System.Diagnostics.PerformanceCounter cpuCounter;
    System.Diagnostics.PerformanceCounter ramCounter;

    public Transform ScrollContent;
    public Transform Scroll;

    public InputField Input;

    public Text DebugText;
    public Text FPS;
    public Text CPU;
    public Text RAM;
    public Text AudioDB;

    private float updateInterval = 0.5f;
    private float powerBank = 0.0f;
    private float clocksGone;

    private int cadre = 0;
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
        cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total",true);

        //cpuCounter.CategoryName = "Processor";
        //cpuCounter.CounterName = "% Processor Time";
        //cpuCounter.InstanceName = "_Total";
        

        ramCounter = new PerformanceCounter("Memory", "Available MBytes");

        FPS.gameObject.SetActive(false);
        CPU.gameObject.SetActive(false);
        RAM.gameObject.SetActive(false);

        clocksGone = updateInterval;
        if (Scroll)
            Scroll.gameObject.SetActive(false);
    }

    void Update()
    {
        if (FPS.gameObject.activeInHierarchy) //в нулевой, потому что захотелось, какая разница, ведь я делаю active весь массив. Чекай строку 100!
        {
            CPU.text = "> CPU: " + GetCPU();
            RAM.text = "> RAM: " + GetRAM();

            clocksGone -= Time.deltaTime;
            powerBank += Time.timeScale / Time.deltaTime;
            ++cadre;

            if (clocksGone <= 0.0f)
            {
                FPS.text = (powerBank / cadre).ToString("f2");

                clocksGone = updateInterval;
                powerBank = 0.0f;
                cadre = 0;
            }
        }

    }

    public void OnClick()
    {
        if (Scroll)
            Scroll.gameObject.SetActive(!Scroll.gameObject.activeInHierarchy);
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

                case "stats":
                    FPS.gameObject.SetActive(true);
                    CPU.gameObject.SetActive(true);
                    RAM.gameObject.SetActive(true);
                    break;

            }
        }
    }

    public void Print(string message)
    {
        var text = Instantiate(DebugText, ScrollContent);
        text.text = message;
    }

    public string GetCPU()
    {
        return cpuCounter.NextValue() + " %";
    }

    public string GetRAM()
    {
        return ramCounter.NextValue() + " MB";
    }
}
