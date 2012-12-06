using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MonoSoftware.MonoX;
using MonoSoftware.MonoX.Utilities;

namespace ProjectName.Web.WebParts
{
    public partial class HelloWorld : ProjectName.Web.BaseAutoRegisterPart
    {
        private string _name;
        [WebBrowsable(true), Personalizable(true)]
        [WebDescription("Test property for the Web part sample with the default editor for string properties.")]
        [WebDisplayName("Web part name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _story;
        [WebBrowsable(true), Personalizable(true), WebEditor(typeof(HtmlEditorPart))]
        [WebDescription("Test property for the Web part sample with the custom editor.")]
        [WebDisplayName("Please enter a story")]
        public string Story
        {
            get { return _story; }
            set { _story = value; }
        }
	
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
                lblGreeting.Text = "Please enter your name!";
            else if (string.IsNullOrEmpty(this.Name))
                lblGreeting.Text = string.Format("Hello, {0}. You can enter my name by choosing Properties from the Web part menu, and entering name in the Web part name field of the Module properties tab in the administrator's toolbar", txtName.Text, this.Name);
            else
                lblGreeting.Text = string.Format("Hello, {0}. I am {1}. Nice to meet you!", txtName.Text, this.Name);

            if (string.IsNullOrEmpty(this.Story))
                lblGreeting.Text += "<br />Please tell me a story by using the Story property... Note that the HTML editor is used to edit this property.";
            else
                lblGreeting.Text += string.Format("<br /><br />Here is your story:<br />{0}", this.Story);
        }

    }
}
