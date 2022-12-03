using Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace EditorExtensions
{
    
    /*
     * use [SerializeReference,SelectableImpl] on filed definition to use the drawer, like this:
     * [SerializeReference,SelectableImpl] private IFloatValueProvider source;
     * property drawer will automatically find all types that implement your interface
     * and will provide UI dropdown, where you can select concrete type that you want to use.
     */
    [CustomPropertyDrawer(typeof(SelectableImplAttribute), true)]
    public class InterfaceWithSerializableContentDrawer:PropertyDrawer
    {
     
        /**
         * extracts object from the property.
         * don't ask how this works, I've nicked it from some forum post
         */
        private static object GetTargetObjectOfProperty(SerializedProperty prop)
        {
            if (prop == null) return null;

            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }
            return obj;
        }

        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }
            return null;
        }
        
        
        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
            if (enumerable == null) return null;
            var enm = enumerable.GetEnumerator();

            for (var i = 0; i <= index; i++)
            {
                if (!enm.MoveNext()) return null;
            }
            return enm.Current;
        }

        //find all types inherited from the baseType (skipping generics, abstracts, interfaces, and non serializable types)
        private static Type[] GetTypes(Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => 
                    !p.IsAbstract 
                    && !p.IsInterface
                    && !p.ContainsGenericParameters
                    && p.IsSerializable
                    && baseType.IsAssignableFrom(p) 
                    ).ToArray();
        }

        private static readonly Dictionary<string, Type[]> TypeCache = new();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 
                2*EditorGUIUtility.singleLineHeight
                +3*EditorGUIUtility.standardVerticalSpacing
                +EditorGUI.GetPropertyHeight(property, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rect = new Rect(position)
            {
                height = EditorGUIUtility.singleLineHeight
            };
            EditorGUI.LabelField(rect,label);
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            var rect2 = new Rect(position)
            {
                height = EditorGUIUtility.singleLineHeight
            };

            /*
             * for some reason returned type name has some strange formatting, so we have to split it and reorder parts
             * to actually get usable type name
             */
            var parts = property.managedReferenceFieldTypename.Split(" ");
            var typeName = parts[1] + "," + parts[0];
            //now we have to find all types that are assignable to baseType and get their names
            Type[] types;
            if(TypeCache.ContainsKey(typeName))
            {
                //if we have already cached this type, we can use the cached version
                types = TypeCache[typeName];
            }
            else
            {
                //if we haven't cached the types for this interface yet, find them and do so
                types = GetTypes(Type.GetType(typeName,true));
                TypeCache.Add(typeName, types);
            }
            var typeNames = types.Select(t => t.Name).ToArray();
            var obj = GetTargetObjectOfProperty(property) ?? CreateInstance(property,0,types);
            var index = Array.IndexOf(typeNames, obj.GetType().Name);
            //list available types for the interface in popup
            var newIndex = EditorGUI.Popup(rect2, index, typeNames);
            if(newIndex != index)
            {
               CreateInstance(property,newIndex,types);
            }
            EditorGUI.PropertyField(position, property, GUIContent.none, true);
        }

        private object CreateInstance(SerializedProperty property, int typeIndex,Type[] types)
        {
            var target = Activator.CreateInstance(types[typeIndex]);
            property.managedReferenceValue = target;
            property.serializedObject.ApplyModifiedProperties();
            return target;
        }
    }
}