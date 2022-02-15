using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Table = Xceed.Document.NET.Table;

namespace WebAppLicenta
{
    public partial class FormReferatPO : System.Web.UI.Page
    {

        String anulCurent = DateTime.Now.Year.ToString();        
        String anulUrmator = (DateTime.Now.Year + 1).ToString();
        String[] luni = { "Ian", "Feb", "Mar", "Apr", "Mai", "Iun", "Iul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        String sesiuneProfesor = "";
        String sesiuneDirector = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            sesiuneDirector = "_" + (string)Session["Director"] + "_";
            sesiuneProfesor = "_" + (string)Session["Profesor"] + "_";
            initializeazaSesiune(sesiuneProfesor, sesiuneDirector);

            if (sesiuneProfesor != "__" && sesiuneDirector.Equals("__"))
            {
                GridView1.Visible = false;
                ddlProfesori.Visible = false;
                lblProfesori.Visible = false;
            }
            else if (sesiuneProfesor.Equals("__") && sesiuneDirector != "__")
            {
                GridView1.Visible = true;
                ddlProfesori.Visible = true;
            }

            if (!Page.IsPostBack)
            {
                GridView1.DataBind();
                BindData();
                ddlLuna.DataSource = luni;
                ddlLuna.DataBind();
            }

        }
        //functii logare
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

        //functii legate de butoane
        protected void btnGenereazaCerere_Click(object sender, EventArgs e)
        {

            int id = 0;
            String nume = "Lipsa";
            if (sesiuneProfesor != "__" && sesiuneDirector.Equals("__"))
            {
                id = Int32.Parse(gasesteIdProfesor());
            }
            else if (sesiuneProfesor.Equals("__") && sesiuneDirector != "__")
            {
                id = Int32.Parse(ddlProfesori.Text);
            }
            nume = gasesteNumeProfesor(id);

            String numeNou = Regex.Replace(nume, @"\s+", "");

            String modelDoc = @"D:\referat_PO.docx";
            String docNou = @"D:\referat_PO_" + numeNou + ".doc";
            adaugaTabel(modelDoc, docNou);
            adaugaInformatiiText(docNou);

            Process.Start("WINWORD.EXE", docNou);


        }

        //functii principale
        private void adaugaInformatiiText(object filename)
        {
            int id = 0;

            if (sesiuneProfesor != "__" && sesiuneDirector.Equals("__"))
            {
                id = Int32.Parse(gasesteIdProfesor());
            }
            else if (sesiuneProfesor.Equals("__") && sesiuneDirector != "__")
            {
                id = Int32.Parse(ddlProfesori.Text);
            }

            String luna = ddlLuna.Text;

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            object missing = Missing.Value;

            Microsoft.Office.Interop.Word.Document myWordDoc = null;

            if (File.Exists((string)filename))
            {
                object readOnly = false;

                object isvisible = false;

                wordApp.Visible = false;
                myWordDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                     ref missing, ref missing, ref missing, ref missing);
                myWordDoc.Activate();
                this.FindAndReplace(wordApp, "<nume>", gasesteNumeProfesor(id));
                this.FindAndReplace(wordApp, "<functie>", gasesteFunctieProfesor(id)); ;
                this.FindAndReplace(wordApp, "<an>", anulCurent + "-" + anulUrmator);
                this.FindAndReplace(wordApp, "<luna>", luna);
                this.FindAndReplace(wordApp, "<totalOre>", "__");

                myWordDoc.Save();
                myWordDoc.Close();
                wordApp.Quit();
            }
        }
        private void adaugaTabel(String modelDoc, String docNou)
        {
            #region Initializare Document
            try
            {
               var doc = DocX.Load(modelDoc);
            #endregion

            #region Determinare Id
            int id = 0;

            if (sesiuneProfesor != "__" && sesiuneDirector.Equals("__"))
            {
                id = Int32.Parse(gasesteIdProfesor());
            }
            else if (sesiuneProfesor.Equals("__") && sesiuneDirector != "__")
            {
                id = Int32.Parse(ddlProfesori.Text);
            }
            #endregion

            #region Tabel
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            String datePost = "SELECT [PozitieStatFunctie], [Post], [NrOreCurs], [NrOreLaborator], [NrOreSeminar], [DenumireD], [ProgramStudii], [An_Serie_Grupe] " +
                "FROM Post INNER JOIN Disciplina ON Post.NrCrt = Disciplina.NrCrt " +
                "WHERE (Post.IdProfesor= " + id + " )";
            String nrRanduri = "SELECT COUNT ( [NrCrt] ) FROM [Post] WHERE (IdProfesor= " + id + " )";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(datePost, con);
            OleDbCommand com = new OleDbCommand(nrRanduri, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            OleDbDataReader dr2 = com.ExecuteReader();

            int randuri = 1;
            while (dr2.Read())
            {
                randuri = Convert.ToInt32(dr2[0].ToString());
            }
            Table t = doc.AddTable(randuri + 1, 9);

            t.Alignment = Alignment.center;
            //Capul tabelului
            t.Rows[0].Cells[0].Paragraphs.First().Append("Post și Poziție Stat de funcții");
            t.Rows[0].Cells[1].Paragraphs.First().Append("Nr. de ore curs");
            t.Rows[0].Cells[2].Paragraphs.First().Append("Nr. de ore aplicaţii");
            t.Rows[0].Cells[3].Paragraphs.First().Append("Disciplina");
            t.Rows[0].Cells[4].Paragraphs.First().Append("Program de studii");
            t.Rows[0].Cells[5].Paragraphs.First().Append("Anul, Seria și Grupa");
            t.Rows[0].Cells[6].Paragraphs.First().Append("Date de desfasurare");
            t.Rows[0].Cells[7].Paragraphs.First().Append("Interval orar");
            t.Rows[0].Cells[8].Paragraphs.First().Append("Format/Loc");
                //Corpul tabelului
            int rand = 1;
            while (dr.Read())
            {
                String col1 = dr[1].ToString() + " " + dr[0].ToString();
                String col2 = dr[2].ToString();
                int nrOreAplicatii = Convert.ToInt32(dr[3].ToString()) + Convert.ToInt32(dr[4].ToString());
                String col3 = nrOreAplicatii.ToString();
                String col4 = dr[5].ToString();
                String col5 = dr[6].ToString();
                String col6 = dr[7].ToString();
                t.Rows[rand].Cells[0].Paragraphs.First().Append(col1);
                t.Rows[rand].Cells[1].Paragraphs.First().Append(col2);
                t.Rows[rand].Cells[2].Paragraphs.First().Append(col3);
                t.Rows[rand].Cells[3].Paragraphs.First().Append(col4);
                t.Rows[rand].Cells[4].Paragraphs.First().Append(col5);
                t.Rows[rand].Cells[5].Paragraphs.First().Append(col6);
                t.Rows[rand].Cells[6].Paragraphs.First().Append("dd/mm/yyyy");
                t.Rows[rand].Cells[7].Paragraphs.First().Append("hh:mm-hh:mm");
                t.Rows[rand].Cells[8].Paragraphs.First().Append("Online/Teams");
                rand++;
            }
            doc.InsertTable(t);
            con.Close();
            #endregion

            #region Salvare document
            doc.SaveAs(docNou);
                #endregion

            }
            catch (System.IO.IOException e)
            {
                System.Windows.Forms.MessageBox.Show("Documentul nu poate fi accesat! ");
            }
        }

        //functii secundare
        private void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object toFindText, object replaceWithText)
        {
            object matchCase = true;

            object matchwholeWord = true;

            object matchwildCards = false;

            object matchSoundLike = false;

            object nmatchAllforms = false;

            object forward = true;

            object format = false;

            object matchKashida = false;

            object matchDiactitics = false;

            object matchAlefHamza = false;

            object matchControl = false;

            object read_only = false;

            object visible = true;

            object replace = -2;

            object wrap = 1;

            wordApp.Selection.Find.Execute(ref toFindText, ref matchCase,
                                            ref matchwholeWord, ref matchwildCards, ref matchSoundLike,
                                            ref nmatchAllforms, ref forward,
                                            ref wrap, ref format, ref replaceWithText,
                                            ref replace, ref matchKashida,
                                            ref matchDiactitics, ref matchAlefHamza,
                                            ref matchControl);
        }
        private String gasesteIdProfesor()
        {   //extrage nume persoana logata
            String nume = sesiuneProfesor;
            nume = nume.Remove(0, 7);
            char[] caractere = { '_', ' ' };
            nume = nume.TrimEnd(caractere);
            //gaseste Id in BD
            String id = "Lipsa";
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            String interogare = "SELECT [IdProfesor] FROM [Profesor] WHERE (Nume_Prenume = '" + nume + "' )";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(interogare, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                id = dr[0].ToString();
            }
            dr.Close();
            con.Close();
            return id;
        }
        private String gasesteFunctieProfesor(Int32 id)
        {
            String functie = "Lipsa";
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            String interogare = "SELECT [FunctieBaza_Loc] FROM [Profesor] WHERE (IdProfesor= " + id + " )";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(interogare, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                functie = dr[0].ToString();
            }
            dr.Close();
            con.Close();
            return functie;
        }
        private String gasesteNumeProfesor(Int32 id)
        {
            String nume = "Lipsa";
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            String interogare = "SELECT [Nume_Prenume] FROM [Profesor] WHERE (IdProfesor= " + id + " )";
            con.Open();
            OleDbCommand cmd = new OleDbCommand(interogare, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                nume = dr[0].ToString();
            }
            dr.Close();
            con.Close();
            return nume;
        }

        //functii test
        public void BindData()
        {
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            OleDbCommand cmd;
            String interogare= "";

           foreach (String s in luni)
            {
                interogare = "SELECT Zi FROM Calendar WHERE Orar" + s + " = 'activitate didactica' AND NOT " + s + " = 'Sa' AND NOT " + s + " = 'Du'";
                cmd = new OleDbCommand(interogare, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                // Execute DataReader
                OleDbDataReader dr = cmd.ExecuteReader();
                // Read DataReader till it reaches the end
                while (dr.Read() == true)
                {
                    String data = "";
                    string zi = (dr.GetInt32(0)).ToString();
                    
                    switch (s)
                    {
                        case "Ian":
                            data = zi + "-Jan-" + anulCurent;
                            break;
                        case "Mai":
                            data = zi + "-May-" + anulCurent;
                            break;
                        case "Iun":
                            data = zi + "-Jun-" + anulCurent;
                            break;
                        case "Iul":
                            data = zi + "-Jul-" + anulCurent;
                            break;

                        default:
                            data = zi + "-" + s + "-" + anulCurent;
                            break;                           
                    }
                    Calendar1.SelectedDates.Add(Convert.ToDateTime(data));
                }

                // Close DataReader
                dr.Close();
                // Close database Connection
                con.Close();
            } 
        }
        protected void SetCalendarVisibleDate(object sender, EventArgs e)
        {
            Calendar1.VisibleDate = new DateTime(int.Parse(ddlLuna.SelectedValue), int.Parse(anulCurent),1);
        }

        protected void ddlLuna_SelectedIndexChanged(object sender, EventArgs e)
        {
            Calendar1.VisibleDate = new DateTime(int.Parse(ddlLuna.SelectedValue), int.Parse(anulCurent), 1);

          //  Label1.Text = (Calendar1.VisibleDate);
        }
    }
} 