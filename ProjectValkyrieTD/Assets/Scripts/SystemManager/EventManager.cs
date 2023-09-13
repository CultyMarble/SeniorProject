using System;

public static class EventManager
{
    // SCENE LOAD EVENTS
    // Before Scene Unload Load Screen Event
    public static event Action BeforeLoadingScreenEvent;
    public static void CallBeforeLoadingScreenEvent()
    {
        if (BeforeLoadingScreenEvent != null)
        {
            BeforeLoadingScreenEvent();
        }
    }

    // Before Scene Unload Event
    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeUnloadingSceneEvent()
    {
        if (BeforeSceneUnloadEvent != null)
        {
            BeforeSceneUnloadEvent();
        }
    }

    // After Scene Loaded Event
    public static event Action AfterSceneLoadEvent;
    public static void CallAfterSceneLoadEvent()
    {
        if (AfterSceneLoadEvent != null)
        {
            AfterSceneLoadEvent();
        }
    }

    // After Scene Loaded Load Screen Event
    public static event Action AfterLoadingScreenEvent;
    public static void CallAfterLoadingScreenEvent()
    {
        if (AfterLoadingScreenEvent != null)
        {
            AfterLoadingScreenEvent();
        }
    }
}