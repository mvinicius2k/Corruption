using Flax.Build;

public class GameEditorTarget : GameProjectEditorTarget
{
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();

        // Reference the modules for editor


        Modules.Add("EditorPlus");
        Modules.Add("CustomEditors");
        Modules.Add("Game");
        //Modules.Add("Editors");
    }
}
