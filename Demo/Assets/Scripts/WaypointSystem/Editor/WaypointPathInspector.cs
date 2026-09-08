using UnityEngine;
using UnityEditor;

namespace UnityDemo
{
	/// <summary>
	/// WaypointPathInspector is a custom editor for the WaypointPath class.
	/// It allows the user to add waypoints to the path directly from the inspector.
	/// </summary>
	[CustomEditor(typeof(WaypointPath))]
	public class WaypointPathInspector : Editor
	{
		private WaypointPath _path = null;

		private void OnEnable()
		{
			_path = target as WaypointPath;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("Add Waypoint"))
			{
				int childCount = _path.transform.childCount;
				string name = $"Waypoint{(childCount + 1):D3}";
				GameObject waypointGO = new GameObject(name);

				waypointGO.transform.parent = _path.transform;
				waypointGO.transform.position = _path.transform.position;
				Waypoint waypoint = waypointGO.AddComponent<Waypoint>();

				_path.OnValidate();

				Selection.activeGameObject = waypointGO;
			}
		}
	}
}