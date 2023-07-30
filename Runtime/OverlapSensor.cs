using UnityEngine;

namespace ThreeDISevenZeroR.SensorKit
{
    /// <summary>
    /// <para>Abstract class for any overlap sensor</para>
    /// <para>Overlap sensor can detect objects at specified location</para>
    /// </summary>
    public abstract class OverlapSensor: PhysicsSensor
    {
        #region Abstract
        protected abstract int DoOverlapCheck(Vector3 center, Collider[] colliders);
        #endregion
        #region MonoBehaviour Lifecycle
        private void Start()
        {
            if (!lazyAllocation) EnsureArrayCapacity(ref hitColliders);
        }
        #endregion
        #region PhysicsSensor Implementation
        public override int UpdateSensor()
        {
            EnsureArrayCapacity(ref hitColliders);
            hitCount = DoOverlapCheck(transform.position, hitColliders);
            return hitCount;
        }
        #endregion

#if UNITY_EDITOR
        private static Collider[] gizmoCollider = new Collider[1];

        private void OnDrawGizmosSelected()
        {
            var count = DoOverlapCheck(transform.position, gizmoCollider);
            Gizmos.color = count != 0 ? PhysicsSensorUtils.hasHitColor : PhysicsSensorUtils.noHitColor;
            DrawColliderShape(transform.position, transform.rotation, transform.lossyScale);
        }

        protected virtual void DrawColliderShape(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            // noop
        }
#endif
    }
}