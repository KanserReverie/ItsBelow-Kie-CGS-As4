using System;
using UnityEngine;
public class PoolTesting : MonoBehaviour
{
	public Pooling cubePool;
	// Start is called before the first frame update
	void Start()
	{
		cubePool.Spawn();
		Transform test = cubePool.Spawn();
		cubePool.Despawn(test);
		cubePool.Spawn();
		cubePool.Spawn();
		Transform test2 = cubePool.Spawn();
		cubePool.Despawn(test2);
		Debug.Log("Spawned Cubes");
		Debug.LogWarning("Spawned Cubes 2");
		Debug.LogWarning("Spawned Cubes 3");
		int y = 0;
		try
		{
			int x = 10 / y;
		}
		catch (DivideByZeroException e)
		{
			Debug.LogWarning(e.Message);
		}
		catch (Exception e)
		{
			Debug.LogError(e.Message);
		}
		finally
		{
			//file.Close();
		}
		Debug.Log("We are here");
	}

	private void Update()
	{
		//Input.
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		// Debug.Log("update!");
	}
}