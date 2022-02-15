<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormReferatPO.aspx.cs" Inherits="WebAppLicenta.FormReferatPO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
       <div class="row justify-content-center align-items-center">
                <h2>Generare referat de plata cu ora pe semestru</h2>
       </div>
       <br /><br />
       <div class="row">
            <div class="col-sm-7">       
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
            </div>
            <div class="col-sm-5">
              <div class="form-group">
                  <h5><asp:Label ID="lblProfesori" style="color:darkslateblue" runat="server" Text="Selecteaza profesor:"></asp:Label></h5>
                  <asp:DropDownList class="btn btn-light" ID="ddlProfesori" runat="server" DataSourceID="AccessDataSource1" DataTextField="Nume_Prenume" DataValueField="IdProfesor"></asp:DropDownList>
                  <h5><asp:Label ID="Label3" style="color:darkslateblue" runat="server" Text="Selecteaza luna:"></asp:Label></h5>                                   
                  <asp:DropDownList class="btn btn-light" ID="ddlLuna" runat="server" OnSelectedIndexChanged="ddlLuna_SelectedIndexChanged"></asp:DropDownList>
              </div>
              <div class="form-group">
                  <h5><asp:Label ID="Label2" style="color:darkslateblue" runat="server" Text="Adauga inregistrari in tabelul de plata"></asp:Label></h5>                 
                  <h6><asp:Label ID="Label4" style="color:darkslateblue" runat="server" Text="Selecteaza disciplina:"></asp:Label></h6>                                   
                  <asp:DropDownList class="btn btn-light" ID="ddlDisciplina" runat="server" DataSourceID="AccessDS2" DataTextField="DenumireD" DataValueField="IdDisciplina"></asp:DropDownList>
                  <asp:SqlDataSource ID="AccessDS2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" SelectCommand="SELECT [IdDisciplina], [DenumireD] FROM [Disciplina]"></asp:SqlDataSource>
                  <br />
                  <h6><asp:Label ID="Label5" style="color:darkslateblue" runat="server" Text="Selecteaza data:"></asp:Label></h6>                                          
                  <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px">
                      <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                      <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                      <OtherMonthDayStyle ForeColor="#999999" />
                      <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                      <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                      <TodayDayStyle BackColor="#CCCCCC" />
                  </asp:Calendar>                                
                  <h6><asp:Label ID="Label6" style="color:darkslateblue" runat="server" Text="Introdu intervalul orar:"></asp:Label></h6>
                  <asp:TextBox ID="txtOraInceput" TextMode="Time" runat="server"></asp:TextBox>
                  <asp:Label ID="Label7" runat="server" Text="-"></asp:Label>
                  <asp:TextBox ID="txtOraSfrasit" TextMode="Time" runat="server"></asp:TextBox>
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
            </div>
       </div>
   </div>
</asp:Content>
