using System;

namespace ProjectAzura.src.Character
{
    /// <summary>
    /// Base class for characters with dialogue options.
    /// </summary>
    // Due to characters not being units, we need to create them here, separate from the RPGSystem.
    // Wait, should this be an interface? idk.
    public abstract class CharacterBase
    {
        protected string[][] dialogue;

        public virtual void PlayDialogue(int index)
        {
            throw new NotImplementedException();
        }
    }
}