﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Grey_Knights
{
    public class GKRazorback : Datasheets
    {
        public GKRazorback()
        {
            DEFAULT_POINTS = 100;
            UnitSize = 1;
            Points = UnitSize * DEFAULT_POINTS;
            TemplateCode = "1m2k";
            Weapons.Add("Twin Heavy Bolter");
            Weapons.Add("");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "IMPERIUM", "SANCTIC ASTARTES", "GREY KNIGHTS", "<BROTHERHOOD>",
                "VEHICLE", "TRANSPORT", "SMOKESCREEN", "RAZORBACK",
            });
            Role = "Transport";
        }

        public override Datasheets CreateUnit()
        {
            return new GKRazorback();
        }
        public override void LoadDatasheets(Panel panel, Faction f)
        {
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {
                "Twin Assault Cannon",
                "Twin Heavy Bolter",
                "Twin Lascannon"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Hunter-killer Missile (+5 pts)";
            if (Weapons[1] != string.Empty)
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            cbOption2.Text = "Storm Bolter";
            if (Weapons[2] != string.Empty)
            {
                cbOption2.Checked = true;
            }
            else
            {
                cbOption2.Checked = false;
            }
        }

        public override void SaveDatasheets(int code, Panel panel)
        {
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbOption2 = panel.Controls["cbOption2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else { Weapons[1] = string.Empty; }
                    break;
                case 22:
                    if (cbOption2.Checked)
                    {
                        Weapons[2] = cbOption2.Text;
                    }
                    else { Weapons[2] = string.Empty; }
                    break;
            }

            Points = DEFAULT_POINTS;

            if (Weapons.Contains("Hunter-killer Missile (+5 pts)"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Razorback - " + Points + "pts";
        }
    }
}
