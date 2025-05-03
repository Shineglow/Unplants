using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Example
{
    [BindingTarget] private int markedField;
    private int unmarkedField;
}

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class BindingTargetAttribute : Attribute { }

public static class AttributeChecker
{
    public static void CheckFields()
    {
        var type = typeof(Example);

        // Получаем все поля экземпляра (public и private)
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var field in fields)
        {
            if (field.IsDefined(typeof(BindingTargetAttribute), inherit: false))
            {
                Debug.Log($"Поле {field.Name} помечено атрибутом BindingTarget.");
            }
            else
            {
                Debug.Log($"Поле {field.Name} — без атрибута.");
            }
        }
    }
}

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AttributeChecker.CheckFields();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
