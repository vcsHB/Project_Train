using Crogen.CrogenPooling;
using UnityEngine;

public class TrailRendererPoolingExtension : MonoBehaviour
{
	[SerializeField] private Transform _poolingObjectParent;
	public float duration = 2f;
	private float _curTime = 0f;
	private TrailRenderer _trailRenderer;
	private IPoolingObject _owner;
	private bool _isParentDisable = false;

	private void Awake()
	{
		_owner = _poolingObjectParent.GetComponent<IPoolingObject>();
		_trailRenderer = GetComponent<TrailRenderer>();

		if (_trailRenderer == null)
		{
			Debug.LogError("TrailRenderer�� �������� �ʽ��ϴ�.");
			return;
		}

		if (_owner == null)
			Debug.LogError("TrailRenderer���� IPoolingObject�� ��ӹ޴� �θ� �������� �ʽ��ϴ�.");
	}

	private void OnDisable()
	{
		_trailRenderer.Clear();
		_isParentDisable = false;
		_curTime = 0f;
	}

	private void Update()
	{
		if (_poolingObjectParent.gameObject.activeInHierarchy == false && _isParentDisable == false)
		{
			transform.SetParent(null, true);
			_isParentDisable = true;
		}

		if (_isParentDisable)
		{
			_curTime += Time.deltaTime;

			if (_curTime > duration)
			{
				transform.SetParent(_poolingObjectParent.transform, false);
			}
		}
	}
}
