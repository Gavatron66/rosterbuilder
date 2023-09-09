﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roster_Builder.Necrons
{
    public class Overlord : Datasheets
    {
        public Overlord()
        {
            DEFAULT_POINTS = 95;
            TemplateCode = "1m1k_c";
            Points = DEFAULT_POINTS;
            Weapons.Add("Tachyon Arrow and Hyperphaise Glaive");
            Weapons.Add("");
            Keywords.AddRange(new string[]
            {
                "NECRONS", "<DYNASTY>",
                "INFANTRY", "CHARACTER", "NOBLE", "OVERLORD"
            });
        }

        public override Datasheets CreateUnit()
        {
            return new Overlord();
        }

        public override void LoadDatasheets(Panel panel, Faction f)
        {
            repo = f as Necrons;
            Template.LoadTemplate(TemplateCode, panel);

            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;

            cmbWarlord.Items.Clear();
            List<string> traits = repo.GetWarlordTraits("");
            foreach (var item in traits)
            {
                cmbWarlord.Items.Add(item);
            }


            cmbOption1.Items.Clear();
            cmbOption1.Items.AddRange(new string[]
            {   
                "Hyperphase Sword",
                "Staff of Light",
                "Tachyon Arrow and Hyperphaise Glaive",
                "Voidblade",
                "Voidscythe",
                "Warscythe"
            });
            cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf(Weapons[0]);

            cbOption1.Text = "Resurrection Orb";
            if (Weapons[1] == "Resurrection Orb")
            {
                cbOption1.Checked = true;
            }
            else
            {
                cbOption1.Checked = false;
            }

            if (isWarlord)
            {
                cbWarlord.Checked = true;
                cmbWarlord.Enabled = true;
                cmbWarlord.SelectedIndex = cmbWarlord.Items.IndexOf(WarlordTrait);
            }
            else
            {
                cbWarlord.Checked = false;
                cmbWarlord.Enabled = false;
            }

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
            ComboBox cmbOption1 = panel.Controls["cmbOption1"] as ComboBox;
            CheckBox cbOption1 = panel.Controls["cbOption1"] as CheckBox;
            CheckBox cbWarlord = panel.Controls["cbWarlord"] as CheckBox;
            ComboBox cmbWarlord = panel.Controls["cmbWarlord"] as ComboBox;
            ComboBox cmbRelic = panel.Controls["cmbRelic"] as ComboBox;
            CheckBox cbStratagem1 = panel.Controls["cbStratagem1"] as CheckBox;
            CheckBox cbStratagem2 = panel.Controls["cbStratagem2"] as CheckBox;

            switch (code)
            {
                case 11:
                    Weapons[0] = cmbOption1.SelectedItem.ToString();

                    if(cmbOption1.SelectedItem.ToString() == "Tachyon Arrow and Hyperphaise Glaive"
                        || cmbRelic.SelectedItem.ToString() == "Orb of Eternity")
                    {
                        cbOption1.Enabled = false;
                    }
                    else
                    {
                        cbOption1.Enabled = true;
                    }
                    break;
                case 15:
                    if (cmbWarlord.SelectedIndex != -1)
                    {
                        WarlordTrait = cmbWarlord.SelectedItem.ToString();
                    }
                    else
                    {
                        WarlordTrait = string.Empty;
                    }
                    break;
                case 17:
                    Relic = cmbRelic.SelectedItem.ToString();

                    if(cmbRelic.SelectedItem.ToString() == "Blood Scythe")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Warscythe");
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }

                    if (cmbRelic.SelectedItem.ToString() == "Solar Staff")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Staff of Light");
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }

                    if (cmbRelic.SelectedItem.ToString() == "The Arrow of Infinity")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Tachyon Arrow and Hyperphaise Glaive");
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }

                    if (cmbRelic.SelectedItem.ToString() == "Orb of Eternity")
                    {
                        cbOption1.Checked = true;
                        cbOption1.Enabled = false;
                        cmbOption1.Items.Remove("Tachyon Arrow and Hyperphaise Glaive");
                    }
                    else
                    {
                        cbOption1.Enabled = true;
                        if(!Weapons.Contains("Tachyon Arrow and Hyperphaise Glaive"))
                        {
                            cmbOption1.Items.Insert(2, "Tachyon Arrow and Hyperphaise Glaive");
                        }
                    }

                    if (cmbRelic.SelectedItem.ToString() == "Voidreaper")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Warscythe");
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }

                    if (cmbRelic.SelectedItem.ToString() == "Voltaic Staff")
                    {
                        cmbOption1.SelectedIndex = cmbOption1.Items.IndexOf("Staff of Light");
                        cmbOption1.Enabled = false;
                    }
                    else
                    {
                        cmbOption1.Enabled = true;
                    }
                    break;
                case 21:
                    if (cbOption1.Checked)
                    {
                        Weapons[1] = cbOption1.Text;
                    }
                    else
                    {
                        Weapons[1] = "";
                    }
                    break;
                case 25:
                    if (cbWarlord.Checked)
                    {
                        this.isWarlord = true;
                    }
                    else { this.isWarlord = false; }
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

            if (Weapons.Contains("Warscythe"))
            {
                Points += 5;
            }

            if (Weapons.Contains("Voidscythe"))
            {
                Points += 15;
            }

            if (Weapons.Contains("Resurrection Orb"))
            {
                Points += 30;
            }

            if(Weapons.Contains("Tachyon Arrow and Hyperphaise Glaive"))
            {
                Points += 5;
            }
        }

        public override string ToString()
        {
            return "Overlord - " + Points + "pts";
        }
    }
}
