<%@ Control Language="C#" AutoEventWireup="true" Inherits="ProjectName.Web.SideContentTemplate" Codebehind="SideContentTemplate.ascx.cs" %>
<%@ Register TagPrefix="MonoX" TagName="StyledButton" Src="~/MonoX/controls/StyledButton.ascx" %>

<div class="profile-status-top" style="background-image: none; padding: 0px;">
    <div class="buttons">
        <ul id="Ul1" class="button" runat="server">
            <li class="<%= !IsPreviewMode ? String.Empty : "current" %>">
                <asp:LinkButton ID="lnkViewProfile2" runat="server" CausesValidation="false">
                    <span id="labViewProfile2" runat="server"></span>
                </asp:LinkButton></li>
            <li class="<%= !IsPreviewMode ? "current" : String.Empty %>">
                <asp:LinkButton ID="lnkEditProfile2" runat="server" CausesValidation="false">
                    <span id="labEditProfile2" runat="server"></span>
                </asp:LinkButton></li>
        </ul>
    </div>
</div>
<br />
<div class="input-form">
    <dl class="profile-details" style="margin-top: 20px;">        
        <dd>
            <asp:Label ID="labIsModerator" AssociatedControlID="IsModerator" runat="server" Text="Is&nbsp;Moderator"
                CssClass="label-bold" style="width: auto;"></asp:Label>
            <asp:Label ID="prevIsModerator" runat="server"></asp:Label>
            <asp:CheckBox runat="server" ID="IsModerator" />
        </dd>
    </dl>
</div>
<div class="profile-status-top" style="background-image: none; padding: 0px; overflow: hidden;">
        <MonoX:StyledButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" />    
</div>