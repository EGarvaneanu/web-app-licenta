<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="WebAppLicenta.Homepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br /><br />
   <div class="container">
        <div class="row justify-content-center">
            <div class="col-sm-12">
                <div class="card border-warning">
                    <div class="card-header text-warning">
                        <h5>Recomandare!</h5>
                    </div>
                    <div class="card-body text-warning">                      
                        <p class="card-text">Dupa prima logare este recomandata modificarea parolei</p>
                        <asp:Button class="btn btn-warning" ID="btnModificaParola" runat="server" Text="Modifica Parola" OnClick="btnModificaParola_Click" />
                    </div>
                </div>
            </div>
        </div>
       <br /><br />
</div>
        <div class="card border-info">
        <div class="card-header text info">
           <h5>Calendarul academic:</h5>
        </div>
                    <div class="card-body text-info">                      
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Zi" DataSourceID="sqlCalendar">
                            <Columns>
                                <asp:BoundField DataField="Zi" HeaderText="Zi" ReadOnly="True" SortExpression="Zi" />
                                <asp:BoundField DataField="Oct" HeaderText="Oct" SortExpression="Oct" />
                                <asp:BoundField DataField="OrarOct" HeaderText="OrarOct" SortExpression="OrarOct" />
                                <asp:BoundField DataField="Nov" HeaderText="Nov" SortExpression="Nov" />
                                <asp:BoundField DataField="OrarNov" HeaderText="OrarNov" SortExpression="OrarNov" />
                                <asp:BoundField DataField="Dec" HeaderText="Dec" SortExpression="Dec" />
                                <asp:BoundField DataField="OrarDec" HeaderText="OrarDec" SortExpression="OrarDec" />
                                <asp:BoundField DataField="Ian" HeaderText="Ian" SortExpression="Ian" />
                                <asp:BoundField DataField="OrarIan" HeaderText="OrarIan" SortExpression="OrarIan" />
                                <asp:BoundField DataField="Feb" HeaderText="Feb" SortExpression="Feb" />
                                <asp:BoundField DataField="OrarFeb" HeaderText="OrarFeb" SortExpression="OrarFeb" />
                                <asp:BoundField DataField="Mar" HeaderText="Mar" SortExpression="Mar" />
                                <asp:BoundField DataField="OrarMar" HeaderText="OrarMar" SortExpression="OrarMar" />
                                <asp:BoundField DataField="Apr" HeaderText="Apr" SortExpression="Apr" />
                                <asp:BoundField DataField="OrarApr" HeaderText="OrarApr" SortExpression="OrarApr" />
                                <asp:BoundField DataField="Mai" HeaderText="Mai" SortExpression="Mai" />
                                <asp:BoundField DataField="OrarMai" HeaderText="OrarMai" SortExpression="OrarMai" />
                                <asp:BoundField DataField="Iun" HeaderText="Iun" SortExpression="Iun" />
                                <asp:BoundField DataField="OrarIun" HeaderText="OrarIun" SortExpression="OrarIun" />
                                <asp:BoundField DataField="Iul" HeaderText="Iul" SortExpression="Iul" />
                                <asp:BoundField DataField="OrarIul" HeaderText="OrarIul" SortExpression="OrarIul" />
                                <asp:BoundField DataField="Aug" HeaderText="Aug" SortExpression="Aug" />
                                <asp:BoundField DataField="OrarAug" HeaderText="OrarAug" SortExpression="OrarAug" />
                                <asp:BoundField DataField="Sep" HeaderText="Sep" SortExpression="Sep" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="sqlCalendar" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [Calendar]"></asp:SqlDataSource>
                    </div>
        </div>

</asp:Content>
