# unity serialize interface
Small adhoc class allowing to use interfaces as types and select implementation from ui.

To use add ``` [CustomPropertyDrawer(typeof(ISomeInterface), true)]``` to the InterfaceWithSerializableContentDrawer.cs
and use your interface with ```[SerializeReference] private ISomeInterface someName```.
