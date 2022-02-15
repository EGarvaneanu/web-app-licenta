using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppLicenta
{
    public partial class FormResetareParola : System.Web.UI.Page
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
                ((Button)Master.FindControl("btnDetaliiProfesori")).Visible = false;
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

        protected void btnTrmitere_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                lblTest.Text = "Va rog completati e-mailul";
            }
            else
            {
                DataSet ds = new DataSet();

                OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
                con.Open();
                String date = "SELECT Utilizator.Email, Profesor.Nume_Prenume " +
                   "FROM Utilizator INNER JOIN Profesor ON Utilizator.IdProfesor = Profesor.IdProfesor " +
                   "WHERE((Profesor.FunctieBaza_Loc = 'dir. lect. dr./UOC' AND Profesor.Departament = " +
                   "(SELECT Profesor.Departament " +
                   "FROM Utilizator INNER JOIN Profesor ON Utilizator.IdProfesor = Profesor.IdProfesor " +
                   "WHERE Utilizator.Email = '" + txtEmail.Text + "')) " +
                   "OR(Profesor.FunctieBaza_Loc = 'dir. conf. dr./UOC' AND Profesor.Departament = " +
                   "(SELECT Profesor.Departament " +
                   "FROM Utilizator INNER JOIN Profesor ON Utilizator.IdProfesor = Profesor.IdProfesor " +
                   "WHERE Utilizator.Email = '" + txtEmail.Text + "') " +
                   ")) ";

                OleDbCommand com = new OleDbCommand(date, con);
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(com))
                {
                    adapter.Fill(ds);
                }
                if (ds.Tables[0].Rows.Count == 0)
                {
                    lblTest.Text = "Acest e-mail nu este inregistrat";
                }
                else
                {
                    String nume = ds.Tables[0].Rows[0]["Nume_Prenume"].ToString();
                    String email = ds.Tables[0].Rows[0]["Email"].ToString();
                    lblTest.Text = nume + " " + email;

                    //trimite date pe email
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("kenna29elena@gmail.com");
                    msg.To.Add(email);
                    msg.Subject = "Resetare parola";
                    msg.Body = nume + ", profesorul cu email-ul: " + txtEmail.Text + " solicita resetarea parolei.";
                    msg.IsBodyHtml = true;
                    var client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        Credentials = new System.Net.NetworkCredential("kenna29elena@gmail.com", "kenna2909elena!"),
                        EnableSsl = true
                    };
                    client.Send(msg);
                }

                con.Close();
            }
        }
    }
}