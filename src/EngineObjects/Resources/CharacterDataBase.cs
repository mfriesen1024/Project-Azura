using Godot;

namespace ProjectAzura.src.EngineObjects.Resources
{
    [GlobalClass]
    internal abstract partial class CharacterDataBase : Resource
    {
        //[Export] 
        string[,] dialogue;
        protected string[][] FormattedDialogue { get => FormatDialogue(dialogue); }

        string[][] FormatDialogue(string[,] dialogue)
        {
            int ol = dialogue.GetLength(0);
            string[][] o = new string[ol][];
            for (int oi = 0; oi < ol; oi++)
            {
                int il = dialogue.GetLength(1);
                string[] i = new string[il];
                for (int ii = 0; ii < il; ii++)
                {
                    i[ii] = dialogue[oi, ii];
                }
                o[oi] = i;
            }
            return o;
        }
    }
}