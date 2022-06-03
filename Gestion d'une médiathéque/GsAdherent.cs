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
    public partial class GsAdherent : Form
    {
        Form parant;
        public GsAdherent(Form parant)
        {
            InitializeComponent();
            this.parant = parant;
        }

        private void GsAdherent_Load(object sender, EventArgs e)
        {
            parant.Visible = false;
            dataGridView1.DataSource = Adherent.afficherAdherent();
            dataGridView1.Columns["id"].Visible = false;
        }

        private void GsAdherent_FormClosing(object sender, FormClosingEventArgs e)
        {
            parant.Visible = Enabled;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Adherent ad = new Adherent(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            Adherent.ajouterAdherent(ad);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Etes-vous sûr ?", "ATTENTION !!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Adherent.supprimerAdherent(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value));
            }
            dataGridView1.DataSource = Adherent.afficherAdherent();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Adherent ad = new Adherent(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            Adherent.modifierAdherent(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value), ad);
            dataGridView1.DataSource = Adherent.afficherAdherent();
        }
    }
}
