using System;
using System.Collections.Generic;
using UnityEngine;

namespace  Project_Train.EntitySystem
{
    public abstract class EntityBase<T> : MonoBehaviour where T : EntityBase<T>
	{
        protected Dictionary<Type, IEntityComponent<T>> _componentDictionary;

        protected virtual void Awake()
        {
            var components = GetComponentsInChildren<IEntityComponent<T>>();

            foreach (var component in components) 
            {
                _componentDictionary.Add(component.GetType(), component);
            }
        }

        protected virtual void InitializeComponents()
        {

        }

        protected virtual void Start()
        {
        
        }

		protected virtual void Update()
        {
        
        }
    }
}
