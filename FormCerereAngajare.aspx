<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormCerereAngajare.aspx.cs" Inherits="WebAppLicenta.FormCerereAngajare" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
       <div class="row">
            <div class="col-sm-7">
            </div>
            <div class="col-sm-5">
                <h2>Generare cerere angajare pe semestru.</h2>
            </div>
       </div>
       <div class="row">
            <div class="col-sm-7">
            </div>
            <div class="col-sm-5">
              <div class="form-group">
                  <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="IdProfesor" DataSourceID="AccessDataSource1" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                      <AlternatingRowStyle BackColor="#F7F7F7" />
                    <Columns>
                        <asp:BoundField DataField="IdProfesor" HeaderText="IdProfesor" ReadOnly="True" SortExpression="IdProfesor" InsertVisible="False" />
                        <asp:BoundField DataField="Nume_Prenume" HeaderText="Nume_Prenume" SortExpression="Nume_Prenume" />
                        <asp:BoundField DataField="FunctieBaza_Loc" HeaderText="FunctieBaza_Loc" SortExpression="FunctieBaza_Loc" />
                        <asp:BoundField DataField="GradEvaluat" HeaderText="GradEvaluat" SortExpression="GradEvaluat" />
                        <asp:BoundField DataField="Departament" HeaderText="Departament" SortExpression="Departament" />
                    </Columns>
                      <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                      <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                      <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                      <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                      <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                      <SortedAscendingCellStyle BackColor="#F4F4FD" />
                      <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                      <SortedDescendingCellStyle BackColor="#D8D8F0" />
                      <SortedDescendingHeaderStyle BackColor="#3E3277" />
                  </asp:GridView>
                  <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
                      DataFile="~/App_Data/Facultate.mdb"
                      SelectCommand="SELECT [IdProfesor], [Nume_Prenume], [FunctieBaza_Loc], [GradEvaluat], [Departament] FROM [Profesor]">
                  </asp:AccessDataSource>
                  <br />
                  <asp:DropDownList class="btn btn-light" ID="ddlProfesori" runat="server" DataSourceID="AccessDataSource1" DataTextField="Nume_Prenume" DataValueField="IdProfesor"></asp:DropDownList>
                  <br />
                  <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                    <asp:ListItem> Semestrul 1</asp:ListItem>
                    <asp:ListItem> Semestrul 2</asp:ListItem>
                  </asp:RadioButtonList>       
              </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-7">
            </div>
            <div class="col-sm-5">
                <div class="form-group">

                </div>
                <asp:Button ID="btnGenereazaCerere" runat="server" style="background-color:#20376d; color:white" Text="Genereaza cerere" OnClick="btnGenereazaCerere_Click"/>
               <br />
                <br />
                <asp:RequiredFieldValidator ID="ValidatorProf" runat="server" ControlToValidate="ddlProfesori" SetFocusOnError="true" ErrorMessage="Selectati Profesor" Display="None"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" ForeColor="#FF3300" runat="server" />
            </div>
       </div>
   </div>
</asp:Content>
