<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    MasterPageFile="~/MonoX/MasterPages/Default.master"
    Theme="Default" 
    Inherits="MonoSoftware.MonoX.BasePage" 
    Title="MonoX Samples - Portal Framework for ASP.NET" 
    %>
    
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
 
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Rss" Src="~/MonoX/ModuleGallery/RssReader.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.ModuleGallery" TagPrefix="ModuleGallery" %>
<%@ Register TagPrefix="MonoX" TagName="AdModule" Src="~/MonoX/ModuleGallery/AdModule.ascx"  %>
<%@ Register TagPrefix="MonoX" TagName="SlideShow" Src="~/MonoX/ModuleGallery/SlideShow.ascx"  %>

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">

<div class="main"> 
    <table cellpadding="0" cellspacing="0" class="two-columns">
	    <tr>
    	    <td>    	        
	                <h2>MonoX General Samples</h2>
	                <img runat="server" src="~/App_Themes/Default/img/content-management-icon.png" alt="Content management" />
	                <div>
	                MonoX is powerful and flexible Content Management System which allows you to design and develop next generation ASP.NET Web 
	                portals and applications. Below you can find an examples that show how flexible MonoX CMS can be.                     
                    <ul>
                        <li><a runat="server" href="~/ProjectName/ProfileSamples/ASPNETProfileSample/ASPNETProfileSample.aspx">&gt;&gt; ASP.NET Profile Sample</a> </li>
                        <li><a runat="server" href="~/ProjectName/ProfileSamples/MonoXProfileTemplates/MonoXProfileSample.aspx">&gt;&gt; MonoX Profile Templates Sample</a> </li>
	                </ul>
                    </div>
            </td>            
	    </tr>
    </table>
</div> 
<div class="light-blue-wrapper">
	<div class="light-blue-bg">
    	<table cellpadding="0" cellspacing="0" class="featured-project">
        	<tr>            	
                <td class="project-container">
                    <MonoX:SlideShow runat="server" ID="ctlSlideShow">
                        <SlideShowItems>
                            <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/Default/img/Projects/project-jobs-market.jpg" Url="http://www.jobsmarket.ie" Title="Jobs Market"></ModuleGallery:SlideShowItem>
                            <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/Default/img/Projects/project-promo-zebra.jpg" Url="http://www.promozebra.com" Title="Promo Zebra"></ModuleGallery:SlideShowItem>                            
                            <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/Default/img/Projects/project-cpl.jpg" Url="http://www.cpl.ie" Title="CPL"></ModuleGallery:SlideShowItem>
                            <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/Default/img/Projects/project-drzavni-inspektorat.jpg" Url="http://www.inspektorat.hr/" Title="Croatian State Inspectorate"></ModuleGallery:SlideShowItem>
                            <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/Default/img/Projects/project-employ-ireland.jpg" Url="http://www.employireland.ie/" Title="Employ Ireland"></ModuleGallery:SlideShowItem>
                            <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/Default/img/Projects/project-anesthesia-scheduler.jpg" Url="http://v2.anesthesiascheduler.com/" Title="Anesthesia Scheduler"></ModuleGallery:SlideShowItem>
                            <ModuleGallery:SlideShowItem runat="server" ImageUrl="~/App_Themes/Default/img/Projects/project-ectd-office.jpg" Url="http://www.ectdoffice.com/" Title="eCTD Office"></ModuleGallery:SlideShowItem>
                        </SlideShowItems>
                    </MonoX:SlideShow>
				</td>                
                <td class="project-description">
                    <portal:PortalWebPartZone ID="featuredProjectPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText='<%$ Code: PageResources.Zone_FeaturedProjectsZone %>'>
                    <ZoneTemplate>
                        <MonoX:Editor runat="server" ID="editor3" Title='<%$ Code: PageResources.Title_FeaturedProjects %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_FeaturedProjects %>'>
                        <DefaultContent>
                	        <h2>Featured Projects</h2>
                            <div>MonoX is used in a variety of projects of all sizes, from small-scale content management systems to large scale community sites.</div><br />
					        <div>Visit some of the more recent projects built on top of MonoX framework and get a glimpse of its capabilities.
                            <ul>
                                <li><a href="~/MonoX/Pages/Contact.aspx">>> Contact us for more details...</a></li>
                            </ul>
                            </div>
                        </DefaultContent>
                        </MonoX:Editor>
                    </ZoneTemplate>
                    </portal:PortalWebPartZone>
                </td>
			</tr>
		</table>                                    
    </div>
</div>        
</asp:Content>