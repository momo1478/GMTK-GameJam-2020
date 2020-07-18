using Objectives;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectiveManager))]
public class ObjectiveStacker : MonoBehaviour
{
    public enum Objectives
    {
        MoveToArea,
        ActivateTarget,
        SurviveLasers,
        ActivateSafeZone
    }

    [HideInInspector] public ObjectiveManager objectiveManager;
    public float TimeToNextObjective;

    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        objectiveManager = GetComponent<ObjectiveManager>();
        AddNewObjective();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TimeToNextObjective)
        {
            AddNewObjective();
            timer = 0;
        }
    }

    void AddNewObjective()
    {
        Array values = Objectives.GetValues(typeof(Objectives));
        // Objectives randomObjective = (Objectives)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        var randomObjective = Objectives.MoveToArea;
        switch (randomObjective)
        {
            case Objectives.MoveToArea:
                objectiveManager.AddObjective(gameObject.AddComponent<MoveToArea>());
                break;
            case Objectives.ActivateTarget:
                objectiveManager.AddObjective(gameObject.AddComponent<ActivateTarget>());
                break;
            case Objectives.SurviveLasers:
                objectiveManager.AddObjective(gameObject.AddComponent<SurviveLasers>());
                break;
            case Objectives.ActivateSafeZone:
                objectiveManager.AddObjective(gameObject.AddComponent<ActivateSafeZone>());
                break;
        }
    }
}