using System;
using UnityEngine;

namespace Lean.Pool
{
	/// <summary>This component allows you to reset a Rigidbody's velocity via Messages or via Poolable.</summary>
	[RequireComponent(typeof(Rigidbody))]
	[HelpURL(LeanPool.HelpUrlPrefix + "LeanPooledRigidbody")]
	[AddComponentMenu(LeanPool.ComponentPathPrefix + "Pooled Rigidbody")]
	public class LeanPooledRigidbody : MonoBehaviour, IPoolable
	{
		protected Rigidbody _rigidbody;

		protected virtual void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		void IPoolable.OnSpawn()
		{
		}

		void IPoolable.OnDespawn()
		{
			_rigidbody.velocity        = Vector3.zero;
			_rigidbody.angularVelocity = Vector3.zero;
		}
	}
}