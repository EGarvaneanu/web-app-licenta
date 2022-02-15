using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppLicenta
{
    public partial class FormModificareParola : System.Web.UI.Page
    {

        String sesiune = "null";
        protected void Page_Load(object sender, EventArgs e)
        {
            String sesiuneDirector = "_" + (string)Session["Director"] + "_";
            String sesiuneProfesor = "_" + (string)Session["Profesor"] + "_";
            initializeazaSesiune(sesiuneProfesor, sesiuneDirector);
        }

        private void initializeazaSesiune(String sesiuneProfesor, String sesiuneDirector)
        {
            if (sesiuneProfesor == "__")
            {
                if (sesiuneDirector == "__")
                {
                    statusLogare(false);
                }
                else if (sesiuneDirector != "__")
                {
                    statusLogare(true);
                    functiiDir(true);
                    sesiune = (string)Session["Director"];
                }
            }
            else if (sesiuneProfesor != "__" && sesiuneDirector == "__")
            {
                statusLogare(false);
                functiiDir(false);
                sesiune = (string)Session["Profesor"];
            }
        }

        private void functiiDir(Boolean director)
        {
            if (director == true)
            {
                ((Button)Master.FindControl("btnInregistrare")).Visible = true;
                ((Button)Master.FindControl("btnIncarcareDocumente")).Visible = true;
            }
            else if (director == false)
            {
                ((Button)Master.FindControl("btnInregistrare")).Visible = false;
                ((Button)Master.FindControl("btnIncarcareDocumente")).Visible = false;
            }
        }

        private void statusLogare(Boolean logat)
        {
            if (logat == true)
            {
                ((Button)Master.FindControl("btnProfil")).Visible = true;
                ((Button)Master.FindControl("btnSchimbaParola")).Visible = true;
                ((Button)Master.FindControl("btnDeconectare")).Visible = true;
                ((Button)Master.FindControl("btnLogare")).Visible = false;
                ((Button)Master.FindControl("btnResetParola")).Visible = false;
            }
            else
            {
                Response.Redirect("Logare.aspx");
            }
        }

        protected void btnModificareParola_Click(object sender, EventArgs e)
        {
            Int32 id = gasesteIdProfesor();
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            con.Open();
            String interogare1 = "SELECT Utilizator.Email, Utilizator.Parola, Profesor.Nume_Prenume FROM Utilizator INNER JOIN Profesor ON Utilizator.IdProfesor = Profesor.IdProfesor" +
                " WHERE IdUtilizator = " + id + "  and Parola = '" + txtParolaVeche.Text + "' ";

            OleDbCommand com = new OleDbCommand(interogare1, con);
            OleDbDataReader reader = com.ExecuteReader();
            
            while (reader.Read())
            {
                String interogare2 = "UPDATE Utilizator SET Parola = '" + txtParolaNoua.Text + "' WHERE IdUtilizator = " + id + " ";
                com = new OleDbCommand(interogare2, con);
                com.ExecuteNonQuery();
            }
            con.Close();
        }

        private Int32 gasesteIdProfesor()
        {           
            sesiune = sesiune.Replace("Dir. ", "");
            sesiune = sesiune.Replace("Prof. ", "");

            Int32 id = 0;
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            String interogare = "SELECT IdProfesor FROM Profesor WHERE (Nume_Prenume = '" + sesiune + "' )";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(interogare, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                id = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();
            return id;
        }
    }
}