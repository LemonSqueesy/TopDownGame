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
    public bool ButtonDown;
    public bool ButtonClamp;
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
        Shoot
    }

    public bool WindowsDebug;
    private bool AndroidPlatform = Application.platform == RuntimePlatform.Android;

    public static GameActionManager Instance { get; private set; }

    [Header("Настройки управления для винды!")]
    public WindowsPair[] WindowsInputSetting;

    [Header("Настройки управления для мобилки!")]
    public AndroidPair[] InputMobileSetting;

    public GameActionManager()
    {
        WindowsInputSetting = new WindowsPair[System.Enum.GetNames(typeof(GameAction)).Length];
        InputMobileSetting = new AndroidPair[System.Enum.GetNames(typeof(GameAction)).Length];
    }

    public void GetPlayerRotation(Vector3 pos, out Vector2? mousePosWorld, out Quaternion? lookRot)
    {
        if (AndroidPlatform)
        {
            mousePosWorld = new Vector2(pos.x+1,pos.y+1);
            var r = GetVirtual();
            if (r.HasValue)
                lookRot = r.Value;
            else
                lookRot = null;
        }
        else
        {
            GetMouseLook2D(pos, out mousePosWorld, out lookRot);
            lookRot = lookRot.Value * Quaternion.Euler(0, 0, 90);
        }
    }

    void GetMouseLook2D(Vector3 pos, out Vector2? mousePosWorld, out Quaternion? lookRot)
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(pos - mousePosition, Vector3.forward);
        rot.eulerAngles = new Vector3(0, 0, rot.eulerAngles.z);
        lookRot = rot;
        mousePosWorld = mousePosition;
    }

    public bool GetAction(GameAction action)
    {
        if (AndroidPlatform)
            return GetAndroidAction(action);
        else
            return GetWindowsAction(action);
    }

    public Vector2 GetMove()
    {
        Vector2 move = new Vector2(0, 0);
        if (AndroidPlatform)
        {
            move = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        }
        else
        {
            if (GetWindowsAction(GameAction.MoveUp))
            {
                move += new Vector2(0, 1);
            }
            if (GetWindowsAction(GameAction.MoveDown))
            {
                move += new Vector2(0, -1);
            }
            if (GetWindowsAction(GameAction.MoveLeft))
            {
                move += new Vector2(-1, 0);
            }
            if (GetWindowsAction(GameAction.MoveRight))
            {
                move += new Vector2(1, 0);
            }
        }

        return move;
    }


    Quaternion? GetVirtual()
    {
        var x = CrossPlatformInputManager.GetAxis("Mouse X");
        var y = CrossPlatformInputManager.GetAxis("Mouse Y");
        if (x != 0 && y != 0)
        {
            float headingAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            if (headingAngle < 0) headingAngle += 360f;
            Quaternion newHeading = Quaternion.Euler(0f, 0f, headingAngle);
            return newHeading;
        }
        return null;
    }

    private bool GetWindowsAction(GameAction action)
    {
        WindowsPair? item = WindowsInputSetting.FirstOrDefault(x => x.Action == action);
        if (item.HasValue)
            return Input.GetKey(item.Value.Key);
        else
            return false;
    }

    private bool GetAndroidAction(GameAction action)
    {
        var item = InputMobileSetting.First(x => x.Action == action);
        if (!item.IsButton)
        {
            float axisVal = CrossPlatformInputManager.GetAxis(item.Key);

            if (action == GameAction.MoveLeft || action == GameAction.MoveUp)
                return axisVal > 0 ? true : false;

            if (action == GameAction.MoveRight || action == GameAction.MoveDown)
                return axisVal < 0 ? true : false;
        }
        else if (item.ButtonDown)
            return CrossPlatformInputManager.GetButtonDown(item.Key);
        else if (item.ButtonClamp)
            return CrossPlatformInputManager.GetButton(item.Key);

        return false;
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        Input.simulateMouseWithTouches = false;
    }


    void Start()
    {
#if UNITY_EDITOR
        AndroidPlatform |= UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android;
        AndroidPlatform &= !WindowsDebug;
#endif
    }

    void Update()
    {

    }
}
