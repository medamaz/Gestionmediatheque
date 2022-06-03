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
    public partial class ListRetard : Form
    {
        Form parent;
        GestionPrets pr;
        public ListRetard(Form parent,GestionPrets pr)
        {
            InitializeComponent();
            this.pr = pr;
            this.parent = parent;
        }

        private void ListRetard_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = pr.adhrentReatrd();
        }
    }
}
