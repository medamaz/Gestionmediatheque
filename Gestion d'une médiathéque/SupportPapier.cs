using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_d_une_médiathéque
{
    public class SupportPapier 
    {
        public SupportPapier()
        {
        }
        public override string ToString()
        {
            string str = "les Oeuvre de type Papier est :\n";
            List<Oeuvre> l = new List<Oeuvre>();
            //l = this.afficherOeuvres("SupportPapier");
            foreach(Oeuvre o in l)
            {
                str = str + o.ToString() + "\n";
            }
            return str;
        }
    }
}
