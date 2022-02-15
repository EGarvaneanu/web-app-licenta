using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Table = Xceed.Document.NET.Table;

namespace WebAppLicenta
{
    public partial class TestFormCA : System.Web.UI.Page
    {
        String anulCurent = DateTime.Now.Year.ToString();
        String anulUrmator = (DateTime.Now.Year + 1).ToString();
        String semestru = " ";
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();


        }

        protected void btnGenereazaCerere_Click(object sender, EventArgs e)
        {
            String modelDoc = @"D:\cerereAngajare.docx";
            String docNou = @"D:\cerereAngajareNoua.doc";
            adaugaTabel(modelDoc, docNou);
            adaugaInformatiiText(docNou);

            Process.Start("WINWORD.EXE", docNou);
        }

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

        private void adaugaInformatiiText(object filename)
        {
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
                this.FindAndReplace(wordApp, "numeProfesor", gasesteNumeProfesor());
                this.FindAndReplace(wordApp, "functieProfesor", gasesteFunctieProfesor()); ;
                this.FindAndReplace(wordApp, "anulUniversitar", anulCurent + "-" + anulUrmator);
                this.FindAndReplace(wordApp, "sem", semestru);


                myWordDoc.Save();
                myWordDoc.Close();
                wordApp.Quit();
            }
        }

        private void adaugaTabel(String modelDoc, String docNou)
        {
            #region Initializare Document
            var doc = DocX.Load(modelDoc);
            #endregion

            #region Tabel
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            String datePost = "SELECT [PozitieStatFunctie], [Post], [NrOreCurs], [NrOreLaborator], [NrOreSeminar], [DenumireD], [ProgramStudii], [An_Serie_Grupe] " +
                "FROM Post INNER JOIN Disciplina ON Post.NrCrt = Disciplina.NrCrt " +
                "WHERE (Post.IdProfesor= " + ddlProfesori.SelectedValue + " )";
            String nrRanduri = "SELECT COUNT ( [NrCrt] ) FROM [Post] WHERE (IdProfesor= " + ddlProfesori.SelectedValue + " )";
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
            Table t = doc.AddTable(randuri + 1, 6);

            t.Alignment = Alignment.center;
            //Capul tabelului
            t.Rows[0].Cells[0].Paragraphs.First().Append("Post și Poziție Stat de funcții");
            t.Rows[0].Cells[1].Paragraphs.First().Append("Nr. de ore curs");
            t.Rows[0].Cells[2].Paragraphs.First().Append("Nr. de ore aplicaţii");
            t.Rows[0].Cells[3].Paragraphs.First().Append("Disciplina");
            t.Rows[0].Cells[4].Paragraphs.First().Append("Program de studii");
            t.Rows[0].Cells[5].Paragraphs.First().Append("Anul, Seria și Grupa");
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
                rand++;
            }
            doc.InsertTable(t);
            con.Close();
            #endregion

            #region Salvare document
            doc.SaveAs(docNou);
            #endregion
        }
        private String gasesteFunctieProfesor()
        {
            String functie="Lipsa";
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            String interogare = "SELECT [FunctieBaza_Loc] FROM [Profesor] WHERE (IdProfesor= " + ddlProfesori.SelectedValue + " )";
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
        private String gasesteNumeProfesor()
        {
            String nume = "Lipsa";
            OleDbConnection con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            String interogare = "SELECT [Nume_Prenume] FROM [Profesor] WHERE (IdProfesor= " + ddlProfesori.SelectedValue + " )";
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
    }
}