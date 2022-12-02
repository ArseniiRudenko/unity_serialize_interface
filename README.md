# unity serialize interface
Drawer class that allows to use interfaces as monobehaviour fileds in Unity and select implementation from inspector ui.

## How to use

To use your custom ```ISomeInterface``` add ```[CustomPropertyDrawer(typeof(ISomeInterface), true)]``` to the InterfaceWithSerializableContentDrawer.cs
and declare your interface as ```[SerializeReference] private ISomeInterface someName```. Make sure that all implementations of your interface are ```[Serializable]```

I've provided some examples that you can try this on, just copy Scripts folder into your project Assets folder, and you will have MbExample monobehavior that you can attach to something. It will have 3 fields that are all interfaces with multiple implementations that you can select from ui.

## Drawer will generate dropdown with available implementations for your interface.

![Selection example](Screenshots/Mb_example_editor_dropdown.png?raw=true "Ui will look something like this")


## UI should look something like this.

![Ui Example](Screenshots/Mb_example_editor.png?raw=true "Ui will look something like this")

As you can see interfaces can also be nested within implementations of other interfaces.

## Contributions.

Generated ui layout is kinda meh, so if you want to improve it - be my guest. 


