# unity serialize interface
Small adhoc class allowing to use interfaces as types and select implementation from ui.

To use add ``` [CustomPropertyDrawer(typeof(ISomeInterface), true)]``` to the InterfaceWithSerializableContentDrawer.cs
and use your interface with ```[SerializeReference] private ISomeInterface someName```.

I've provided some examples that you can try this on, just copy Scripts folder into your project Assets folder, and you will have MbExample monobehavior that you can attach to something. It will have 3 fields that are all interfaces with multiple implementations that you can select from ui.

Interfaces can also be nested.
![Ui Example](Screenshots/Mb_example_editor.png?raw=true "Ui will look something like this")

If everything works as expected, drawer will generate dropdown with available implementations.
![Selection example](Screenshots/Mb_example_editor_dropdown.png?raw=true "Ui will look something like this")

