using System;
using System.Text;
using UnityEngine;
using TMPro;

public class TimeManager : SingletonMonobehaviour<TimeManager>
{
    public struct OnTimeSecondChangedEventArgs { public int second; }
    public event EventHandler<OnTimeSecondChangedEventArgs> OnTimeSecondChanged;

    public struct OnTimeMinuteChangedEventArgs { public int minute; }
    public event EventHandler<OnTimeMinuteChangedEventArgs> OnTimeMinuteChanged;

    [SerializeField] private TextMeshProUGUI timeText;

    [Header("Time Settings:")]
    [SerializeField] private int gameplayTimeInMinutes;

    private int minute = default;
    private int second = default;
    private float timeCounter = default;

    private StringBuilder stringBuilderMinute;
    private StringBuilder stringBuilderSecond;
    private StringBuilder displayTime;

    public bool runTimer = false;

    //===========================================================================
    protected override void Awake()
    {
        base.Awake();

        ResetTimer();
    }

    private void OnEnable()
    {
        SceneLoadManager.Instance.OnPurge += Instance_OnPurge;
    }

    private void Update()
    {
        if (timeText == null)
            return;

        if (runTimer == false)
            return;

        timeCounter += Time.deltaTime;
        if (timeCounter >= 1)
        {
            timeCounter -= 1;

            if (second == 0)
            {
                second = 60;

                minute--;
                OnTimeMinuteChanged?.Invoke(this, new OnTimeMinuteChangedEventArgs { minute = minute});
                UpdateStringBuilder(stringBuilderMinute, minute);
                UpdateDisplayTime();
            }

            second--;
            OnTimeSecondChanged?.Invoke(this, new OnTimeSecondChangedEventArgs { second = second });
            UpdateStringBuilder(stringBuilderSecond, second);
            UpdateDisplayTime();
        }
    }

    private void OnDisable()
    {
        SceneLoadManager.Instance.OnPurge -= Instance_OnPurge;
    }

    //===========================================================================
    private void Instance_OnPurge(object sender, EventArgs e)
    {
        runTimer = true;
    }

    //===========================================================================
    private void UpdateStringBuilder(StringBuilder stringBuilder, int timeParameter)
    {
        stringBuilder.Clear();
        if (timeParameter < 10)
        {
            stringBuilder.Append("0");
            stringBuilder.Append(timeParameter);
        }
        else
        {
            stringBuilder.Append(timeParameter);
        }
    }

    private void UpdateDisplayTime()
    {
        displayTime.Clear();
        displayTime.Append(stringBuilderMinute);
        displayTime.Append(":");
        displayTime.Append(stringBuilderSecond);

        timeText.SetText(displayTime);
    }

    public void ResetTimer()
    {
        stringBuilderMinute = new StringBuilder();
        stringBuilderSecond = new StringBuilder();
        displayTime = new StringBuilder();

        minute = gameplayTimeInMinutes;
        second = 0;

        if (timeText != null)
        {
            UpdateStringBuilder(stringBuilderMinute, minute);
            UpdateStringBuilder(stringBuilderSecond, second);
            UpdateDisplayTime();
        }
    }
}