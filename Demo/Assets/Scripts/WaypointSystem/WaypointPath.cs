using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace UnityDemo
{
    /// <summary>
    /// WaypointPath is a class that represents a path made up of waypoints.
    /// It can be used to create different types of paths, such as looping or ping-pong paths.
    /// This class also generates a spline based on the waypoints for smooth movement along the path.
    /// The path can be visualized in the editor using Gizmos.
    /// </summary>
    public class WaypointPath : MonoBehaviour
    {
        public enum PathType
        {
            None = 0,
            DelayLoop,
            DelayPingPong
        }

        public enum Direction
        {
            None = 0,
            Forward,
            Backward
        }

        [SerializeField] private PathType _type = PathType.None;
        [SerializeField] private bool _useSpline = false;

        private Waypoint[] _path;
        private Spline _spline;

        public PathType Type => _type;

        public bool UseSpline
        {
            get
            {
                if (!_useSpline) return false;

                if (_spline == null)
                {
                    _spline = GenerateSpline(_path);
                }

                return _spline != null;
            }
        }

        private void Start()
        {
            _path = GetComponentsInChildren<Waypoint>(includeInactive: true);
            if (_useSpline)
            {
                _spline = GenerateSpline(_path);
            }
        }

        private Spline GenerateSpline(Waypoint[] path)
        {
            Spline spline = new Spline(knotCapacity: path.Length);
            // Add knots to spline based on waypoints
            foreach (Waypoint waypoint in path)
            {
                BezierKnot knot = new BezierKnot(waypoint.transform.position);
                spline.Add(knot, TangentMode.AutoSmooth);
            }

            // If the path type is Loop, close the spline
            spline.Closed = _type == PathType.DelayLoop;

            return spline;
        }

        public float GetSplineLength()
        {
            if (_spline == null)
            {
                return -1f;
            }

            return _spline.GetLength();
        }

        public Vector3 GetPointOnSpline(float distance)
        {
            float t = distance / GetSplineLength();
            return _spline.EvaluatePosition(t);
        }

        public Waypoint GetNextWaypoint(Waypoint previous, Direction travellingDirection)
        {
            int index = travellingDirection == Direction.Forward ? -1 : _path.Length;
            if (previous != null)
            {
                for (int i = 0; i < _path.Length; ++i)
                {
                    if (_path[i] == previous)
                    {
                        index = i;
                        break;
                    }
                }
            }

            switch (_type)
            {
                case PathType.DelayLoop: return GetNextWaypointLoop(index, travellingDirection);
                case PathType.DelayPingPong: return GetNextWaypointPingPong(index, ref travellingDirection);
                default: return null;
            }
        }

        /// <summary>
        /// Gets the next waypoint in a loop path.
        /// If the end of the path is reached, it loops back to the start.
        /// </summary>
        /// <param name="index">The index of the waypoint.</param>
        /// <param name="pathDirection">The direction of the path.</param>
        /// <returns>The next waypoint.</returns>
        private Waypoint GetNextWaypointLoop(int index, Direction pathDirection)
        {
            int nextIndex;

            if (pathDirection == Direction.Forward)
            {
                nextIndex = index + 1;
                if (nextIndex >= _path.Length)
                {
                    nextIndex = 0;
                }
            }
            else
            {
                nextIndex = index - 1;
                if (nextIndex < 0)
                {
                    nextIndex = _path.Length - 1;
                }
            }

            return _path[nextIndex];
        }

        /// <summary>
        /// Gets the next waypoint in a ping-pong path.
        /// If the end of the path is reached, the direction reverses.
        /// </summary>
        /// <param name="previousIndex">The index of the previous waypoint.</param>
        /// <param name="travellingDirection">The direction in which path user is travelling.</param>
        /// <returns>The next waypoint.</returns>
        private Waypoint GetNextWaypointPingPong(int previousIndex,
            ref Direction travellingDirection)
        {
            int nextIndex;
            switch (travellingDirection)
            {
                case Direction.Forward:
                    nextIndex = previousIndex + 1;
                    if (nextIndex >= _path.Length)
                    {
                        // Direction changes
                        travellingDirection = Direction.Backward;
                        nextIndex -= 2;
                    }

                    break;
                case Direction.Backward:
                    nextIndex = previousIndex - 1;
                    if (nextIndex < 0)
                    {
                        travellingDirection = Direction.Forward;
                        nextIndex = 1;
                    }

                    break;
                default:
                    // No movement here
                    nextIndex = previousIndex;
                    break;
            }

            return _path[nextIndex];
        }

        public Waypoint GetFirstWaypoint()
        {
            return _path[0];
        }

        #region Editor stuff

#if UNITY_EDITOR

        [ContextMenu("Redraw")]
        public void OnValidate()
        {
            _path = GetComponentsInChildren<Waypoint>(includeInactive: true);
            if (_useSpline)
            {
                _spline = GenerateSpline(_path);
            }
        }

        private Dictionary<PathType, Color> _pathColours = new Dictionary<PathType, Color>
        {
            { PathType.DelayLoop, Color.magenta },
            { PathType.DelayPingPong, Color.blue }
        };

        private void OnDrawGizmos()
        {
            if (_path == null || _path.Length < 2)
            {
                return;
            }

            // Sets a colour for a gizmo
            Gizmos.color = _pathColours[_type];

            for (int i = 1; i < _path.Length; ++i)
            {
                Waypoint from = _path[i - 1];
                Waypoint to = _path[i];

                if (from == null || to == null)
                {
                    OnValidate();
                    return;
                }

                Gizmos.DrawLine(from.transform.position, to.transform.position);
            }

            if (_type == PathType.DelayLoop)
            {
                Waypoint from = _path[_path.Length - 1];
                Waypoint to = _path[0];

                Gizmos.DrawLine(from.transform.position, to.transform.position);
            }

            if (_spline != null)
            {
                Gizmos.color = Color.cyan;

                int samples = 100;

                for (int i = 0; i < samples; ++i)
                {
                    float t1 = i / (float)samples;
                    float t2 = (i + 1) / (float)samples;
                    Vector3 from = _spline.EvaluatePosition(t1);
                    Vector3 to = _spline.EvaluatePosition(t2);
                    Gizmos.DrawLine(from, to);
                }
            }
        }
#endif
        #endregion

    }
}
