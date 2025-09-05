using UnityEngine;

namespace  Project_Train.EntitySystem
{
    public interface IEntityComponent<T> where T : EntityBase<T>
    {
        void Initialize(T owner);
    }
}
