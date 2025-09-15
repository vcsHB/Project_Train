using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;
namespace Project_Train.ResourceSystem
{


    [CreateAssetMenu(menuName = "SO/Manager/ResourceManager")]
    public class ResourceManagerSO : ScriptableObject
    {
        public SerializedDictionary<ResourceType, ResourceEntry> resourceDictionary;

        // TODO : Need DataLoad Value Setter

        public int GetResourceValue(ResourceType type)
        {
            if (resourceDictionary.TryGetValue(type, out ResourceEntry entry))
            {
                return entry.amount;
            }
            else
            {
                return 0;
            }
        }

        public void Add(ResourceType resourceType, int amount)
        {
            resourceDictionary[resourceType].Add(amount);
        }
        public bool TrySubtractResource(ResourceType resourceType, int amount)
        {
            return resourceDictionary[resourceType].TrySubtract(amount);
        }

        public bool IsEnough(ResourceType resourceType, int amount)
        {
            return resourceDictionary[resourceType].IsEnough(amount);
        }


        //
        public void ClearEventListener() // Must Call when Scene Destroyed
        {
            foreach (var item in resourceDictionary.Values)
            {
                item.ClearEvent();
            }
        }

        public void AddEventListener(ResourceType resourceType, Action<int> handler)
        {
            resourceDictionary[resourceType].OnValueChangedEvent += handler;

        }

        public void RemoveEventListener(ResourceType resourceType, Action<int> handler)
        {
            resourceDictionary[resourceType].OnValueChangedEvent -= handler;

        }
    }
}