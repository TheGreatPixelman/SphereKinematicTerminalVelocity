using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**
* @author Charles Lemaire, 2021
* @date - 2021-08-04
* 
* @Github https://github.com/TheGreatPixelman
*/

[CustomEditor(typeof(KinematicTerminalVelocity))]
public class KinematicSphereEditor : Editor
{
    KinematicTerminalVelocity sphere;

    void OnEnable()
    {
        sphere = (KinematicTerminalVelocity)target;
    }

    public override void OnInspectorGUI()
    {
        ShowGeneralDetails();

        switch (sphere.simulationType)
        {
            case SimulationType.AnimationCurve:
                ShowAnimationCurveDetails();
                break;
            case SimulationType.DragEquation:
                ShowDragEquationDetails();
                break;
        }
    }

    bool generalInfosToggle = false;

    void ShowGeneralDetails()
    {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("General Informations", EditorStyles.boldLabel);

        EditorGUI.BeginDisabledGroup(true);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Velocity");
        EditorGUILayout.FloatField(sphere.velocity.y);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Scale");
        EditorGUILayout.FloatField(sphere.transform.localScale.x);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Gravity");
        EditorGUILayout.FloatField(sphere.gravity.y);
        EditorGUILayout.EndHorizontal();

        EditorGUI.EndDisabledGroup();


        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("General Properties", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Terminal Velocity");
        sphere.terminalVelocity = EditorGUILayout.FloatField(sphere.terminalVelocity);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Mass");
        sphere.mass =  EditorGUILayout.FloatField(sphere.mass);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Simulation Type");
        sphere.simulationType = (SimulationType)EditorGUILayout.EnumPopup(sphere.simulationType);
        EditorGUILayout.EndHorizontal();
    }
    void ShowAnimationCurveDetails()
    {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Animation Curve Properties", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Drag Curve");
        sphere.dragCurve = EditorGUILayout.CurveField(sphere.dragCurve);
        EditorGUILayout.EndHorizontal();
    }
    void ShowDragEquationDetails()
    {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Drag Equation Properties", EditorStyles.boldLabel);

        EditorGUI.BeginDisabledGroup(true);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Drag Coefficient");
        EditorGUILayout.FloatField(sphere.CD);
        EditorGUILayout.EndHorizontal();

        EditorGUI.EndDisabledGroup();
    }
}
