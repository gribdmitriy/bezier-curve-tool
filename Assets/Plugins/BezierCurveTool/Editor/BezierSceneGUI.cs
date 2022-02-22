﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BezierCurveTool.Editor
{
   
    [CustomEditor(typeof(BezierCurve))]
    public class BezierSceneGUI : UnityEditor.Editor 
    {
        [MenuItem("GameObject/Create Other/Bezier Curve")]
        static void CreateBezierCurve()
        {
            var gameObject = new GameObject
            {
                name = "Bezier Curve", 
                transform = { position = Vector3.zero }
            };
            gameObject.AddComponent<BezierCurve>();
            gameObject.GetComponent<BezierCurve>().arcs.Add(new Point(true, false, 
                new Vector3(-1, 0, 0), new List<Vector3>{new Vector3(-0.5f, 0, 1)}, true));
            gameObject.GetComponent<BezierCurve>().arcs.Add(new Point(false, true, 
                new Vector3(1, 0, 0), new List<Vector3>{new Vector3(0.5f, 0, -1)}, true));
        }
        
        public override void OnInspectorGUI()
        {
            
            var script = (BezierCurve) target;

            if (script.arcs.Count == 0) EditorGUILayout.LabelField("Points is empty");
/*
            var i = 0;
            foreach (var arc in script.arcs)
            {
                if (arc.isFirstPoint)
                {
                    arc.position = EditorGUILayout.Vector3Field("Point" + i, arc.position);
                    arc.handles[0] = EditorGUILayout.Vector3Field("Handle", arc.handles[0]);
                }

                if (arc.isLastPoint)
                {
                    arc.position = EditorGUILayout.Vector3Field("Point" + i, arc.position);
                    arc.handles[0] = EditorGUILayout.Vector3Field("Handle", arc.handles[0]);
                }

                if (!arc.isFirstPoint && !arc.isLastPoint)
                {
                    arc.position = EditorGUILayout.Vector3Field("Point" + i, arc.position);
                    arc.handles[0] = EditorGUILayout.Vector3Field("1 handle", arc.handles[0]);
                    arc.handles[1] = EditorGUILayout.Vector3Field("2 handle", arc.handles[1]);
                }

                i++;
            }*/

            /*if (GUILayout.Button("Add point"))
            {
                var last = script.arcs[script.arcs.Count - 1];
                if (last.isUpArc)
                {
                    script.arcs.Add(new Point(false, true, 
                        new Vector3(last.position.x + 2, last.position.y, last.position.z), 
                        new List<Vector3>{last.handles}, !last.isUpArc));
                }
                else
                {
                    
                }
                
                foreach (var point in script.arcs)
                {
                    if (point.isFirstPoint) continue;
                    if (point.isLastPoint)
                    {
                        
                    }
                }
            }*/
        }
        
        void OnSceneGUI() 
        {
            var script = (BezierCurve) target;
            var points = script.arcs;
            
            for (var i = 0; i < points.Count - 1; i++)
            {
                if (points[i].isFirstPoint && points.Count == 2)
                {
                    Handles.PositionHandle(points[i].position, Quaternion.identity);
                    Handles.PositionHandle(points[i].handles[0], Quaternion.identity);
                    Handles.PositionHandle(points[i + 1].position, Quaternion.identity);
                    Handles.PositionHandle(points[i + 1].handles[0], Quaternion.identity);
                    break;
                }

                if (points[i].isFirstPoint && points.Count != 2)
                {
                    Handles.PositionHandle(points[i].position, Quaternion.identity);
                    Handles.PositionHandle(points[i].handles[0], Quaternion.identity);
                    Handles.PositionHandle(points[i + 1].position, Quaternion.identity);
                    Handles.PositionHandle(points[i + 1].handles[0], Quaternion.identity);
                }
                
                if (!points[i].isFirstPoint && points.Count != 2)
                {
                    Handles.PositionHandle(points[i].position, Quaternion.identity);
                    Handles.PositionHandle(points[i].handles[1], Quaternion.identity);
                    Handles.PositionHandle(points[i + 1].position, Quaternion.identity);
                    Handles.PositionHandle(points[i + 1].handles[0], Quaternion.identity);
                }
            }
            
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (points[i].isFirstPoint && points.Count == 2)
                {
                    Handles.DrawBezier(points[i].position, points[i + 1].position, points[i].handles[0], 
                        points[i + 1].handles[0], Color.red, null, 5);
                        
                    break;
                }
                    
                if (points[i].isFirstPoint && points.Count != 2)
                {
                    Handles.DrawBezier(points[i].position, points[i + 1].position, points[i].handles[0], 
                        points[i + 1].handles[0], Color.red, null, 5);
                }
                    
                if (!points[i].isFirstPoint && points.Count != 2)
                {
                    Handles.DrawBezier(points[i].position, points[i + 1].position, points[i].handles[1], 
                        points[i + 1].handles[0], Color.red, null, 5);
                }
            }
        }
    }
}