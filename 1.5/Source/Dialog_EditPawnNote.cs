using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace PawnNotes
{
    public class Dialog_EditPawnNote : Window
    {
        private CompNote note;

        private string newText;

        private static readonly Vector2 ButSize = new Vector2(150f, 38f);
        public override Vector2 InitialSize => new Vector2(700f, 400f);
        public Dialog_EditPawnNote(CompNote _note)
        {
            note = _note;
            newText = note.noteText;
            absorbInputAroundWindow = true;
        }
        public override void OnAcceptKeyPressed()
        {
            Event.current.Use();
        }
        public override void DoWindowContents(Rect rect)
        {
            Text.Font = GameFont.Medium;
            Widgets.Label(new Rect(rect.x, rect.y, rect.width, 35f), "EditNote".Translate());
            Text.Font = GameFont.Small;
            float num = rect.y + 35f + 10f;
            string text = Widgets.TextArea(new Rect(rect.x, num, rect.width, rect.height - ButSize.y - 17f - num), newText);
            if (text != newText)
            {
                newText = text;
            }
            if (Widgets.ButtonText(new Rect(0f, rect.height - ButSize.y, ButSize.x, ButSize.y), "Cancel".Translate()))
            {
                Close();
            }
            if (Widgets.ButtonText(new Rect(rect.width - ButSize.x, rect.height - ButSize.y, ButSize.x, ButSize.y), "DoneButton".Translate()))
            {
                ApplyChanges();
            }
        }

        private void ApplyChanges()
        {
            if (note.noteText != newText)
            {
                note.noteText = newText;
            }
            Close();
        }

    }
}
