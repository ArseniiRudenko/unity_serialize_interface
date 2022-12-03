# unity serializable interface drawer
Drawer class that allows to use interfaces as monobehaviour fields in Unity and select implementation from inspector ui.

## How to use

Just copy Scripts folder into your project Assets folder.

To use this drawer on your custom ```ISomeInterface``` declare your interface as ```[SerializeReference,SelectableImpl]  private ISomeInterface someName```.
Only non-generic non-abstract implementations of your interface that are ```[Serializable]``` will be avalable for selection.

Repo contains some examples that you can try, once you imported the scripts you will have MbExample monobehavior that you can attach to something. It will have 3 fields that are all interfaces with multiple implementations that you can select from ui.

## Drawer will generate dropdown with available implementations for your interface.

![Selection example](Screenshots/Mb_example_editor_dropdown.png?raw=true "Ui will look something like this")


## UI should look something like this.

![Ui Example](Screenshots/Mb_example_editor.png?raw=true "Ui will look something like this")

As you can see interfaces can also be nested within implementations of other interfaces.

## Contributions.

Generated ui layout is kinda meh, so if you want to improve it - be my guest. 


