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
using MonoSoftware.MonoX.DAL.EntityClasses;
using MonoSoftware.MonoX.Resources;
using MonoSoftware.MonoX;
using MonoSoftware.MonoX.BusinessLayer;
using MonoSoftware.MonoX.Utilities;
using MonoSoftware.MonoX.Repositories;

namespace ProjectName.Web
{
    public partial class MonoXProfileSample : ProjectName.Web.BasePage
    {
        #region Properties
        private UserProfileEntity CurrentUser { get; set; }        
        #endregion

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            #region Demo code used for Visibility, Titles etc.
            ctlProfile.ShowASPNETProfile = false;
            ctlProfile.MyStatusChanged += new EventHandler(ctlProfile_MyStatusChanged);
            ctlProfile.Title = PageResources.UserProfile_UserProfile_Title;
            snPeopleSearch.InfoText = PageResources.UserProfile_PeopleSearch_InfoText;
            ctlInvitationsReceived.Title = PageResources.Module_InvitationsReceived;
            ctlInvitationsSent.Title = PageResources.Module_InvitationsSent;

            string userName = string.Empty;
            if (UrlParams.UserProfile.UserName.HasValue)
                userName = UrlParams.UserProfile.UserName.Value;
            ctlProfile.ShowWorkingModeSwitch = false;
            Guid userId = SecurityUtility.GetUserId(userName);
            if (userId.Equals(Guid.Empty) && Page.User.Identity.IsAuthenticated)
            {
                userId = SecurityUtility.GetUserId();
                userName = Membership.GetUser(userId).UserName;
            }
            snFriendList.Visible = false;
            snWallNotes.Visible = false;
            discussionTopicMessages.Visible = false;
            ctlInvitationsSent.Visible = false;
            ctlInvitationsReceived.Visible = false;
            if (!String.IsNullOrEmpty(userName) && !Guid.Empty.Equals(userId))
            {
                snPeopleSearch.Title = string.Format(PageResources.UserProfile_PeopleSearch_Title, userName);
                snWallNotes.Title = String.Format(PageResources.Module_WallNotes, userName);
                this.SetPageTitle(String.Format(MonoSoftware.MonoX.Resources.PageResources.UserProfile_Title, userName));
                CurrentUser = UserProfileBLL.GetInstance().GetCachedUserProfile(userId);
                if (CurrentUser != null)
                {
                    string nameToShow = (CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.FirstName) ? CurrentUser.FirstName : CurrentUser.AspnetUser.UserName);
                    snFriendList.Title = String.Format(PageResources.Module_UserProfileFriends, nameToShow);
                    discussionTopicMessages.Title = PageResources.UserProfile_DiscussionMessages_Title;

                    if ((SecurityUtility.GetUserId() == CurrentUser.Id) && Page.User.Identity.IsAuthenticated)
                        ctlProfile.ShowWorkingModeSwitch = true;

                    ctlProfile.UserId = CurrentUser.Id;
                    ctlInvitationsSent.UserId = CurrentUser.Id;
                    ctlInvitationsReceived.UserId = CurrentUser.Id;
                    snFriendList.UserId = CurrentUser.Id;
                    snFriendList.Visible = true;
                    discussionTopicMessages.UserId = CurrentUser.Id;
                    discussionTopicMessages.Visible = true;
                    discussionTopicMessages.BindData();
                    snWallNotes.UserId = CurrentUser.Id;
                    snWallNotes.Visible = true;
                    snPeopleSearch.UserId = CurrentUser.Id;
                }
            }
            else
            {
                this.SetPageTitle(MonoSoftware.MonoX.Resources.PageResources.UserProfile_NoSuchUser);
            } 
            #endregion
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Transfer initial Profile working mode state to sub controls
                mainContentTemplate.IsPreviewMode = ctlProfile.WorkingMode.Equals(ProfileWorkingMode.Preview);
                sideContentTemplate.IsPreviewMode = ctlProfile.WorkingMode.Equals(ProfileWorkingMode.Preview);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {            
            #region Demo code used for Visibility, Titles etc.
            snPeopleSearch.SearchBoxVisible = false;
            if (CurrentUser != null)
            {
                ctlInvitationsSent.Visible = CurrentUser.Id.Equals(SecurityUtility.GetUserId());
                ctlInvitationsReceived.Visible = CurrentUser.Id.Equals(SecurityUtility.GetUserId());
            }
            #endregion            
        } 
        #endregion

        #region Methods
        void ctlProfile_MyStatusChanged(object sender, EventArgs e)
        {
            snWallNotes.BindData();
        }

        private bool IsPeopleSearchVisible()
        {
            return (Page.User.Identity.IsAuthenticated && CurrentUser != null && SecurityUtility.GetUserId() != CurrentUser.Id && !FriendRepository.GetInstance().RelationshipExists(CurrentUser.Id));
        }
        #endregion
    }
}
