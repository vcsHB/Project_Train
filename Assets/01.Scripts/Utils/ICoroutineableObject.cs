using UnityEngine;

namespace Project_Train.Utils
{
	public interface ICoroutineableObject<T> where T : MonoBehaviour
	{
		public void Initialize(T owner);
		public T Owner { get; set; }
	}
}
