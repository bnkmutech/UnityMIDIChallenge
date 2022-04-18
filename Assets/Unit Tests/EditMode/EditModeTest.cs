using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EditModeTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void KeyCodeCheck()
    {
        var settings = new Settings();
        settings.DefaultKeyCode();
        Assert.AreEqual(KeyCode.A, Settings.keyCodeDict[1]);

        // Use the Assert class to test conditions
    }

  
}
