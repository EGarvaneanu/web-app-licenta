<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormInregistrare.aspx.cs" Inherits="WebAppLicenta.FormInregistrare" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>body {background-image: linear-gradient(rgba(0,0,0,.7), rgba(0,0,0,.7)),url('../Imagini/backgroundOvidius.jpeg');background-repeat: repeat-y;background-size: 1920px 1080px;}</style>
    <!--Inregistrare-->
    <div class="container">
        <div class="row justify-content-center align-items-center" style="height:100vh">
            <div class="col-4">
               <div class="card">
                 <div class="card-head">
                        <h2 class="text-center">Inregistrare</h2>
                 </div>
                 <div class="card-body">
                    <div class="form-group">
                        <label for="txtNume">Nume:</label>
                        <asp:TextBox ID="txtNume" class="form-control" runat="server" placeholder="Nume"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtPrenume">Prenume:</label>
                        <asp:TextBox ID="txtPrenume" class="form-control" runat="server" placeholder="Prenume"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="ddlGrad">Grad didactic evaluat:</label>
                        <br />
                        <asp:DropDownList ID="ddlGrad" class="form-control" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="conf." Value="0" />
                            <asp:ListItem Text="lect." Value="0" />
                            <asp:ListItem Text="prof." Value="0" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="txtEmail">Adresa email:</label>
                        <asp:TextBox ID="txtEmail" class="form-control" runat="server" TextMode="Email" aria-describedby="informatieParola" placeholder="Introduceti email"></asp:TextBox>
                        <small id="informatieParola" class="form-text text-muted">Parola va fi generata automat si trimisa pe email.</small>
                    </div>
                    <br />
                    <asp:Button ID="btnInregistrare" class="btn btn-primary" type="submit" runat="server" Text="Inregistreaza" OnClick="btnInregistrare_Click" />
                    <asp:Label ID="lblParola" runat="server" Text=" "></asp:Label>
                     
                    <asp:RequiredFieldValidator ID="ValidatorNume" runat="server" ControlToValidate="txtNume" ErrorMessage="Introduceti Nume" SetFocusOnError="true" Display="none"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ValidatorPrenume" runat="server" ControlToValidate="txtPrenume" ErrorMessage="Introduceti Prenume" SetFocusOnError="true" Display="none"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ValidatorGrad" runat="server" ControlToValidate="ddlGrad" ErrorMessage="Introduceti Gradul Didactic" SetFocusOnError="true" Display="none"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ValidatorEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Introduceti Email" SetFocusOnError="true" Display="none"></asp:RequiredFieldValidator>
                     <asp:ValidationSummary ID="ValidationSummary1" ForeColor="#FF3300" runat="server" />
               </div>
             </div>
            </div>
        </div>
    </div>
</asp:Content>