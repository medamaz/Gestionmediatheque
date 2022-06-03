using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_d_une_médiathéque
{
    public class Support
    {
        string cinstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moami\source\repos\DB\GestionMediatheque.mdf;Integrated Security = True";
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        public Support()
        {
            
        }

        public static DataTable afficherOeuvres()
        {
            string cinstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moami\source\repos\DB\GestionMediatheque.mdf;Integrated Security = True";
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                adapter = new SqlDataAdapter("select * from Oeuvre", cinstr)
                {
                    MissingSchemaAction = MissingSchemaAction.AddWithKey
                };
                adapter.Fill(dt);
                SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable afficherOeuvres(string type)
        {
            string cinstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moami\source\repos\DB\GestionMediatheque.mdf;Integrated Security = True";
            string rq;
            if (type == "SupportCD")
                rq = "select o.title, o.auteur, o.editeur, o.anneeS from Oeuvre o, SupportCD s where o.title = s.Oeuvreid";
            else
                rq = "select o.title, o.auteur, o.editeur, o.anneeS from Oeuvre o, SupportLivre s where o.title = s.Oeuvreid";
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                adapter = new SqlDataAdapter(rq, cinstr)
                {
                    MissingSchemaAction = MissingSchemaAction.AddWithKey
                };
                adapter.Fill(dt);
                SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public void ajouterOeuvres(string type,params Oeuvre[] Eo)
        {
            foreach (Oeuvre o in Eo)
            {
                try
                {
                    cn.ConnectionString = cinstr;
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = "INSERT INTO  Oeuvre (title,auteur,editeur,anneeS) VALUES (@title,@auteur,@editeur,@anneeS)";
                    cmd.Parameters.AddWithValue("@title", o.Titre);
                    cmd.Parameters.AddWithValue("@auteur", o.Auteur);
                    cmd.Parameters.AddWithValue("@editeur", o.Editeur);
                    cmd.Parameters.AddWithValue("@anneeS", o.Year);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    if (type == "SupportCD")
                    {
                        cmd.CommandText = "INSERT INTO  SupportCD VALUES (@Oeuvreid)";
                        cmd.Parameters.AddWithValue("@Oeuvreid", o.Titre);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO  SupportLivre VALUES (@Oeuvreid)";
                        cmd.Parameters.AddWithValue("@Oeuvreid", o.Titre);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
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

        public void ajouterOeuvres(string titre, string auteur, string editeur, int year,string type)
        {
            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "INSERT INTO  Oeuvre (title,auteur,editeur,anneeS) VALUES (@title,@auteur,@editeur,@anneeS)";
                cmd.Parameters.AddWithValue("@title", titre);
                cmd.Parameters.AddWithValue("@auteur", auteur);
                cmd.Parameters.AddWithValue("@editeur", editeur);
                cmd.Parameters.AddWithValue("@anneeS", year);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                if (type == "SupportCD")
                {
                    cmd.CommandText = "INSERT INTO  SupportCD VALUES (@Oeuvreid)";
                    cmd.Parameters.AddWithValue("@Oeuvreid", titre);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                else
                {
                    cmd.CommandText = "INSERT INTO  SupportLivre VALUES (@Oeuvreid)";
                    cmd.Parameters.AddWithValue("@Oeuvreid", titre);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
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

        public static DataTable rechercherOeuvres(string title, string auteur, string editeur, int year)
        {
            string cinstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moami\source\repos\DB\GestionMediatheque.mdf;Integrated Security = True";

            try
            {
                DataTable dt = new DataTable();
                string rq = "";
                SqlDataAdapter adapter = new SqlDataAdapter();
                if (title != "" || auteur != "" || editeur != "" || year >1900)
                {
                    rq = "select * from Oeuvre where";
                    if (title != "")
                    {
                        rq =String.Format("{0} title = {1}",rq,title);
                        
                    }
                    if (auteur != "")
                    {
                        rq = String.Format("{0} auteur = {1}", rq, auteur);

                    }
                    if (editeur != "")
                    {
                        rq = String.Format("{0} editeur = {1}", rq, editeur);

                    }
                    if (year > 1900)
                    {
                        rq = String.Format("{0} anneeS = {1}", rq, year);

                    }
                }
                else
                {
                    rq = "select * from Oeuvre";
                }
                adapter = new SqlDataAdapter(rq, cinstr)
                {
                    MissingSchemaAction = MissingSchemaAction.AddWithKey
                };
                adapter.Fill(dt);
                SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
                return dt;
            }
            catch
            {
                return null;
            }
           
        }

        public static Oeuvre rechercherOeuvres(int id)
        {
            string cinstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moami\source\repos\DB\GestionMediatheque.mdf;Integrated Security = True";

            try
            {
                Oeuvre oeuvre = new Oeuvre();
                DataTable dt = new DataTable();
                string rq = "";
                SqlDataAdapter adapter = new SqlDataAdapter();
                rq = "select * from Oeuvre where";
                rq = String.Format("{0} id = {1}", rq, id);
                adapter = new SqlDataAdapter(rq, cinstr)
                {
                    MissingSchemaAction = MissingSchemaAction.AddWithKey
                };
                adapter.Fill(dt);
                oeuvre.Titre = dt.Rows[0]["title"].ToString();
                oeuvre.Auteur = dt.Rows[0]["auteur"].ToString();
                oeuvre.Editeur = dt.Rows[0]["auteur"].ToString();
                oeuvre.Year = Convert.ToInt32(dt.Rows[0]["anneeS"]);
                SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
                return oeuvre;
            }
            catch
            {
                return null;
            }

        }

        public static Oeuvre rechercherOeuvres(string title)
        {
            string cinstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moami\source\repos\DB\GestionMediatheque.mdf;Integrated Security = True";

            try
            {
                Oeuvre oeuvre = new Oeuvre();
                DataTable dt = new DataTable();
                string rq = "";
                SqlDataAdapter adapter = new SqlDataAdapter();
                rq = "select * from Oeuvre where";
                rq = String.Format("{0} title = '{1}'", rq, title);
                adapter = new SqlDataAdapter(rq, cinstr)
                {
                    MissingSchemaAction = MissingSchemaAction.AddWithKey
                };
                adapter.Fill(dt);
                oeuvre.Titre = dt.Rows[0]["title"].ToString();
                oeuvre.Auteur = dt.Rows[0]["auteur"].ToString();
                oeuvre.Editeur = dt.Rows[0]["auteur"].ToString();
                oeuvre.Year = Convert.ToInt32(dt.Rows[0]["anneeS"]);
                SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
                return oeuvre;
            }
            catch
            {
                return null;
            }

        }

        public void supprimerOeuvres(string titre)
        {
                try
                {
                    cn.ConnectionString = cinstr;
                    cn.Open();
                    cmd.Connection = cn;

                    
                    cmd.CommandText = "DELETE FROM SupportCD WHERE Oeuvreid = @Oeuvreid ";
                    cmd.Parameters.AddWithValue("@Oeuvreid", titre);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "DELETE FROM SupportLivre WHERE Oeuvreid = @Oeuvreid ";
                    cmd.Parameters.AddWithValue("@Oeuvreid", titre);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "DELETE FROM Oeuvre WHERE title = @title";
                    cmd.Parameters.AddWithValue("@title", titre);
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

        public string findSupport(string titre)
        {
            try
            {
                SqlCommand cnd = new SqlCommand();
                cn.ConnectionString = cinstr;
                cn.Open();
                cnd.Connection = cn;
                cnd.CommandText = "select Oeuvreid from SupportCD where Oeuvreid = @title ";
                cnd.Parameters.AddWithValue("@title", titre);
                if (cnd.ExecuteScalar() != null)
                {
                    return "SupportCD";
                }
                else
                {
                    cnd.Parameters.Clear();
                    cnd.CommandText = "select Oeuvreid from SupportLivre where Oeuvreid = @title ";
                    cnd.Parameters.AddWithValue("@title", titre);
                    cnd.ExecuteScalar().ToString();
                    if (cnd.ExecuteScalar() != null)
                    {
                        return "SupportLivre";
                    }
                    else
                        return null;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                cn.Close();
            }
        }

        public void modifierOeuvres(int id,Oeuvre o)
        {
            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.CommandText = "UPDATE Oeuvre SET title = @title, auteur = @auteur, editeur =@editeur, anneeS = @anneeS where id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@title",o.Titre);
                cmd.Parameters.AddWithValue("@auteur", o.Auteur);
                cmd.Parameters.AddWithValue("@editeur", o.Editeur);
                cmd.Parameters.AddWithValue("@anneeS", o.Year);
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
