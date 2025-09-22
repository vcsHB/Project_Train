using AYellowpaper.SerializedCollections;
using Project_Train.DataManage.CoreDataBaseSystem;
using UnityEngine;
namespace Project_Train.ResourceSystem
{

    public class ResourceDataManager : MonoBehaviour
    {
        [SerializeField] private ResourceManagerSO _resourceManager;
        [SerializeField] private SerializedDictionary<ResourceDataSO, int> _initResource;

        private void Awake()
        {

            //TODO : ResourceSave Data Load
            foreach (var item in _initResource)
            {
                _resourceManager.SetResourceAmount(item.Key.resourceType, item.Value);
            }
        }


    }
}