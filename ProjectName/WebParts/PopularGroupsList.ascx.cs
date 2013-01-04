using MonoSoftware.MonoX.Caching;
using MonoSoftware.MonoX.Samples.Repositories;
using MonoSoftware.MonoX.Resources;
using MonoSoftware.MonoX.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NVelocity;

namespace MonoSoftware.MonoX.ModuleGallery.SocialNetworking
{
    /// <summary>
    /// Displays most popular groups ordered by their creation date, often used on dashboard pages. Inherits from the GroupListBasedPart.
    /// </summary>
    public partial class PopularGroupsList : GroupListBasePart
    {
        #region Properties
        /// <summary>
        /// Templated list view that is used for rendering the "repeatable" section of this control.
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ListView TemplatedControl
        {
            get { return lvItems; }
            set
            {
                pnlContainer.Controls.Remove(lvItems);
                lvItems = value;
                pnlContainer.Controls.AddAt(0, lvItems);
            }
        }

        /// <summary>
        /// Pager page size.
        /// </summary>
        [WebBrowsable(true), Personalizable(true)]
        [WebDescription("Pager page size")]
        [WebDisplayName("Pager page size")]
        public int PageSize
        {
            get { return pager.PageSize; }
            set { pager.PageSize = value; }
        }

        private bool _pagingEnabled = true;
        /// <summary>
        /// Gets or sets a value indicating whether paging is enabled.
        /// </summary>
        [WebBrowsable(true), Personalizable(true)]
        [WebDescription("Paging enabled")]
        [WebDisplayName("Paging enabled")]
        public bool PagingEnabled
        {
            get { return _pagingEnabled; }
            set { _pagingEnabled = value; }
        }

        private int _avatarSize = 80;
        /// <summary>
        /// Avatar size - 1 to 80 for gravatars.
        /// </summary>
        [WebBrowsable(true), Personalizable(true)]
        [WebDescription("Avatar size")]
        [WebDisplayName("Avatar size")]
        public int AvatarSize
        {
            get { return _avatarSize; }
            set { _avatarSize = value; }
        }

        private string _pagerQueryString = "PopularGroupsPageNo";
        /// <summary>
        /// Pager query string name.
        /// </summary>
        [WebBrowsable(true), Personalizable(true)]
        [WebDescription("Pager query string name")]
        [WebDisplayName("Pager query string name")]
        public string PagerQueryString
        {
            get { return _pagerQueryString; }
            set { _pagerQueryString = value; }
        } 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public PopularGroupsList()
        {
            //Set the title that will be visible in the Module gallery so user can pick the module from the list
            Title = "SN popular groups list";
            //Define the template root folder for where MonoX can search for specified template
            ControlSpecificTemplateSubPath = "SocialNetworkingTemplates";
            //Define the template file name (without the extension)
            Template = "PopularGroupsList";
            IsTemplated = true;
        } 
        #endregion

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            //Set the SEO pager query string so pager can read the proper page number form the Url
            PagerUtility.SetCustomQueryStringName(this, pager, this.PagerQueryString);
            this.TemplatedControl.ItemDataBound += new EventHandler<ListViewItemEventArgs>(lvItems_OnItemDataBound);
            ltlNoData.Text = SocialNetworkingResources.Groups_NoDataWarning;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Optimize the binding process
            if (this.Visible)
                BindData();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //Hide the pager if there are no pages or paging is disabled
            pager.Visible = (pager.PageCount > 1) && this.PagingEnabled;
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Binds and formats group data for display in the main list view control.
        /// </summary>
        [WebPartApplyChanges]
        public override void BindData()
        {
            //Optimize the DB access by using the MonoX cache mechanism (Note: To gain from this kind of optimization you need to set the CacheDuration for this web part)
            MonoXCacheManager cacheManager = MonoXCacheManager.GetInstance(CacheKeys.Groups.PopularGroupsList, this.CacheDuration);
            PopularGroupRepository repository = PopularGroupRepository.GetInstance();
            int recordCount = cacheManager.Get<int>(PopularGroupRepository.CacheParamMonoXPopularGroupsList, "recordCount");
            List<MonoSoftware.MonoX.Repositories.SnGroupDTO> groups = cacheManager.Get<List<MonoSoftware.MonoX.Repositories.SnGroupDTO>>(PopularGroupRepository.CacheParamMonoXPopularGroupsList, pager.CurrentPageIndex + 1, pager.PageSize);
            if (groups == null)
            {
                //Fetch the popular groups
                groups = repository.GetPopularGroups(String.Empty, Guid.Empty, pager.CurrentPageIndex + 1, pager.PageSize, out recordCount);
                //Store the values to the cache (Note: If CacheDuration is set to "0" values are not saved to the cache)
                cacheManager.Store(groups, PopularGroupRepository.CacheParamMonoXPopularGroupsList, pager.CurrentPageIndex + 1, pager.PageSize);
                cacheManager.Store(recordCount, PopularGroupRepository.CacheParamMonoXPopularGroupsList, "recordCount");
            }
            //Perform the binding process that will automatically bind the control and pager
            PagerUtility.BindPager(pager, BindData, lvItems, groups, recordCount);
            ltlNoData.Visible = (recordCount == 0);
        }

        protected void lvItems_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                MonoSoftware.MonoX.Repositories.SnGroupDTO group = ((ListViewDataItem)e.Item).DataItem as MonoSoftware.MonoX.Repositories.SnGroupDTO;
                //Parse template tags will extract all the needed information from SnGroupDTO and fill the tags collection with tags that are built-in 
                //Note: To add any custom tags to web part template you need to override the ParseTemplateTags and add custom tags, then you need to place them in web part template file
                VelocityContext velocityContext = new VelocityContext();
                Hashtable tags = ParseTemplateTags(group, velocityContext);
                //Render templated item to the place holder
                //Note: This built-in method will parse the provided html template and replace the tags with the values stored in the tags collection
                RenderTemplatedPart(e.Item, CurrentTemplateHtml, tags);
            }
        } 
        #endregion

   }
}