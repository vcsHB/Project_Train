using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenPooling
{
	public static class PopCore
	{
		private static PoolManager _poolManager { get; set; }
		private static List<PoolCategorySO> _poolBaseList;

		public static void Init(PoolManager poolManager, List<PoolCategorySO> poolBaseList)
		{
			_poolManager = poolManager;
			_poolBaseList = poolBaseList;
		}

		public static IPoolingObject Pop(this GameObject target, Enum type, Transform parent = null)
		{
			return Pop(target, type, parent, Vector2.zero, Quaternion.identity);
		}

		public static IPoolingObject Pop(this GameObject target, Enum type, Vector3 pos, Quaternion rot)
		{
			return Pop(target, type, null, pos, rot);
		}

		public static IPoolingObject Pop(this GameObject target, Enum type, Transform parent, Vector3 pos, Quaternion rot)
		{
			try
			{
				IPoolingObject poolingObject;

				string[] poolName = { type.GetType().ToString().Replace("Crogen.CrogenPooling.", string.Empty).Replace("PoolType", string.Empty), type.ToString() };
				string typeName = $"{poolName[0]}.{poolName[1]}";

				if (PoolManager.poolDict[typeName].Count == 0)
				{
					foreach (var poolBase in _poolBaseList)
					{
						if (poolName[0] != poolBase.name) continue;
						for (int i = 0; i < poolBase.pairs.Count; i++)
						{
							if (poolBase.pairs[i].poolType.Equals(poolName[1]))
							{
								poolingObject = PoolManager.CreateObject(poolBase, poolBase.pairs[i], Vector3.zero, Quaternion.identity);
								PoolManager.PoolingObjectInit(poolingObject, typeName, PoolManager.Transform);
								break;
							}
						}
					}
				}
				poolingObject = PoolManager.poolDict[typeName].Pop();
				GameObject obj = poolingObject.gameObject;

				obj.SetActive(true);

				obj.transform.SetParent(parent);
				obj.transform.localPosition = pos;
				obj.transform.localRotation = rot;
				poolingObject.OnPop();

				return poolingObject;
			}
			catch (KeyNotFoundException)
			{
				Debug.LogError("You should make 'PoolManager'!");
				throw;
			}
		}
	}
}