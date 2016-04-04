/*
 * Author: Scott Goodrow
 * http://www.thewildeternal.com/2015/08/31/devlog-force-component-order/
 * Modified by Soullesswaffle (L77, L96)
 */

using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[InitializeOnLoad]
public static class ForceComponentOrder
{
    #region Private Variables
    private const int ANTI_LOCKUP_MAX_COMPONENTS = 300;
    private static GameObject _selected;
    #endregion

    #region Private Properties
    private static GameObject Selected
    {
        get
        {
            return _selected;
        }
        set
        {
            if (_selected == value) return;

            _selected = value;

            ReorderComponents(_selected);
        }
    }
    #endregion

    #region Private Methods
    static ForceComponentOrder()
    {
        EditorApplication.update += Update;
    }

    private static void Update()
    {
        Selected = Selection.activeGameObject;
    }

    private static void ReorderComponents(GameObject selected)
    {
        if (selected == null) return;

        // get prefab instead, if available
        GameObject prefab = PrefabUtility.GetPrefabParent(selected) as GameObject;
        if (prefab != null) selected = prefab;

        // initialize priorities
        var componentOrders = new Dictionary<string, int>();

        // get valid components (skip transform and missing components)
        var components = selected.GetComponents<Component>().
            Where(c => !(c is Transform || c == null || c.GetType() == null)).
            ToArray();

        // set priorities
        foreach (var component in components)
        {
            if (component is Camera) return;

            // get order
            int? order = GetOrder(component);

            // get name
            string name = component.GetType().Name;

            // assign
            componentOrders[name] = order.GetValueOrDefault();
        }

        // move components
        bool moved;
        int moves;
        foreach (var component in components.OrderBy(c => componentOrders[c.GetType().Name]).ThenBy(c => c.GetType().Name))
        {
            // move to bottom
            moves = 0;
            do
            {
                moved = UnityEditorInternal.ComponentUtility.MoveComponentDown(component);
                moves++;
            } while (moved && moves < ANTI_LOCKUP_MAX_COMPONENTS);

            // if a gameobject has a ridiculous number of components, lets stop looping at some point
            if (moves == ANTI_LOCKUP_MAX_COMPONENTS)
            {
                Debug.LogWarning("Failed to finish moving " + component.GetType().Name + " as we reached the maximum number of moves without reaching the bottom.");
                continue;
            }
        }
    }

    private static int? GetOrder(Component component)
    {
        // physics
        if (component is Collider) return 100;
        if (component is Rigidbody) return 101;
        if (component is Joint) return 102;

        // renderers
        if (component is MeshFilter) return 200;
        if (component is Projector) return 201;
        if (component is Renderer) return 202;
        if (component is Animation) return 203;

        // lights
        if (component is Light) return 300;

        // nav mesh
        if (component is NavMeshAgent) return 400;
        if (component is NavMeshObstacle) return 401;

        // particles
        if (component is ParticleSystem) return 500;

        // environment
        if (component is Tree) return 600;

        // scripts
        if (component is MonoBehaviour)
        {
            int order = 1000;

            // custom script ordering goes here
            // if (component is SpeciallyOrderedMonoBehaviourSubclass) order += 1;

            return order;
        }

        // warning
        Debug.LogWarning("Force Component Order: " + component.GetType().Name + " component is not supported.");
        return null;
    }
    #endregion
}
