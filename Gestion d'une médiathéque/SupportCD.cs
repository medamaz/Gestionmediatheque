using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_d_une_médiathéque
{
    public class SupportCD
    {
        public SupportCD()
        {

        }
        public override string ToString()
        {
            string str = "les Oeuvre de type CD est :\n";
            List<Oeuvre> l = new List<Oeuvre>();
            //l = this.afficherOeuvres("SupportCD");
            foreach (Oeuvre o in l)
            {
                str = str + o.ToString() + "\n";
            }
            return str;
        }
    }
}
