using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse.AI;
using Verse;
using HarmonyLib;
using System.Reflection.Emit;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using VanillaMemesExpanded;

namespace PawnNotes
{

    [StaticConstructorOnStartup]
    public static class NoteDialogHelper
    {
        public static readonly Texture2D NoteIcon = ContentFinder<Texture2D>.Get("UI/Buttons/Pawn_Note_Icon");

        public static void DoDialogButton(Rect rect, Pawn pawn)
        {
            if (pawn == null || !pawn.def.HasComp<CompNote>())
            {
                return;
            }
            CompNote comp = pawn.GetComp<CompNote>();
            Rect rect5 = new Rect(rect.width - 42, 0f, 18f, 24f);
            if (Widgets.ButtonImage(rect5, NoteIcon))
            {
                Find.WindowStack.Add(new Dialog_EditPawnNote(comp));
            }
            TooltipHandler.TipRegion(rect5, comp.noteText.NullOrEmpty()? "NoteTip".Translate() : comp.noteText + "\n\n" + "NoteTipParenthesis".Translate());
        }
    }

    [HarmonyPatch]
    public static class PawnLogWindowNoteButtonPatch
    {

        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(ITab_Pawn_Log), "FillTab");
        }
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            int k = 0;
            bool edit = false;
            foreach (CodeInstruction instr in instructions)
            {
                if (edit)
                {
                    edit = false;
                    yield return new CodeInstruction(OpCodes.Ldloc_1);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ITab_Pawn_Log), "get_SelPawnForCombatInfo"));
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(NoteDialogHelper), nameof(NoteDialogHelper.DoDialogButton)));
                }
                if (instr.opcode == OpCodes.Call && instr.Calls(AccessTools.Method(typeof(Rect), "set_yMin")) && instructions.ElementAt(k - 1).opcode == OpCodes.Ldc_R4)
                {
                    edit = true;
                }
                k++;
                yield return instr;
            }
        }

    }
}
