using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppLicenta
{
    public partial class FormIncarcareDocumente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String sesiuneProfesor = "";
            String sesiuneDirector = "";
            sesiuneDirector = "_" + (string)Session["Director"] + "_";
            sesiuneProfesor = "_" + (string)Session["Profesor"] + "_";
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
                statusLogare(true);
                functiiDir(false);
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

        private void salveazaDate(int NrCrt, int PozitieStatFunctie, int NrOreCurs, int NrOreLaborator, int NrOreSeminar, int NrOreSemestru, 
            int NumarSaptamani, String Post, String Nume_Prenume, String DenumireD, String ProgramStudii, String An_Serie_Grupe)
        {
            OleDbConnection con;
            OleDbCommand com, cmd;
            con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            
            String datePost = "INSERT INTO Post ( NrCrt, PozitieStatFunctie, Post, IdProfesor ) " +
                "SELECT "+ NrCrt +" AS NrCrt, "+PozitieStatFunctie+" AS PozitieStatFunctie, '"+ Post
                +"' AS Post, Profesor.IdProfesor FROM Profesor WHERE((Profesor.[Nume_Prenume]) = '"+Nume_Prenume+"');";

            String dateDisciplina = "INSERT INTO Disciplina ( DenumireD, ProgramStudii, An_Serie_Grupe, " +
                "NrOreCurs, NrOreLaborator, NrOreSeminar, NrOreSemestru, NumarSaptamani, NrCrt) " +
                "VALUES('" + DenumireD + "', '" + ProgramStudii + "', '" + An_Serie_Grupe+ "', " + NrOreCurs+ ", " + NrOreLaborator+ ", " + NrOreSeminar+ ", "+NrOreSemestru+", "+NumarSaptamani+", "+NrCrt+");";
            
            con.Open();
            com = new OleDbCommand(datePost, con);
            com.ExecuteNonQuery();
            con.Close();

            con.Open();
            cmd = new OleDbCommand(dateDisciplina, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void salveazaCalendar(int zi, String oct, String nov, String dec, String ian, String feb, String mar, String apr, String mai, String iun, 
            String iul, String aug, String sep, String orarOct, String orarNov, String orarDec, String orarIan, String orarFeb, String orarMar,
            String orarApr, String orarMai, String orarIun, String orarIul, String orarAug, String orarSep)
        {
            OleDbConnection con;
            OleDbCommand com;
            con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");

            String datePost = "INSERT INTO Calendar VALUES ("+ zi + ", '"+ oct + "', '" + orarOct + "', '" + nov + "', '" + orarNov + "', '" +  dec + "', '" 
                + orarDec + "', '" + ian + "', '" + orarIan + "', '" + feb + "', '" + orarFeb + "', '" + mar + "', '" + orarMar + "', '" + apr + "', '" 
                + orarApr + "', '" + mai + "', '" + orarMai + "', '" + iun + "', '" + orarIun + "', '" + iul + "', '" + orarIul + "', '" + aug + "', '"
                + orarAug + "', '" + sep + "', '" + orarSep + "')";
            con.Open();
            com = new OleDbCommand(datePost, con);
            com.ExecuteNonQuery();
            con.Close();
        }

        protected void btnImport1_Click(object sender, EventArgs e)
        {
            int NrCrt, PozitieStatFunctie, NrOreCurs, NrOreLaborator, NrOreSeminar, NrOreSemestru, NumarSaptamani;
            String Post, Nume_Prenume, DenumireD, ProgramStudii, An_Serie_Grupe;

            try
            {
                //Conexiune cu fisierul excel
                String path = Path.GetFileName(uplFisePlataOra.FileName);
                path = path.Replace(" ", "");
                uplFisePlataOra.SaveAs(Server.MapPath("~/Documente/") + path);
                String ExcelPath = Server.MapPath("~/Documente/") + path;

                OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + ExcelPath + "; Excel 8.0; Persist Security Info = False");
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Foaie1$]", con);
                con.Open();

                //Salvare date
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    NrCrt = Convert.ToInt32(dr[0].ToString());
                    PozitieStatFunctie = Convert.ToInt32(dr[1].ToString());
                    Post = dr[2].ToString();
                    DenumireD = dr[3].ToString();
                    Nume_Prenume = dr[4].ToString();
                    //FunctieBaza_Loc = dr[5].ToString();
                    //GradEvaluat = dr[6].ToString();
                    ProgramStudii = dr[7].ToString();
                    An_Serie_Grupe = dr[8].ToString();
                    NrOreCurs = Convert.ToInt32(dr[9].ToString());
                    NrOreLaborator = Convert.ToInt32(dr[10].ToString());
                    NrOreSeminar = Convert.ToInt32(dr[11].ToString());
                    NrOreSemestru = Convert.ToInt32(dr[12].ToString());
                    NumarSaptamani = Convert.ToInt32(dr[13].ToString());
                    salveazaDate(NrCrt, PozitieStatFunctie, NrOreCurs, NrOreLaborator, NrOreSeminar, NrOreSemestru,
                    NumarSaptamani, Post, Nume_Prenume, DenumireD, ProgramStudii, An_Serie_Grupe);
                    lblEroare.ForeColor= System.Drawing.Color.Green;
                    lblEroare.Text = "Salvat cu succes";
                    lblEroare.Visible = true;

                }
                dr.Close();
                con.Close();
            }
            catch (Exception exc)
            {
                String exceptie = exc + "";
                if (exceptie.Contains("Could not find a part of the path"))
                {
                    lblEroare.Text = "Va rugam sa selectati un fisier adecvat inainte";
                    lblEroare.Visible = true;
                }
            }
        }

        protected void btnAfisare1_Click(object sender, EventArgs e)
        {
            try
            {
                lblEroare.Visible = false;
                //Conexiune cu fisierul excel
                String path = Path.GetFileName(uplFisePlataOra.FileName);
                path = path.Replace(" ", "");
                uplFisePlataOra.SaveAs(Server.MapPath("~/Documente/") + path);
                String ExcelPath = Server.MapPath("~/Documente/") + path;

                OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + ExcelPath + "; Excel 8.0; Persist Security Info = False");
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Foaie1$]", con);

                //Incarcare date
                DataSet ds = new DataSet();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                }
                con.Close();

                //Adaugare date tabel
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            catch(Exception exc)
            {
                String exceptie = exc + "";
                if (exceptie.Contains("Could not find a part of the path"))
                {
                    lblEroare.Text = "Va rugam sa selectati un fisier inainte";
                    lblEroare.Visible = true;
                }
            }
        }

        protected void MyDayRenderer(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsToday)
            {
                e.Cell.BackColor = System.Drawing.Color.Aqua;
            }

            if (e.Day.Date == new DateTime(2021, 6, 22))
            {
                e.Cell.BackColor = System.Drawing.Color.Beige;
            }

            DataTable dt = GetData();
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToDateTime(e.Day.Date).ToShortDateString() == Convert.ToDateTime(row["Date"]).ToShortDateString())
                {
                    e.Cell.BackColor = System.Drawing.Color.Red;
                    e.Cell.ToolTip = row["Desc"].ToString();
                    e.Cell.Controls.Clear();
                    HyperLink link = new HyperLink();
                    link.Text = Convert.ToString(e.Day.Date.Day);
                    link.ToolTip = row["Desc"].ToString();
                    link.NavigateUrl = e.SelectUrl;
                    e.Cell.Controls.Add(link);
                }
                else
                {
                    e.Cell.ToolTip = e.Day.Date.ToString("MMMM dd");
                }
            }
        }

        private static DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Date");
            dt.Columns.Add("Desc");
            dt.Rows.Add("2020-01-01 15:23:34.123", "Happy New Year");
            dt.Rows.Add("2021-12-25 15:23:34.123", "Prima zi de Craciun");
            dt.Rows.Add("2021-12-26 15:23:34.123", "A doua zi de Craciun");
            return dt;
        }

        protected void btnAfisare2_Click(object sender, EventArgs e)
        {
            try
            {
                lblEroare.Visible = false;
                //Conexiune cu fisierul excel
                String path = Path.GetFileName(uplFisePlataOra.FileName);
                path = path.Replace(" ", "");
                uplFisePlataOra.SaveAs(Server.MapPath("~/Documente/") + path);
                String ExcelPath = Server.MapPath("~/Documente/") + path;

                OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + ExcelPath + "; Excel 8.0; Persist Security Info = False");
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Foaie1$]", con);

                //Incarcare date
                DataSet ds = new DataSet();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                }
                con.Close();

                //Adaugare date tabel
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            catch (Exception exc)
            {
                String exceptie = exc + "";
                if (exceptie.Contains("Could not find a part of the path"))
                {
                    lblEroare.Text = "Va rugam sa selectati un fisier inainte";
                    lblEroare.Visible = true;
                }
            }
        }

        protected void btnImport2_Click(object sender, EventArgs e)
        {
            try
            {
                int zi;
                String oct, nov, dec, ian, feb, mar, apr, mai, iun, iul, aug, sep, orarOct, orarNov, orarDec,
                       orarIan, orarFeb, orarMar, orarApr, orarMai, orarIun, orarIul, orarAug, orarSep;
                //Conexiune cu fisierul excel
                String path = Path.GetFileName(uplCalendar.FileName);
                path = path.Replace(" ", "");
                uplCalendar.SaveAs(Server.MapPath("~/Documente/") + path);
                String ExcelPath = Server.MapPath("~/Documente/") + path;

                OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + ExcelPath + "; Excel 8.0; Persist Security Info = False");
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Foaie1$]", con);
                con.Open();

                //Salvare date
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {


                    zi = Convert.ToInt32(dr[0].ToString());
                    oct = dr[1].ToString();
                    nov = dr[3].ToString();
                    dec = dr[5].ToString();
                    ian = dr[7].ToString();
                    feb = dr[9].ToString();
                    mar = dr[11].ToString();
                    apr = dr[13].ToString();
                    mai = dr[15].ToString();
                    iun = dr[17].ToString();
                    iul = dr[19].ToString();
                    aug = dr[21].ToString();
                    sep = dr[23].ToString();
                    orarOct = dr[2].ToString();
                    orarNov = dr[4].ToString();
                    orarDec = dr[6].ToString();
                    orarIan = dr[8].ToString();
                    orarFeb = dr[10].ToString();
                    orarMar = dr[12].ToString();
                    orarApr = dr[14].ToString();
                    orarMai = dr[16].ToString();
                    orarIun = dr[18].ToString();
                    orarIul = dr[20].ToString();
                    orarAug = dr[22].ToString();
                    orarSep = dr[24].ToString();
                    salveazaCalendar(zi, oct, nov, dec, ian, feb, mar, apr, mai, iun, iul, aug, sep, orarOct, orarNov, orarDec, orarIan, orarFeb, orarMar, orarApr, orarMai, orarIun, orarIul, orarAug, orarSep);
                    lblEroare2.ForeColor = System.Drawing.Color.Green;
                    lblEroare2.Text = "Salvat cu succes";
                    lblEroare2.Visible = true;
                }
                dr.Close();
                con.Close();
            }
            catch (Exception exc)
            {
                String exceptie = exc + "";
                if (exceptie.Contains("Could not find a part of the path"))
                {
                    lblEroare.Text = "Va rugam sa selectati un fisier inainte";
                    lblEroare.Visible = true;
                }
            }
           
        }

        protected void btnStergere_Click(object sender, EventArgs e)
        {
            OleDbConnection con;
            OleDbCommand com, com2;
            con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");

            String interog = "DELETE FROM Post ;";
            String interog2 = "DELETE FROM Disciplina ;";
            com = new OleDbCommand(interog, con);
            com2 = new OleDbCommand(interog2, con);
            con.Open();
            com.ExecuteNonQuery();
            com2.ExecuteNonQuery();
            con.Close();

            System.Windows.Forms.MessageBox.Show("Stergerea s-a realizat cu succes!");
        }

        protected void btnStergere2_Click(object sender, EventArgs e)
        {
            OleDbConnection con;
            OleDbCommand com;
            con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");

            String interog = "DELETE FROM Calendar ;";
            com = new OleDbCommand(interog, con);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();

            System.Windows.Forms.MessageBox.Show("Stergerea s-a realizat cu succes!");
        }
    }
}