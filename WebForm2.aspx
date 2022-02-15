<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebAppLicenta.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:AccessDataSource ID="adsFisaPost" runat="server" DataFile="~/App_Data/Facultate.mdb" SelectCommand="SELECT Post.NrCrt, Post.PozitieStatFunctie, Post.Post, Disciplina.DenumireD, Profesor.Nume_Prenume, Profesor.FunctieBaza_Loc, Profesor.GradEvaluat, Disciplina.[ProgramStudii/An/Serie], Disciplina.Grupe, Disciplina.NrOreCurs, Disciplina.NrOreLaborator, Disciplina.NrOreSeminar, Disciplina.NrOreSemestru, Disciplina.NumarSaptamani FROM ((Post INNER JOIN Disciplina ON Post.NrCrt = Disciplina.NrCrt) INNER JOIN Profesor ON Disciplina.IdProfesor = Profesor.IdProfesor)"></asp:AccessDataSource>        
        <div>
            <table>
                <tr>
                <td>Selecteaza fisier:</td>
                <td>
                    <asp:FileUpload ID="uplFisaPosturi" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnImport" runat="server" Text="Import" OnClick="btnImport_Click" />
                </td>
                </tr>
            </table>
            
            <div>
                <br />
                <asp:Label ID="lblMesaj" runat="server" Text="Label" Font-Bold="true"></asp:Label>

            </div>
        </div>
    </form>
</body>
</html>
