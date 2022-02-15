using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppLicenta
{
    public partial class Homepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String sesiuneDirector = "_" + (string)Session["Director"] + "_";
            String sesiuneProfesor = "_" + (string)Session["Profesor"] + "_";
            initializeazaSesiune(sesiuneProfesor, sesiuneDirector);
        }

        private void initializeazaSesiune(String sesiuneProfesor, String sesiuneDirector)
        {
            if (sesiuneProfesor == "__")
            {
                if (sesiuneDirector == "__")
                {
                    statusLogare(false);
                }
                else if (sesiuneDirector != "__")
                {
                    statusLogare(true);
                    functiiDir(true);
                }
            }
            else if (sesiuneProfesor != "__" && sesiuneDirector == "__")
            {
                statusLogare(true);
                functiiDir(false);
            }
        }

        private void functiiDir(Boolean director)
        {
            if (director == true)
            {
     
                ((Button)Master.FindControl("btnInregistrare")).Visible = true;
                ((Button)Master.FindControl("btnIncarcareDocumente")).Visible = true;
            }
            else if (director == false)
            {
                ((Button)Master.FindControl("btnInregistrare")).Visible = false;
                ((Button)Master.FindControl("btnIncarcareDocumente")).Visible = false;
            }
        }

        private void statusLogare(Boolean logat)
        {
            if (logat == true)
            {
                ((Button)Master.FindControl("btnProfil")).Visible = true;
                ((Button)Master.FindControl("btnSchimbaParola")).Visible = true;
                ((Button)Master.FindControl("btnDeconectare")).Visible = true;
                ((Button)Master.FindControl("btnLogare")).Visible = false;
                ((Button)Master.FindControl("btnResetParola")).Visible = false;
            }
            else
            {
                Response.Redirect("Logare.aspx");
            }
        }

        protected void btnModificaParola_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormModificareParola.aspx");
        }
    }
}