<%@ Control Language="C#" AutoEventWireup="true" Inherits="ProjectName.Web.MainContentTemplate" Codebehind="MainContentTemplate.ascx.cs" %>
<%@ Register TagPrefix="MonoX" TagName="StyledButton" Src="~/MonoX/controls/StyledButton.ascx" %>

<div class="profile-status-top" style="background-image: none;">
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
<div class="input-form">
    <dl class="profile-details" style="margin-top: 20px;">
        <dd>
            <asp:Label ID="labGPSLocation" AssociatedControlID="GPSLocation" runat="server" Text="GPS Location" CssClass="label-bold"></asp:Label>
            <asp:Label ID="prevGPSLocation" runat="server"></asp:Label>
            <asp:TextBox runat="server" ID="GPSLocation" />
        </dd>
        <dd>
            <asp:Label ID="labHometown" AssociatedControlID="Hometown" runat="server" Text="Hometown" CssClass="label-bold"></asp:Label>
            <asp:Label ID="prevHometown" runat="server"></asp:Label>
            <asp:TextBox runat="server" ID="Hometown" />
        </dd>
        <dd>
            <asp:Label ID="labUpdateDate" AssociatedControlID="UpdateDate" runat="server" Text="Update date" CssClass="label-bold"></asp:Label>
            <asp:Label ID="prevUpdateDate" runat="server"></asp:Label>
            <asp:TextBox runat="server" ID="UpdateDate" />
        </dd>
    </dl>
</div>
<div class="profile-status-top" style="background-image: none; padding: 0px; overflow: hidden;">
        <MonoX:StyledButton ID="btnSave" runat="server" Text="Save" CausesValidation="true" />    
</div>