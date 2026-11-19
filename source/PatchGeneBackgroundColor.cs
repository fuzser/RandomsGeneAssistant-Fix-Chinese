using System;
using System.Collections.Generic;
using Verse;
using HarmonyLib;
using RimWorld;
using UnityEngine;

namespace RandomsGeneAssistant
{
    [HarmonyPatch]
    public static class PatchGeneBackgroundColor
    {
#if !RIMWORLD_1_7
        [HarmonyPatch(typeof(GeneUIUtility), nameof(GeneUIUtility.DrawGene))]
        [HarmonyPrefix]
        public static void DrawGene_Prefix(Gene gene, Rect geneRect, GeneType geneType, ref bool doBackground, bool clickable = true)
        {
            //  Early returns
            if (!doBackground) { return; }
            Map map = Find.CurrentMap;
            if (map == null || !map.IsPlayerHome) { return; }

            //  Get genebanks
            List<Thing> banks = new List<Thing>(map.listerThings.ThingsMatching(ThingRequest.ForGroup(ThingRequestGroup.GenepackHolder)));
            if ((banks == null) || (banks.Count == 0)) { return; }

            //  Init tracking vars
            int findQuality = 0;

            if (SettingsRef.ignoredGenes.Contains(gene.def))
            {
                findQuality = -1;
            }
            else
            {
                //  Loop through buildings
                foreach (Thing b in banks)
                {
                    //  Get the genepacks
                    CompGenepackContainer comp = b.TryGetComp<CompGenepackContainer>();
                    foreach (Genepack gp in comp.ContainedGenepacks)
                    {
                        //  Scan each gene
                        foreach (GeneDef g in gp.GeneSet.GenesListForReading)
                        {
                            if (g == gene.def)
                            {
                                findQuality = gp.GeneSet.GenesListForReading.Count == 1 ? 2 : 1;
                            }

                            if (findQuality > 0)
                            {
                                break;
                            }
                        }
                        if (findQuality == 2)
                        {
                            break;
                        }
                    }
                    if (findQuality == 2)
                    {
                        break;
                    }
                }
            }

            //  Draw our own background
            GUI.BeginGroup(geneRect);
            Rect rect1 = geneRect.AtZero();
            if (doBackground)
            {
                Color c = SettingsRef.red;
                if (findQuality == 1)
                {
                    c = SettingsRef.yellow;
                }
                else if (findQuality == 2)
                {
                    c = SettingsRef.green;
                }
                else if (findQuality == -1)
                {
                    c = SettingsRef.gray;
                }
                Widgets.DrawBoxSolid(rect1, c);
                GUI.color = new Color(1f, 1f, 1f, 0.05f);
                Widgets.DrawBox(rect1);
                GUI.color = Color.white;
            }
            GUI.EndGroup();

            //  Override the previous background
            doBackground = false;
        }
#endif

        [HarmonyPatch(typeof(GeneUIUtility), "DrawGeneBasics")]
        [HarmonyPrefix]
#if RIMWORLD_1_7
        public static void DrawGeneBasics_Prefix(GeneDef gene, Rect geneRect, GeneType geneType, ref bool doBackground, bool clickable, bool overridden)
#else
        public static void DrawGeneBasics_Prefix(Gene gene, Rect geneRect, GeneType geneType, ref bool doBackground, bool clickable, bool overridden)
#endif
        {
#if RIMWORLD_1_7
            GeneDef geneDef = gene;
#else
            GeneDef geneDef = gene.def;
#endif

            // RimWorld 1.7 passes doBackground=false for genes in the xenogerm
            // selection panel. Those entries still need the ownership colors.
#if !RIMWORLD_1_7
            if (!doBackground) { return; }
            bool drawOwnershipBackground = doBackground;
#else
            bool drawOwnershipBackground = true;
#endif

            //  Early returns
            Map map = Find.CurrentMap;
            if (map == null || !map.IsPlayerHome) { return; }

            //  Get genebanks
            List<Thing> banks = new List<Thing>(map.listerThings.ThingsMatching(ThingRequest.ForGroup(ThingRequestGroup.GenepackHolder)));
            if ((banks == null) || (banks.Count == 0)) { return; }

            //  Init tracking vars
            int findQuality = 0;

            if (SettingsRef.ignoredGenes.Contains(geneDef))
            {
                findQuality = -1;
            }
            else
            {
                //  Loop through buildings
                foreach (Thing b in banks)
                {
                    //  Get the genepacks
                    CompGenepackContainer comp = b.TryGetComp<CompGenepackContainer>();
                    foreach (Genepack gp in comp.ContainedGenepacks)
                    {
                        //  Scan each gene
                        foreach (GeneDef g in gp.GeneSet.GenesListForReading)
                        {
                            if (g == geneDef)
                            {
                                findQuality = gp.GeneSet.GenesListForReading.Count == 1 ? 2 : 1;
                            }

                            if (findQuality > 0)
                            {
                                break;
                            }
                        }
                        if (findQuality == 2)
                        {
                            break;
                        }
                    }
                    if (findQuality == 2)
                    {
                        break;
                    }
                }
            }

            //  Draw our own background
            GUI.BeginGroup(geneRect);
            Rect rect1 = geneRect.AtZero();
            if (drawOwnershipBackground)
            {
                Color c = SettingsRef.red;
                if (findQuality == 1)
                {
                    c = SettingsRef.yellow;
                }
                else if (findQuality == 2)
                {
                    c = SettingsRef.green;
                }
                else if (findQuality == -1)
                {
                    c = SettingsRef.gray;
                }
                Widgets.DrawBoxSolid(rect1, c);
                GUI.color = new Color(1f, 1f, 1f, 0.05f);
                Widgets.DrawBox(rect1);
                GUI.color = Color.white;
            }
            GUI.EndGroup();

            //  Override the previous background
            doBackground = false;
        }
    }
}
