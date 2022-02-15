<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormResetareParola.aspx.cs" Inherits="WebAppLicenta.FormResetareParola" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!--Modal pt "mi-am uitat parola"-->
            <div class="modal fade" id="myModal" role="dialog" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Notificare</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                         </div>
                        <div class="modal-body">
                            <p>Directorul departamentului dumneavoastra va fi anuntat!</p> 
                            <p>Veti primi pe e-mail o parola noua cat mai curand.
                            <p>Recomandam schimbarea parolei dupa prima logare.</p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div> 
        
            <div class="container">
                <div class="row">
                    <div class="col text-center">
                       <asp:Panel ID="pnlResetParola" runat="server">
                            <h1><asp:Label ID="Label1" runat="server" Text="Resetare parola"></asp:Label></h1>
                            <div class="form-group">
                                <label ID="lblEmail" for="txtEmail">Adresa e-mail:</label>
                                <asp:TextBox ID="txtEmail" class="form-control" runat="server" TextMode="Email" aria-describedby="informatieParola" placeholder="Introduceti email"></asp:TextBox>
                                <small id="informatieParola" class="form-text text-muted">Veti primi parola noua pe e-mail</small>
                                </div>
                           <asp:Label ID="lblTest" runat="server" Text="Label"></asp:Label>
                            <asp:Button ID="btnTrmitere" runat="server" Text="Trimite" CssClass="btn btn-info btn-lg" OnClick="btnTrmitere_Click"/>
                            <asp:Button ID="btnInfo" runat="server" Text="Informatii" CssClass="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal" OnClientClick="return false;"/>
                       </asp:Panel>
                    </div>
                </div>
           </div>
</asp:Content>
