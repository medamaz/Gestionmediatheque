using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_d_une_médiathéque
{
    public partial class GsOeuvres : Form
    {
        Support s;
        Form parent;
        public GsOeuvres(Form parent)
        {
            InitializeComponent();
            s = new Support();
            this.parent = parent;
        }

        private void GsOeuvres_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            parent.Visible = false;
            numericUpDown1.Maximum = DateTime.Now.Year;
            dataGridView1.DataSource = Support.afficherOeuvres();
            dataGridView1.Columns["id"].Visible = false;

        }

        private void GsOeuvres_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!=""&& textBox3.Text != "" && textBox2.Text != "")
            {
                s.ajouterOeuvres(textBox1.Text, textBox2.Text, textBox3.Text, (int)numericUpDown1.Value, comboBox2.SelectedItem.ToString());
                dataGridView1.DataSource = Support.afficherOeuvres();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Etes-vous sûr ?", "ATTENTION !!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {

                s.supprimerOeuvres(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());

            }
            dataGridView1.DataSource = Support.afficherOeuvres();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            numericUpDown1.Value = 1900;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Etes-vous sûr ?", "ATTENTION !!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Oeuvre oeuvre = new Oeuvre(textBox1.Text, textBox2.Text, textBox3.Text, (int)numericUpDown1.Value);
                s.modifierOeuvres(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value), oeuvre);
            }
            dataGridView1.DataSource = Support.afficherOeuvres();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Recherche(this,Support.afficherOeuvres()).ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                dataGridView1.DataSource = Support.afficherOeuvres();
            else if (comboBox1.SelectedIndex == 1)
                dataGridView1.DataSource = Support.afficherOeuvres(comboBox1.SelectedItem.ToString());
            else if (comboBox1.SelectedIndex == 2)
                dataGridView1.DataSource = Support.afficherOeuvres(comboBox1.SelectedItem.ToString());
           
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            numericUpDown1.Value = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value);
            if (s.findSupport(textBox1.Text).ToString() == "SupportCD")
                comboBox2.SelectedIndex = 0;
            else
                comboBox2.SelectedIndex=1;
        }
    }
}
