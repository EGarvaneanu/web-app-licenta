<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormModificareParola.aspx.cs" Inherits="WebAppLicenta.FormModificareParola" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <style>body {background-image: linear-gradient(rgba(0,0,0,.7), rgba(0,0,0,.7)),url('../Imagini/backgroundOvidius.jpeg');background-repeat: repeat-y;background-size: 1920px 1080px;}</style>
    <!--Modificare parola-->
    <div class="container">
    <div class="row justify-content-center align-items-center" style="height:100vh">
            <div class="col-4">
                <div class="card">
                    <div class="card-head">
                        <h2 class="text-center">Modificare parola</h2>
                    </div>
                    <div class="card-body">

                            <div class="form-group">
                                <label for="txtParola">Parola veche:</label>
                                <asp:TextBox ID="txtParolaVeche" class="form-control" runat="server" placeholder="Introduceti parola" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtParola">Parola noua:</label>
                                <asp:TextBox ID="txtParolaNoua" class="form-control" runat="server" placeholder="Introduceti parola" TextMode="Password"></asp:TextBox>
                            </div>
                            <br />
                            <asp:Button ID="btnModificareParola"  class="btn btn-primary" runat="server" Text="Modifica" OnClick="btnModificareParola_Click" /> <br />
                       
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
