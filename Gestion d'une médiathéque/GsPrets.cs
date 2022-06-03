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
    public partial class GsPrets : Form
    {
        GestionPrets gP = new GestionPrets();
        Form parent;
        BindingSource bs = new BindingSource();

        public GsPrets(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void GsPrets_Load(object sender, EventArgs e)
        {
            parent.Visible = false;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "nom";
            comboBox2.ValueMember = "title";
            comboBox2.DisplayMember = "title";
            comboBox1.DataSource = Adherent.afficherAdherent();
            comboBox2.DataSource = Support.afficherOeuvres();
            bs.DataSource = gP.afficherPrets();
            dataGridView1.DataSource = bs;
            dataGridView1.Columns["id"].Visible = false;
            comboBox1.DataBindings.Add("SelectedValue", bs, "adherents", true);
            comboBox2.DataBindings.Add("SelectedValue", bs, "ouevre", true);
            dateTimePicker1.DataBindings.Add("value", bs, "dtepr", true);

        }

        private void GsPrets_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Visible=true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                gP.addPret(dateTimePicker1.Value,Support.rechercherOeuvres(comboBox2.SelectedValue.ToString()), Adherent.rechercherAdherent(Convert.ToInt32(comboBox1.SelectedValue.ToString())));
                bs.DataSource = gP.afficherPrets();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ListRetard(this, gP).ShowDialog();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            comboBox1.SelectedValue = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value);
            comboBox2.SelectedItem = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gP.modifierPret(dateTimePicker1.Value, Support.rechercherOeuvres(comboBox2.SelectedValue.ToString()), Adherent.rechercherAdherent(Convert.ToInt32(comboBox1.SelectedValue.ToString())),Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value));
            bs.DataSource = gP.afficherPrets();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            gP.supprimerPret(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value));
            bs.DataSource = gP.afficherPrets();

        }
    }
}
