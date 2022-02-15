using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data.OleDb;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Data;

namespace WebAppLicenta
{
    public partial class Logare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogare_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            con.Open();
            String date = "SELECT Utilizator.Email, Utilizator.Parola, Profesor.Nume_Prenume, Profesor.FunctieBaza_Loc FROM Utilizator INNER JOIN Profesor ON Utilizator.IdProfesor = Profesor.IdProfesor" +
                " WHERE Email = '" + txtEmailLog.Text + "'  and Parola = '" + txtParola.Text + "' ";

            OleDbCommand com = new OleDbCommand(date, con);
            OleDbDataAdapter da = new OleDbDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            com.ExecuteNonQuery();
            if (dt.Rows.Count > 0)
            {
                String FunctieBaza_Loc = dt.Rows[0]["FunctieBaza_Loc"].ToString();
                String Nume = dt.Rows[0]["Nume_Prenume"].ToString();
                if ((FunctieBaza_Loc == "dir. conf. dr./UOC") || (FunctieBaza_Loc == "dir. lect. dr./UOC"))
                {
                    Session["Director"] = "Dir. " + Nume;
                    Response.Redirect("Homepage.aspx");
                }
                else
                {
                    Session["Profesor"] = "Prof. " + Nume;
                    Response.Redirect("Homepage.aspx");
                }
            }
            con.Close();
        }

    }
}