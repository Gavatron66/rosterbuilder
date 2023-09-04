﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Death_Guard
{
    public class Typhus : Datasheets
    {
        public Typhus()
        {
            DEFAULT_POINTS = 165;
            Points = 165;
            UnitSize = 1;
            TemplateCode = "ncp";
            Keywords.AddRange(new string[]
            {
                "CHAOS", "NURGLE", "HERETIC ASTARTES", "DEATH GUARD", "HARBINGERS",
                "INFANTRY", "CHARACTER", "PSYKER", "BUBONIC ASTARTES", "TERMINATOR", "LORD OF THE DEATH GUARD",
                    "LORD OF CONTAGION", "TYPHUS"
            });
            PsykerPowers = new string[2] { string.Empty, string.Empty };
            WarlordTrait = "Shamblerot (Contagion)";
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as DeathGuard;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            Label lblPsyker = panel.Controls["lblPsyker"] as Label;
            CheckedListBox clbPsyker = panel.Controls["clbPsyker"] as CheckedListBox;

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = true;
                cmbWarlord.Text = WarlordTrait;
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }

            lblPsyker.Text = "Select two of the following:";
            clbPsyker.ClearSelected();
            for (int i = 0; i < clbPsyker.Items.Count; i++)
            {
                clbPsyker.SetItemChecked(i, false);
            }

            if (PsykerPowers[0] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[0]), true);
            }
            if (PsykerPowers[1] != string.Empty)
            {
                clbPsyker.SetItemChecked(clbPsyker.Items.IndexOf(PsykerPowers[1]), true);
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            CheckBox isWarlord = panel.Controls["cbWarlord"] as CheckBox;
            CheckedListBox clb = panel.Controls["clbPsyker"] as CheckedListBox;
            ComboBox warlord = panel.Controls["cmbWarlord"] as ComboBox;

            switch (code)
            {
                case 25:
                    if (isWarlord.Checked)
                    {
                        this.isWarlord = true;
                        warlord.Text = WarlordTrait;
                        warlord.Enabled = false;
                    }
                    else { this.isWarlord = false; }
                    break;
                case 60:
                    if (clb.CheckedItems.Count < 2)
                    {
                        break;
                    }
                    else if (clb.CheckedItems.Count == 2)
                    {
                        PsykerPowers[0] = clb.CheckedItems[0] as string;
                        PsykerPowers[1] = clb.CheckedItems[1] as string;
                    }
                    else
                    {
                        clb.SetItemChecked(clb.SelectedIndex, false);
                    }
                    break;
                default: break;
            }

            if (code == -1)
            {
                if (this.isWarlord)
                {
                    warlord.Text = WarlordTrait;
                    warlord.Enabled = false;
                }
            }

            Points = DEFAULT_POINTS;
        }

        public override string ToString()
        {
            return "Typhus - " + Points + "pts";
        }

        public override Datasheets CreateUnit()
        {
            return new Typhus();
        }
    }
}
