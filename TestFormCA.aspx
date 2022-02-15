<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TestFormCA.aspx.cs" Inherits="WebAppLicenta.TestFormCA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="IdProfesor" DataSourceID="AccessDataSource1">
        <Columns>
            <asp:BoundField DataField="IdProfesor" HeaderText="IdProfesor" ReadOnly="True" SortExpression="IdProfesor" InsertVisible="False" />
            <asp:BoundField DataField="Nume_Prenume" HeaderText="Nume_Prenume" SortExpression="Nume_Prenume" />
            <asp:BoundField DataField="FunctieBaza_Loc" HeaderText="FunctieBaza_Loc" SortExpression="FunctieBaza_Loc" />
            <asp:BoundField DataField="GradEvaluat" HeaderText="GradEvaluat" SortExpression="GradEvaluat" />
            <asp:BoundField DataField="Departament" HeaderText="Departament" SortExpression="Departament" />
        </Columns>
    </asp:GridView>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server"

            DataFile="~/App_Data/Facultate.mdb"

            SelectCommand="SELECT [IdProfesor], [Nume_Prenume], [FunctieBaza_Loc], [GradEvaluat], [Departament] FROM [Profesor]">

    </asp:AccessDataSource>
    <asp:Button ID="btnGenereazaCerere" runat="server" Text="Genereaza cerere de angajare" OnClick="btnGenereazaCerere_Click" />
    <asp:DropDownList ID="ddlProfesori" runat="server" DataSourceID="AccessDataSource1" DataTextField="Nume_Prenume" DataValueField="IdProfesor"></asp:DropDownList>
</asp:Content>
