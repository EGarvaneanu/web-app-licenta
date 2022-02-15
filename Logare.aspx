<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logare.aspx.cs" Inherits="WebAppLicenta.Logare" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css"/>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" />
    <style>body {background-image: linear-gradient(rgba(0,0,0,.7), rgba(0,0,0,.7)),url('../Imagini/backgroundOvidius.jpeg');background-repeat: repeat-y;background-size: 1920px 1080px;}</style>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" ></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" ></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <title>Logare</title>
</head>
<body>
    <div class="container">
        <div class="row justify-content-center align-items-center" style="height:100vh">
            <div class="col-4">
                <div class="card">
                    <div class="card-head">
                        <h2 class="text-center">Logare</h2>
                    </div>
                    <div class="card-body">
                        <form id="form1" runat="server" autocomplete="off">
                            <div class="form-group">
                                <label for="txtEmailLog">Adresa email:</label>
                                <asp:TextBox ID="txtEmailLog" class="form-control" runat="server" TextMode="Email" placeholder="Introduceti email"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtParola">Parola:</label>
                                <asp:TextBox ID="txtParola" class="form-control" runat="server" placeholder="Introduceti parola" TextMode="Password"></asp:TextBox>
                            </div>
                            <br />
                            <asp:Button ID="btnLogare"  class="btn btn-primary" runat="server" Text="Logare" OnClick="btnLogare_Click" /> <br />
                            <asp:RequiredFieldValidator ID="ValidatorMail" runat="server" ErrorMessage="Introduceti Email" ControlToValidate="txtEmailLog" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="ValidatorParola" runat="server" ErrorMessage="Introduceti Parola" ControlToValidate="txtParola" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary1" ForeColor="#FF3300" runat="server" />
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
