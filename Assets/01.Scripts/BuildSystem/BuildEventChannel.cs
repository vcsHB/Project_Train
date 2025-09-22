using System;
namespace Project_Train.BuildSystem
{

    public static class BuildEventChannel
    {
        public static event Action<Building> OnBuildEvent;
        public static event Action<Building> OnDestroyEvent;
        public static event Action OnBuildCancelEvent;
        public static void ClearBuildEvent()
        {
            OnBuildEvent = null;
        }
        public static void InvokeBuildEvent(Building newBuilding)
        {
            OnBuildEvent?.Invoke(newBuilding);
        }

        public static void InvokeDestroyEvent(Building building)
        {
            OnDestroyEvent?.Invoke(building);
        }
    }
}