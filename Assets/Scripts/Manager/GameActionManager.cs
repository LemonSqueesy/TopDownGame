using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public struct WindowsPair
{
    public GameActionManager.GameAction Action;
    public KeyCode Key;
}

[System.Serializable]
public struct AndroidPair
{
    public GameActionManager.GameAction Action;
    public bool IsButton;
    public string Key;
}

public class GameActionManager : MonoBehaviour
{
    public enum GameAction
    {
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        Run,
        Shot
    }

    public bool WindowsDebug;

    public static GameActionManager Instance { get; private set; }

    [Header("Настройки управления для винды!")]
    public WindowsPair[] WindowsInputSetting;

    [Header("Настройки управления для мобилки!")]
    public AndroidPair[] InputMobileSetting;

    public GameActionManager()
    {
        WindowsInputSetting = new WindowsPair[System.Enum.GetNames(typeof(GameAction)).Length];
        InputMobileSetting = new AndroidPair[System.Enum.GetNames(typeof(GameAction)).Length];

        //WindowsInputSetting[0].Action = GameAction.MoveLeft;
        //WindowsInputSetting[0].Key = KeyCode.A;
        //WindowsInputSetting[1].Action = GameAction.MoveRight;
        //WindowsInputSetting[1].Key = KeyCode.D;
        //WindowsInputSetting[2].Action = GameAction.MoveUp;
        //WindowsInputSetting[2].Key = KeyCode.W;
        //WindowsInputSetting[3].Action = GameAction.MoveDown;
        //WindowsInputSetting[3].Key = KeyCode.S;

        //InputMobileSetting[0].Action = GameAction.MoveLeft;
        //InputMobileSetting[0].Key = "Horizontal";
        //InputMobileSetting[1].Action = GameAction.MoveRight;
        //InputMobileSetting[1].Key = "Horizontal";
        //InputMobileSetting[2].Action = GameAction.MoveUp;
        //InputMobileSetting[2].Key = "Vertical";
        //InputMobileSetting[3].Action = GameAction.MoveDown;
        //InputMobileSetting[3].Key = "Vertical";
    }

    public void GetMouseLook2D(Vector3 pos, out Vector2 mousePosWorld, out Quaternion lookRot)
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(pos - mousePosition, Vector3.forward);
        rot.eulerAngles = new Vector3(0, 0, rot.eulerAngles.z);
        lookRot = rot;
        mousePosWorld = mousePosition;
    }

    public bool GetAction(GameAction action)
    {
        if ((Application.platform == RuntimePlatform.Android || UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android) && !WindowsDebug)
            return GetAndroidAction(action);
        else
            return GetWindowsAction(action);
    }


    private bool GetWindowsAction(GameAction action)
    {
        return Input.GetKey(WindowsInputSetting.First(x => x.Action == action).Key);
    }

    private bool GetAndroidAction(GameAction action)
    {
        var item = InputMobileSetting.First(x => x.Action == action);
        if (!item.IsButton)
        {
            float axisVal = CrossPlatformInputManager.GetAxis(item.Key);
            if (action == GameAction.MoveLeft || action == GameAction.MoveUp)
                return axisVal < 0 ? true : false;

            if (action == GameAction.MoveRight || action == GameAction.MoveDown)
                return axisVal > 0 ? true : false;
        }
        else
            return CrossPlatformInputManager.GetButton(item.Key);
        return false;
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;

    }


    void Start()
    {

    }

    void Update()
    {

    }
}
