<%@ Page Language="C#" MasterPageFile="~/MonoX/MasterPages/Default.master" AutoEventWireup="true" Inherits="ProjectName.Web.WebPartSample" Title="Web part sample" Theme="Default" Codebehind="WebPartSample.aspx.cs" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %> 
<%@ Register TagPrefix="sample" TagName="HelloWorld" Src="~/ProjectName/WebParts/HelloWorld.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">
    <table width="998" border="0" align="center" cellspacing="0" cellpadding="0">
      <tr>
        <td width="239" valign="top" class="toppadding10px">
            <portal:PortalWebPartZone HeaderText="Left part zone" ID="leftWebPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm" ShowChromeForNonAdmins="true">
                <ZoneTemplate>
                    <sample:HelloWorld runat="server" ID="helloWorldSample" Title="Hello world" />
                </ZoneTemplate>
            </portal:PortalWebPartZone>
        </td>
        <td width="757" valign="top" class="padding_left_40px" style="text-align:justify;">
            <portal:PortalWebPartZone HeaderText="Right part zone" ID="rightPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="false">
                <ZoneTemplate>
                    <asp:Panel runat="server" ID="pnlDescription">
                        <p><span class="header_blue">Web part sample</span></p>
                        <p>
                        This is a sample page demonstrating the development and the usage of a very simple Web part. The "Hello word" part on this page contains two custom properties and displays them to the user on a button click. 
                        <br /><br />
                        Feel free to modify this page during the learning process.
                        </p>
                    </asp:Panel>          
                </ZoneTemplate>
            </portal:PortalWebPartZone>
        </td>
      </tr>
    </table>
</asp:Content>

