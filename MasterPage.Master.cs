using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppLicenta
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LnkDeconectare_Click(object sender, EventArgs e)
        {

        }


        protected void LnkIncarcareDocumente_Click(object sender, EventArgs e)
        {

             Server.Transfer("FormIncarcareDocumente.aspx");
        }

        protected void LnkInregistrare_Click(object sender, EventArgs e)
        {
            //Response.Redirect("FormInregistrare.aspx");
            Server.Transfer("FormInregistrare.aspx");
        }

        protected void btnCerereAngajare_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormCerereAngajare.aspx");
        }

        protected void btnDeconectare_Click(object sender, EventArgs e)
        {
            String sesiune = "_" + (string)Session["Profesor"] + "_";
            sesiune = "_" + (string)Session["Director"] + "_";

            if (sesiune != null)
            {
                btnProfil.Visible = false;
                btnSchimbaParola.Visible = false;
                btnDeconectare.Visible = false;
                btnLogare.Visible = true;
                btnResetParola.Visible = true;
                Session.Clear();
                Session.Abandon();
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
            }

            Server.Transfer("Logare.aspx");
        }


        protected void btnResetParola_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormResetareParola.aspx");
        }

        protected void btnLogare_Click(object sender, EventArgs e)
        {
            Server.Transfer("Logare.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }

        protected void btnProfil_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormProfil.aspx");
        }

        protected void btnSchimbaParola_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormModificareParola.aspx");
        }

        protected void btnGenerareReferate_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormReferatPO.aspx");
        }

        protected void btnModificareReferate_Click(object sender, EventArgs e)
        {
            Server.Transfer("FormReferatPO.aspx");
        }
    }
}