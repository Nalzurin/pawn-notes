using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace PawnNotes
{
    public class CompNote : ThingComp
    {
        public string noteText;

        public CompNote()
        {

        }
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref noteText, "noteText", string.Empty, false);
        }
    }
}
