<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormProfil.aspx.cs" Inherits="WebAppLicenta.FormProfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="row row d-flex justify-content-center">
        <div class="col-md-10 mt-5 pt-5">
              <div class="row z-depth-3">
                 	<div class="col-sm-4 bg-light rounded-left">
                            <asp:Image ID="imgUser" runat="server" ImageUrl="~/Imagini/defaultUser.png" />
                            <br />
                            <h3>lect.</h3>
                    		<h2 class="font-weight-bold mt-4">Nume Profesor</h2>                    		
                            <asp:LinkButton ID="btnModificaPoza" runat="server"><i class="far fa-edit fa-2x mb-4"></i></asp:LinkButton>
                	</div>
            		<div class="col-sm-8 bg-white rounded-right">
                    	<h3 class="mt-3 text-center">Detalii utilizator</h3>
                    	<hr class="bg-primary mt-0 w-25">
                   		<div class="row">
                        	<div class="col-sm-6">
                            	<p class="font-weight-bold">Email:</p>
                           		<h6 class=" text-muted">nick32@gmail.com</h6>
                        	</div>
                        	<div class="col-sm-6">
                            	<p class="font-weight-bold">Departament:</p>
                           		<h6 class="text-muted">CS</h6>
                        	</div>
                    	</div>
                    		<h4 class="mt-3">Projects</h4>
                    		<hr class="bg-primary">
                   		<div class="row">
                        	<div class="col-sm-6">
                           		<p class="font-weight-bold">Functie de baza:</p>
                          	  	<h6 class="text-muted">Lect.Doct./UOC</h6>
                        	</div>
                        	<div class="col-sm-6">
                                <br />
                                <asp:Button class="btn btn-primary" ID="btnModificaParola" runat="server" Text="Schimba parola" />
                        	</div>
                    	</div>
              		</div>
             </div>
        </div>
    </div>
    </div>
</asp:Content>
