﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class NoxiousBlightbringer : Datasheets
    {
        public NoxiousBlightbringer()
        {
            DEFAULT_POINTS = 55;
            UnitSize = 1;
            TemplateCode = "c";
            Points = DEFAULT_POINTS;
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "<PLAGUE COMPANY>",
                "INFANTRY", "CHARACTER", "BUBONIC ASTARTES", "FOETID VIRION", "NOXIOUS BLIGHTBRINGER"
            });
            Role = "Elites";
        }
        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as DeathGuard;
            factionsRestrictions = repo.restrictedItems;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmb = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cb = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbFaction = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            cmb.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("");
            foreach (var item in traits)
            {
                cmb.Items.Add(item);
            }


            if (isWarlord)
            {
                cb.Checked = true;
                cmb.Enabled = true;
                cmb.SelectedText = WarlordTrait;
            }
            else
            {
                cb.Checked = false;
                cmb.Enabled = false;
            }

            restrictedIndexes = new List<int>();
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (repo.restrictedItems.Contains(cmb.Items[i]) && WarlordTrait != cmb.Items[i].ToString())
                {
                    restrictedIndexes.Add(i);
                }
            }
            this.DrawItemWithRestrictions(restrictedIndexes, cmb);

            cmbFaction.Items.Clear();
            cmbFaction.Items.AddRange(repo.GetFactionUpgrades(Keywords).ToArray());

            if (Factionupgrade != null)
            {
                cmbFaction.SelectedIndex = cmbFaction.Items.IndexOf(Factionupgrade);
            }
            else
            {
                cmbFaction.SelectedIndex = 0;
            }

            restrictedIndexes = new List<int>();
            for (int i = 0; i < cmbFaction.Items.Count; i++)
            {
                if (repo.restrictedItems.Contains(cmbFaction.Items[i]) && Factionupgrade != cmbFaction.Items[i].ToString())
                {
                    restrictedIndexes.Add(i);
                }
            }
            this.DrawItemWithRestrictions(restrictedIndexes, cmbFaction);

            cmbRelic.Items.Clear();
            cmbRelic.Items.AddRange(repo.GetRelics(Keywords).ToArray());

            if (Relic != null)
            {
                cmbRelic.SelectedIndex = cmbRelic.Items.IndexOf(Relic);
            }
            else
            {
                cmbRelic.SelectedIndex = -1;
            }

            restrictedIndexes = new List<int>();
            for (int i = 0; i < cmbRelic.Items.Count; i++)
            {
                if (repo.restrictedItems.Contains(cmbRelic.Items[i]) && Relic != cmbRelic.Items[i].ToString())
                {
                    restrictedIndexes.Add(i);
                }
            }
            this.DrawItemWithRestrictions(restrictedIndexes, cmbRelic);

            panel.Controls["lblFactionupgrade"].Visible = true;
            panel.Controls["cmbFactionupgrade"].Visible = true;

            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            if (Stratagem.Contains(cbStratagem1.Text))
            {
                cbStratagem1.Checked = true;
                cbStratagem1.Enabled = true;
            }
            else
            {
                cbStratagem1.Checked = false;
                cbStratagem1.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem1.Text));
            }

            if (Stratagem.Contains(cbStratagem2.Text))
            {
                cbStratagem2.Checked = true;
                cbStratagem2.Enabled = true;
            }
            else
            {
                cbStratagem2.Checked = false;
                cbStratagem2.Enabled = repo.GetIfEnabled(repo.StratagemList.IndexOf(cbStratagem2.Text));
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox isWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox warlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox factionud = panel.Controls["cmbFactionupgrade"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 25:
                    if (isWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; warlord.SelectedIndex = -1; }
                    break;
                case 15:
                    if (!factionsRestrictions.Contains(warlord.Text))
                    {
                        if (WarlordTrait == "")
                        {
                            WarlordTrait = warlord.Text;
                            if (WarlordTrait != "")
                            {
                                repo.restrictedItems.Add(WarlordTrait);
                            }
                        }
                        else
                        {
                            repo.restrictedItems.Remove(WarlordTrait);
                            WarlordTrait = warlord.Text;
                            if (WarlordTrait != "")
                            {
                                repo.restrictedItems.Add(WarlordTrait);
                            }
                        }
                    }
                    else
                    {
                        if (WarlordTrait == "")
                        {
                            warlord.SelectedIndex = -1;
                        }
                    }
                    break;
                case 16:
                    if (!factionsRestrictions.Contains(factionud.Text))
                    {
                        if (Factionupgrade == "(None)")
                        {
                            Factionupgrade = factionud.Text;
                            if (Factionupgrade != "(None)")
                            {
                                repo.restrictedItems.Add(Factionupgrade);
                            }
                        }
                        else
                        {
                            repo.restrictedItems.Remove(Factionupgrade);
                            Factionupgrade = factionud.Text;
                            if (Factionupgrade != "(None)")
                            {
                                repo.restrictedItems.Add(Factionupgrade);
                            }
                        }
                    }
                    else
                    {
                        if (Factionupgrade == "(None)")
                        {
                            factionud.SelectedIndex = 0;
                        }
                    }
                    break;
                case 17:
                    if (!factionsRestrictions.Contains(cmbRelic.Text))
                    {
                        if (Relic == "(None)")
                        {
                            Relic = cmbRelic.Text;
                            if (Relic != "(None)")
                            {
                                repo.restrictedItems.Add(Relic);
                            }
                        }
                        else
                        {
                            repo.restrictedItems.Remove(Relic);
                            Relic = cmbRelic.Text;
                            if (Relic != "(None)")
                            {
                                repo.restrictedItems.Add(Relic);
                            }
                        }
                    }
                    else
                    {
                        if (Relic == "(None)")
                        {
                            cmbRelic.SelectedIndex = 0;
                        }
                    }
                    break;
                case 71:
                    if (cbStratagem1.Checked)
                    {
                        Stratagem.Add(cbStratagem1.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem1.Text))
                        {
                            Stratagem.Remove(cbStratagem1.Text);
                        }
                    }
                    break;
                case 72:
                    if (cbStratagem2.Checked)
                    {
                        Stratagem.Add(cbStratagem2.Text);
                    }
                    else
                    {
                        if (Stratagem.Contains(cbStratagem2.Text))
                        {
                            Stratagem.Remove(cbStratagem2.Text);
                        }
                    }
                    break;
            }

            Points = DEFAULT_POINTS;

            Points += repo.GetFactionUpgradePoints(Factionupgrade);
        }


        public override string ToString()
        {
            return "Noxious Blightbringer - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new NoxiousBlightbringer();
        }
    }
}
