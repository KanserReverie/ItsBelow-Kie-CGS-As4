using System;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
	public Transform prefab;
	[SerializeField] private List<Transform> objects;
	[SerializeField] private List<Transform> objectsSpawned;
	[SerializeField] private bool resetTransform;
	
	private void Awake()
	{
		objects = new List<Transform>();
		// objects = new List<Transform>();
		if(prefab == null)
		{
			Debug.LogError("Pool " + gameObject.name + " does not have a prefab attached");
		}
	}
	public Transform Spawn()
	{
		return Spawn(null);
	}

	public void SpawnButton()
	{
		Spawn();
	}

	public void SpawnButton(Transform parent)
	{
		Spawn(parent);
	}
	
	private void Reset()
	{
		throw new NotImplementedException();
	}

	public Transform Spawn(Transform parent)
	{
		if (objects.Count == 0)
		{
			Transform spawnedObject = Instantiate(prefab, parent);
			objectsSpawned.Add(spawnedObject);
			return spawnedObject;
		}
		else
		{
			Transform spawnedT = objects[0];
			objects.RemoveAt(0);

			if(resetTransform)
			{
				spawnedT.parent = parent;
				spawnedT.position = prefab.position;
				spawnedT.rotation = prefab.rotation;
				spawnedT.localScale = prefab.localScale;
			}

			spawnedT.gameObject.SetActive(true);
			objectsSpawned.Add(spawnedT);
			return spawnedT;
		}
	}
	public void Despawn(Transform spawnedGO)
	{
		spawnedGO.gameObject.SetActive(false);
		spawnedGO.parent = transform;
		objects.Add(spawnedGO);
		objectsSpawned.Remove(spawnedGO);
	}

	public void Despawn()
	{
		if(objectsSpawned.Count > 0)
			Despawn(objectsSpawned[0]);
	}
}