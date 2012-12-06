<%@ Page Language="C#" MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master"
    AutoEventWireup="true" Inherits="ProjectName.Web.MonoXProfileSample" Title="User profile sample"
    Theme="Default" CodeBehind="MonoXProfileSample.aspx.cs" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="PeopleSearch" Src="~/MonoX/ModuleGallery/SocialNetworking/PeopleSearch.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FriendList" Src="~/MonoX/ModuleGallery/SocialNetworking/FriendList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="EditProfile" Src="~/MonoX/ModuleGallery/ProfileModule/UserProfileModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Wall" Src="~/MonoX/ModuleGallery/SocialNetworking/WallNotes.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register TagPrefix="MonoX" TagName="DiscussionMessages" Src="~/MonoX/ModuleGallery/SocialNetworking/Discussion/DiscussionMessages.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="InvitationList" Src="~/MonoX/ModuleGallery/SocialNetworking/InvitationList.ascx" %>

<%@ Register TagPrefix="ProjectName" TagName="MainContentTemplate" Src="~/ProjectName/ProfileSamples/MonoXProfileTemplates/MainContentTemplate.ascx" %>
<%@ Register TagPrefix="ProjectName" TagName="SideContentTemplate" Src="~/ProjectName/ProfileSamples/MonoXProfileTemplates/SideContentTemplate.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="Server">
    <table border="0" cellspacing="0" cellpadding="0" class="user-profile">
        <tr>
            <td class="left-section">
                <portal:PortalWebPartZone HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>'
                    ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
                    <ZoneTemplate>
                        <MonoX:EditProfile ID="ctlProfile" runat="server" AutoDetectUser="false"
                            HiddenFieldsString="FirstName,LastName">
                            <MainContentTemplate>
                                <br /><br /><br />
                                <h3>Main content</h3>
                                <ProjectName:MainContentTemplate id="mainContentTemplate" runat="server"></ProjectName:MainContentTemplate>
                            </MainContentTemplate>
                            <SideBarTemplate>
                                <br /><br /><br />
                                <h3>Side content</h3>
                                <ProjectName:SideContentTemplate id="sideContentTemplate" runat="server"></ProjectName:SideContentTemplate>
                            </SideBarTemplate>
                            <FooterTemplate>                                
                                <h3>Footer content</h3>                                
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed congue dolor at turpis ultrices cursus placerat dolor porttitor. Nullam ornare laoreet eros quis porttitor. Proin vitae odio a nisl tristique varius et ut sem. Integer faucibus mi ac quam eleifend hendrerit. Nulla eget lorem vitae lectus congue venenatis. Cras sit amet dolor sit amet arcu molestie vehicula. Fusce tortor tellus, vehicula in accumsan eu, pretium sed libero. Nulla auctor pulvinar est, vel accumsan urna consectetur quis. Proin nec risus ut orci adipiscing sodales. Integer facilisis, metus id tincidunt cursus, odio tortor hendrerit tortor, sed semper justo sapien id odio. Maecenas iaculis dignissim ipsum, non auctor ligula ultrices eget. Donec elementum dapibus ipsum, non lacinia nisl lacinia consectetur.
                                </p>
                            </FooterTemplate>
                        </MonoX:EditProfile>
                        <MonoX:Wall runat="server" ID="snWallNotes" UsePrettyPhoto="true" />
                    </ZoneTemplate>
                </portal:PortalWebPartZone>
            </td>
            <td class="right-section">
                <portal:PortalWebPartZone HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>'
                    ID="rightWebPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm"
                    ShowChromeForNonAdmins="true">
                    <ZoneTemplate>
                        <MonoX:PeopleSearch runat="server" ID="snPeopleSearch" />
                        <MonoX:InvitationList runat="server" InvitationType="InvitationsSent" ID="ctlInvitationsSent" PageSize="9" AvatarSize="40" HideIfEmpty="true" />
                        <MonoX:InvitationList runat="server" InvitationType="InvitationsReceived" ID="ctlInvitationsReceived" PageSize="9" AvatarSize="40" HideIfEmpty="true" />
                        <MonoX:FriendList runat="server" PageSize="9" AvatarSize="40" ID="snFriendList">
                        </MonoX:FriendList>
                        <MonoX:DiscussionMessages ID="discussionTopicMessages" runat="server" HideIfEmpty="true" ShowActionButtons="false" ShowMessagePost="false" CurrentMode="None" IsPublic="true" 	EnablePrettyPhoto="true" MaxPostLength="40" Template="UserProfileDiscussionMessage" EnableSyntaxHighlighter="false" ShowHeader="false" ShowPager="false" PageSize="10"></MonoX:DiscussionMessages>
                    </ZoneTemplate>
                </portal:PortalWebPartZone>
            </td>
        </tr>
    </table>
</asp:Content>
