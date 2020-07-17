using System;
using System.Collections;
using System.Collections.Generic;
using Objectives;
using TMPro;
using UI;
using UnityEngine;

public class ObjectiveUiRenderer : MonoBehaviour {
    [SerializeField] private GameObject listItem;
    [SerializeField] private Transform listTransform;
    private ObjectiveManager manager;
    private Dictionary<Objective, GameObject> listItems;
    private void Awake() {
        manager = FindObjectOfType<ObjectiveManager>() ?? throw new Exception("Could not find manager");
        listItems = new Dictionary<Objective, GameObject>();
        ObjectiveManager.onObjectiveAdded += OnObjectiveAdded;
        ObjectiveManager.onObjectiveRemoved += OnObjectiveRemoved;
    }

    // private void Start() {
    //     foreach (var objs in manager.Objectives) Instantiate(listItem, listTransform);
    // }

    private void OnObjectiveRemoved(Objective o) {
        var go = listItems[o];
        Destroy(go);
        listItems.Remove(o);
    }
    private void OnObjectiveAdded(Objective o) {
        listItems.Add(o, Instantiate(listItem, listTransform));
        listItems[o].GetComponent<ObjectiveListItem>().SetObjective(o);
    }
}
