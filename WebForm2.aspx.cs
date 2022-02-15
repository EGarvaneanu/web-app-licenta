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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        

        
        protected void btnImport_Click(object sender, EventArgs e)
        {
            int NrCrt;
            int PozitieStatFunctie;
            String Disciplina;
            String Nume_Prenume;

            String path = Path.GetFileName(uplFisaPosturi.FileName);
            path = path.Replace(" ", "");
            uplFisaPosturi.SaveAs(Server.MapPath("~/ImportDocument/") + path);
            String ExcelPath = Server.MapPath("~/ImportDocument/") + path;
           
            OleDbConnection conn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + ExcelPath + "; Excel 8.0; Persist Security Info = False");
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Foaie1$]", conn);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                NrCrt = Convert.ToInt32(dr[0].ToString());
                PozitieStatFunctie = Convert.ToInt32(dr[1].ToString());
                Disciplina = dr[2].ToString();
                Nume_Prenume = dr[3].ToString();
                salveazaDate(NrCrt, PozitieStatFunctie, Disciplina, Nume_Prenume);
            }
            lblMesaj.Text = "Datele au fost salvate cu succes";
            /*if (uplFisaPosturi.PostedFile.ContentType == "application/vnd.ms-excel" || 
                uplFisaPosturi.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                try
                {
                    string fileName = Path.Combine(Server.MapPath("~/ImportDocument"), Guid.NewGuid().ToString() + Path.GetExtension(uplFisaPosturi.PostedFile.FileName));
                    uplFisaPosturi.PostedFile.SaveAs(fileName);

                    string conString = "";
                    string ext = Path.GetExtension(uplFisaPosturi.PostedFile.FileName);
                    if (ext.ToLower() == ".xls")
                    {
                        conString = "Provider= Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel8.0;HDR=Yes;IMEX=2\";"; ;     
                    }
                    else if (ext.ToLower() == ".xlsx")
                    {
                        conString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + fileName + "; Extended Properties =\"Excel 12.0 Macro;HDR=YES\";"; ;
                    }

                    //setam conexiune cu fisierul
                    string query = "SELECT * FROM [Foaie1$]";
                    OleDbConnection con = new OleDbConnection(conString);
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    con.Close();
                    con.Dispose();

                    string conStringAccess = string.Format("Data Source= adsFisaPost; Provider=Microsoft.Jet.OLEDB.4.0; Persist security Info = false");

                    //import in baza de date 
                    OleDbConnection conAccess = new OleDbConnection();
                    OleDbCommand cmdAccess = conAccess.CreateCommand();
                    cmdAccess.CommandType = CommandType.Text;
                    cmdAccess.CommandText = "INSERT INTO Post (NrCrt, PozitieStatFunctie, Post,) VALUES(@column1, @column2, @column3)" + "INSERT INTO Disciplina (DenumireD, [ProgramStudii/An/Serie], Grupe, NrOreCurs, NrOreLaborator, NrOreSeminar, NrOreSemestru, NumarSaptamani) VALUES(@column4, )" + "INSERT INTO Profesor (Nume_Prenume, FunctieBaza_Loc, GradEvaluat) VALUES(@column5, @column6, @column7)";

                }
                catch (Exception)
                {
                    throw;
                }
                try
                {

                }
                catch (Exception)
                {

                }
            }
            */
        }

        private void salveazaDate(int NrCrt1, int PozitieStatFunctie1, String Disciplina1, String Nume_Prenume1)
        {
            OleDbConnection con;
            OleDbCommand com;
            con = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + Server.MapPath("~/App_Data/Facultate.mdb") + "; Persist Security Info = False");
            con.Open();
            String date;
            date = "INSERT INTO [tableTest] ([NrCrt], [PozitieStatFunctie], [Disciplina], [Nume_Prenume]) VALUES ( " + NrCrt1 + ", " + PozitieStatFunctie1 + ", '" + Disciplina1 + "', '" + Nume_Prenume1 + "' );";

            com = new OleDbCommand(date, con);
            com.ExecuteNonQuery();
            con.Close();
            lblMesaj.Text = "Datele au fost incarcate  cu succes in baza de date";
        }
    }
}