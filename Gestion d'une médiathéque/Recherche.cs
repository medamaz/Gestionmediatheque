using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_d_une_médiathéque
{
    public partial class Recherche : Form
    {
        Form parent;
        DataTable dt;
        BindingSource bs = new BindingSource();
        Support s;
        public Recherche(Form parent, DataTable dt )
        {
            InitializeComponent();
            this.parent = parent;
            this.dt = dt;
        }

        private void Recherche_Load(object sender, EventArgs e)
        {
            parent.Visible = false;
            s = new Support();
            numericUpDown2.Maximum = DateTime.Now.Year;
            bs.DataSource = this.dt;
            dataGridView1.DataSource = bs;
        }

        private void Recherche_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Visible = true;
        }

        void filterApply(int[] check)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int checkedBoxes = 0;
            bool et = false;
            // Iterate through all of the Controls in your Form
            foreach (Control c in panel1.Controls)
            {
                // If one of the Controls is a CheckBox and it is checked, then
                // increment your count
                if (c is CheckBox && (c as CheckBox).Checked)
                {
                    checkedBoxes++;
                }
            }
            string rq = "";
            if (checkedBoxes == 4)
            {
                rq = String.Format("select * from  Oeuvre where {0} title = '{1}' And auteur = '{2}' And editeur = '{3}' And  anneeS = {4}", rq, textBox1.Text, textBox2.Text, textBox3.Text, Convert.ToInt32(numericUpDown2.Value)); ;
            }
            else if (checkedBoxes < 4 && checkedBoxes >= 2)
            {
                rq = "select * from  Oeuvre where";
                if (checkBox1.Checked && !et)
                {
                    rq = String.Format("{0} title = '{1}'", rq, textBox1.Text);
                    et = true;
                }
                else if (checkBox1.Checked)
                    rq = String.Format("{0} And title = '{1}'", rq, textBox1.Text);
                if (checkBox2.Checked && !et)
                {
                    rq = String.Format("{0} auteur = '{1}'", rq, textBox2.Text);
                    et = true;
                }
                else if (checkBox2.Checked)
                    rq = String.Format("{0} And auteur = '{1}'", rq, textBox2.Text);
                if (checkBox3.Checked && !et)
                {
                    rq = String.Format("{0} editeur = '{1}'", rq, textBox3.Text);
                    et = true;
                }
                else if (checkBox3.Checked)
                    rq = String.Format("{0} And editeur = '{1}'", rq, textBox3.Text);
                if (checkBox4.Checked && !et)
                {
                    rq = String.Format("{0} anneeS = {1}", rq, Convert.ToInt32(numericUpDown2.Value));
                    et = true;
                }
                else if (checkBox4.Checked)
                    rq = String.Format("{0} And anneeS = {1}", rq, Convert.ToInt32(numericUpDown2.Value));
            }
            else if (checkedBoxes == 1)
            {
                rq = "select * from  Oeuvre where";
                if (checkBox1.Checked)
                {
                    rq = String.Format("{0} title = '{1}'", rq, textBox1.Text);
                }
                if (checkBox2.Checked)
                {
                    rq = String.Format("{0} auteur = '{1}'", rq, textBox2.Text);
                }
                if (checkBox3.Checked)
                {
                    rq = String.Format("{0} editeur = '{1}'", rq, textBox3.Text);
                }
                if (checkBox4.Checked)
                {
                    rq = String.Format("{0} anneeS = {1}", rq, Convert.ToInt32(numericUpDown2.Value));
                }
            }
            else
            {
                rq = "select * from  Etudiant";
            }
            dataGridView1.DataSource = Support.afficherOeuvresCat(rq);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox2.Text = "";
            numericUpDown2.Value = 1900;
            bs.DataSource = this.dt;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Enabled = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            panel4.Enabled=checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            panel5.Enabled=checkBox4.Checked;
        }
    }
}
