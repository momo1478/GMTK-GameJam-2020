using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Tasks;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests
{
    public class MoveToAreaTest
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator MoveToAreaWithEnumeratorPasses()
        {
            GameObject targetObj = new GameObject();
            targetObj.AddComponent<Target>();
            targetObj.transform.position = new Vector3(5,5,5);

            yield return null;
            
            GameObject managerObj = new GameObject();
            var manager = managerObj.AddComponent<ObjectiveManager>();
            var objective = managerObj.AddComponent<MoveToArea>();
            manager.AddTask(objective);
            managerObj.transform.position = Vector3.zero;
            
            Assert.IsFalse(objective.IsCompleted());
            Assert.AreEqual(manager.Objectives.Count, 1);
            yield return new WaitForSeconds(1f);

            managerObj.transform.position = targetObj.transform.position;
            Assert.IsTrue(objective.IsCompleted());
            yield return null;
        }
    }
}
