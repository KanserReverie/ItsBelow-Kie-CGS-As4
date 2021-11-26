using UnityEngine;

public class Done : MonoBehaviour
{
	private void OnCollisionEnter(Collision _collision)
	{
		if(_collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
		#region Quit on ESC
			Application.Quit();
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif

		#endregion
		}

	}

	private void OnCollisionStay(Collision _collision)
	{
		if(_collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
		#region Quit on ESC
			Application.Quit();
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif

		#endregion
		}
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if(hit.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
		#region Quit on ESC
			Application.Quit();
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#endif

		#endregion
		}
	}
}
