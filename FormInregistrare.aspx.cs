using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppLicenta
{
    public partial class FormInregistrare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblParola.Visible = false;
            }

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
                }
            }
            else if (sesiuneProfesor != "__" && sesiuneDirector == "__")
            {
                statusLogare(false);
                functiiDir(false);                
            }
        }

        private void functiiDir(Boolean director){
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

        protected void btnInregistrare_Click(object sender, EventArgs e)
        {
            //generare parola
            StringBuilder sb = new StringBuilder();
            sb.Append(NumarRandom(10, 199));
            sb.Append(TextRandom(7));
            lblParola.Text = sb.ToString();

            //salvare date form
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            con.Open();
            String date1 = "INSERT INTO [Profesor] ([Nume_Prenume], [GradEvaluat]) VALUES " +
                "( '" + txtNume.Text + " " + txtPrenume.Text + "', '" + ddlGrad.Text + "' );";

            String date2 = "INSERT INTO [Utilizator] (Email, Parola, IdProfesor) " +
                "SELECT '" + txtEmail.Text + "' , '" + lblParola.Text + "' " +
                ", Profesor.IdProfesor FROM Profesor WHERE ((Profesor.Nume_Prenume) =" +
                " '" + txtNume.Text + " " + txtPrenume.Text + "' );";

            OleDbCommand com = new OleDbCommand(date1, con);
            com.ExecuteNonQuery();
            con.Close();
            con.Open();
            com = new OleDbCommand(date2, con);
            com.ExecuteNonQuery();
            con.Close();
            //trimite date pe email
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("kenna29elena@gmail.com");
            msg.To.Add(txtEmail.Text);
            msg.Subject = "Parola cont E-FPO";
            msg.Body = " Parola pentru noul cont este: <br/>" + lblParola.Text + "";
            msg.IsBodyHtml = true;
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("kenna29elena@gmail.com", "kenna2909elena!"),
                EnableSsl = true
            };
            client.Send(msg);
            msg.Dispose();
        }

        private int NumarRandom(int min, int max)
        {
            Random nr = new Random();
            return nr.Next(min, max);
        }

        private string TextRandom(int dim)
        {
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();
            char val;
            for (int i = 0; i < dim; i++)
            {
                val = Convert.ToChar(Convert.ToInt16(Math.Floor(24 * rnd.NextDouble() + 65)));
                sb.Append(val);
            }
            return sb.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label lbl = Master.FindControl("lblUserName") as Label;
            lbl.Text = "Test";
        }
    }
}