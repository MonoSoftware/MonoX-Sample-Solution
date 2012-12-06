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
using MonoSoftware.MonoX.Resources;

namespace ProjectName.Web
{
    #region Demo Data Class
    /// <summary>
    /// Class is serializable due to MonoX ViewState optimization that in some cases needs this flag.
    /// </summary>
    [Serializable]
    public class DemoDataSide
    {
        public bool IsModerator { get; set; }
    }
    #endregion

    public partial class SideContentTemplate : ProjectName.Web.BaseUserControl
    {
        #region Fields
        /// <summary>
        /// Data manager used to transfer and convert data to and from controls. It can handle control and validators visibility.
        /// </summary>
        private DataManager dataManagerMain = new DataManager();        
        #endregion

        #region Properties
        private Guid _userId;
        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        /// <summary>
        /// Gets or sets a flag if module is initially in preview mode.
        /// </summary>
        public bool IsPreviewMode
        {
            get
            {
                if (ViewState["IsPreviewMode"] != null)
                    return (bool)ViewState["IsPreviewMode"];
                return true;
            }
            set
            {
                ViewState["IsPreviewMode"] = value;
            }
        }

        /// <summary>
        /// Gets or sets demo data.
        /// </summary>
        private DemoDataSide DemoDataObject
        {
            get
            {
                if (ViewState["DemoDataObject"] != null)
                    return (DemoDataSide)ViewState["DemoDataObject"];
                return null;
            }
            set
            {
                ViewState["DemoDataObject"] = value;
            }
        }
        #endregion

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            //Note: UserId will be auto populated by the UserProfileModule if this user control is placed in one of UserProfileModule templates
            //Note: DataBind will be automatically called by the UserProfileModule if this user control is placed in one of UserProfileModule templates
            labViewProfile2.InnerText = UserProfileResources.UserProfileModule_ViewProfile;
            labEditProfile2.InnerText = UserProfileResources.UserProfileModule_EditProfile;
            lnkViewProfile2.Click += new EventHandler(lnkViewProfile_Click);
            lnkEditProfile2.Click += new EventHandler(lnkEditProfile_Click);
            btnSave.Click += new EventHandler(btnSave_Click);

            dataManagerMain.DataBindings.Add(IsModerator, null, prevIsModerator, null, "IsModerator"); 
            
            Page.LoadComplete += new EventHandler(Page_LoadComplete);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dataManagerMain.IsPreviewMode = IsPreviewMode;
            if (!Page.IsPostBack)
            {
                DemoDataObject = new DemoDataSide();
                DemoDataObject.IsModerator = false;
            }
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            dataManagerMain.InitControlVisibility();
            btnSave.Visible = !IsPreviewMode;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Module databind.
        /// </summary>
        public override void DataBind()
        {
            //base.DataBind();
            DataBind(!Page.IsPostBack);
        }

        /// <summary>
        /// Module databind.
        /// </summary>
        /// <param name="rebind">Refetch the user data</param>
        public new void DataBind(bool rebind)
        {
            dataManagerMain.IsPreviewMode = IsPreviewMode;
            dataManagerMain.TransferDataToControls(DemoDataObject);
            if (rebind)
            {
                
            }
            else
            {                
                dataManagerMain.InitControlVisibility();
            }
        }
        #endregion

        #region UI Events
        void lnkEditProfile_Click(object sender, EventArgs e)
        {
            IsPreviewMode = false;
        }

        void lnkViewProfile_Click(object sender, EventArgs e)
        {
            IsPreviewMode = true;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            dataManagerMain.TransferDataFromControls(DemoDataObject);
            //Here goes your custom data save code
            IsPreviewMode = true;
        }
        #endregion
    }
}
