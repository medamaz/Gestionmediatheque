using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_d_une_médiathéque
{
    public class GestionPrets
    {

        string cinstr = Program.cinstr;
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        

        List<Prets> prets;

        public GestionPrets()
        {
            
           
        }

        public List<Prets> Prets { get => prets; set => prets = value; }

        public void addPret(DateTime date, Oeuvre oe,Adherent ad)
        {
            Prets pr;
            int cmp = 0;
            if(prets.Count >= 1 || Prets == null)
            {
                foreach (Prets p in Prets)
                {
                    if (p.Ad.Num == ad.Num)
                    {
                        if(p.O.Titre != oe.Titre)
                        {
                            if (cmp > 3)
                            {
                                throw new Exception("le Adherent doit emprunter au max 3 oeuvre");
                            }
                            else
                                cmp++;
                        }
                        else
                        {
                            throw new Exception("le Adherent doit emprunter un seul exmplaire de chaque oeuvre");
                        }
                        
                    }
                       
                }
                if (cmp < 3)
                {
                    pr = new Prets(date, ad, oe);
                    Prets.Add(pr);
                    ajouterPrets(pr);
                }
            }
            else
            {
                pr = new Prets(date, ad, oe);
                Prets.Add(pr);
                ajouterPrets(pr);
            }
            
        }

        public void modifierPret(DateTime date, Oeuvre oe, Adherent ad,int id)
        {
            Prets pr = new Prets(date, ad, oe);
            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "Update Prets Set dtepr =@dtepr, adherents = @adherents, ouevre = @ouevre where id =@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@dtepr", pr.Dtepr);
                cmd.Parameters.AddWithValue("@adherents", pr.Ad.Num);
                cmd.Parameters.AddWithValue("@ouevre", pr.O.Titre);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch
            {
                return;
            }
            finally
            {
                cn.Close();
            }
        }

        public List<Prets> adhrentReatrd()
        {
            List <Prets> adhrentReatrd = new List<Prets>();
            foreach (Prets p in prets)
            {
                if (p.Dtepr.AddDays(14) < DateTime.Now)
                {
                    adhrentReatrd.Add(p);
                }
            }
            return adhrentReatrd;
        }

        public DataTable afficherPrets()
        {
            Prets = new List<Prets>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                adapter = new SqlDataAdapter("select * from Prets", cinstr)
                {
                    MissingSchemaAction = MissingSchemaAction.AddWithKey
                };
                adapter.Fill(dt);
                SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Prets pr = new Prets();
                    Adherent adherent = new Adherent(Adherent.rechercherAdherent(Convert.ToInt32(dt.Rows[i]["adherents"])));
                    Oeuvre oeuvre = new Oeuvre(Support.rechercherOeuvres(dt.Rows[i]["ouevre"].ToString()));
                    pr.Dtepr = Convert.ToDateTime(dt.Rows[i]["dtepr"]);
                    pr.O = oeuvre;
                    pr.Ad = adherent;
                    prets.Add(pr);
                }
                return dt;
            }
            catch
            {
                return null;
            }
        }

        private void ajouterPrets(Prets pr)
        {
            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "INSERT INTO  Prets (dtepr,adherents,ouevre) VALUES (@dtepr,@adherents,@ouevre)";
                cmd.Parameters.AddWithValue("@dtepr", pr.Dtepr);
                cmd.Parameters.AddWithValue("@adherents", pr.Ad.Num);
                cmd.Parameters.AddWithValue("@ouevre", pr.O.Titre);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch
            {
                return;
            }
            finally
            {
                cn.Close();
            }
           
        }

        public void supprimerPret(int id)
        {
            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "DELETE FROM Prets WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch
            {
                return;
            }
            finally
            {
                cn.Close();
            }
        }

    }
}
