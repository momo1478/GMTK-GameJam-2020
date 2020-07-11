using System;
using System.Collections;
using System.Collections.Generic;
using Tasks;
using UnityEngine;

public class ObjectiveUiRenderer : MonoBehaviour {
    [SerializeField] private GameObject listItem;
    [SerializeField] private Transform listTransform;
    private ObjectiveManager manager;
    private void Awake() {
        manager = FindObjectOfType<ObjectiveManager>() ?? throw new Exception("Could not find manager");
    }

    private void Start() {
        foreach (var objs in manager.Objectives) Instantiate(listItem, listTransform);
    }
}
