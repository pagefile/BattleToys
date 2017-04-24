using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IObjectiveMessageTarget : IEventSystemHandler
{
    void ObjectivePassed(string message);
    void ObjectiveFailed(string message);
}
