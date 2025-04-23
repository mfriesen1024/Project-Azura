using Godot;
using KeystoneUtils.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAzura.src.Management
{
    partial class InputDebugger:Node
    {
        Logger Log = new Logger(true, true, "logs\\", "InputDebug", "log");

        public override void _UnhandledInput(InputEvent @event)
        {
            base._UnhandledInput(@event);
            if(@event is InputEventMouseButton iemb) { Log.WriteAll($"Got unhandled iemb. button:{iemb.ButtonIndex}."); }
        }

        public override void _Input(InputEvent @event)
        {
            base._Input(@event);
            if (@event is InputEventMouseButton iemb) { Log.WriteAll($"Got iemb. button:{iemb.ButtonIndex}."); }
        }
    }
}
