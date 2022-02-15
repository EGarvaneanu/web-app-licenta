<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormIncarcareDocumente.aspx.cs" Inherits="WebAppLicenta.FormIncarcareDocumente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="container">
       <div class="row">
            <div class="col-sm-7">
            </div>
            <div class="col-sm-5">
                <h1>Incarcare documente</h1>
            </div>
       </div>
        <div class="row">
            <div class="col-sm-7">
            </div>
            <div class="col-sm-5">
                <div class="form-group">
                    <label for="uplFisePlataOra">Selectati documentul care contine tabelele de plata cu ora: </label>
                    <br />
                   <asp:FileUpload ID="uplFisePlataOra" runat="server" />
                </div>
                <asp:Button ID="btnImport1" runat="server" style="background-color:#20376d; color:white" Text="Salveaza date" OnClick="btnImport1_Click"/>
                <asp:Button ID="btnAfisare1" runat="server" style="background-color:#20376d; color:white" Text="Afisare date" OnClick="btnAfisare1_Click" />
                <asp:Button ID="btnStergere" runat="server" style="background-color:#20376d; color:white" Text="Sterge date vechi" OnClick="btnStergere_Click"/>                              
                <br />
                <asp:Label style="color:crimson" ID="lblEroare" runat="server" Text=" "></asp:Label>
                <br />
                <br />
            </div>
        </div>
        <div class="row">
            <div class="col-sm-7">
            </div>
            <div class="col-sm-5">
                <div class="form-group">
                    <label for="uplCalendar">Selectati calendar </label>
                    <br />
                    <asp:FileUpload ID="uplCalendar"  runat="server" />
                    <br />
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" ondayrender="MyDayRenderer">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>
                </div>
                <asp:Button ID="btnImport2" runat="server" style="background-color:#20376d; color:white" Text="Salveaza date" OnClick="btnImport2_Click"/>
                <asp:Button ID="btnAfisare2" runat="server" style="background-color:#20376d; color:white" Text="Afisare date" OnClick="btnAfisare2_Click" />
                <asp:Button ID="btnStergere2" runat="server" style="background-color:#20376d; color:white" Text="Sterge date vechi" OnClick="btnStergere2_Click"/>                              
                
                <br />
                <asp:Label style="color:crimson" ID="lblEroare2" runat="server" Text=" "></asp:Label>
                <br />
                <br />
            </div>
        </div>
   </div>
   <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
        <AlternatingRowStyle BackColor="#F7F7F7" />
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

</asp:Content>
