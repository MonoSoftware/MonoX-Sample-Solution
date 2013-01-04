using MonoSoftware.Core;
using MonoSoftware.MonoX.DAL;
using MonoSoftware.MonoX.DAL.EntityClasses;
using MonoSoftware.MonoX.DAL.HelperClasses;
using MonoSoftware.MonoX.Utilities;
using SD.LLBLGen.Pro.ORMSupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using MonoSoftware.MonoX.DAL.RelationClasses;

namespace MonoSoftware.MonoX.Repositories
{
    /// <summary>
    /// Repository for social network popular group-related functionality
    /// </summary>
    public class PopularGroupRepository : GroupRepository 
    {

        #region Fields
        /// <summary>
        /// MonoX popular groups list cache parameter.
        /// </summary>
        public const string CacheParamMonoXPopularGroupsList = "MonoXPopularGroupsList";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        protected PopularGroupRepository()
        { }

        /// <summary>
        /// Returns an instance of the repository class, wraps the constructor to allow for encapsulated instatiation, so the logic in he constructor can be changed without changing the client code.
        /// Reference: http://www.netobjectives.com/ezines/ez0405NetObj_PerspectivesOfUseVsCreationInOODesign.pdf
        /// </summary>
        /// <returns>Instance of the repository.</returns>
        public static new PopularGroupRepository GetInstance()
        {
            return new PopularGroupRepository();
        } 
        #endregion        

        #region Methods
        /// <summary>
        /// Retrieves a list of groups matching the criteria specified via method parameters.
        /// </summary>
        /// <param name="category">Category name.</param>
        /// <param name="categoryId">Category Id</param>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="recordCount">Record count.</param>
        /// <returns>List of groups.</returns>
        public List<SnGroupDTO> GetPopularGroups(string category, Guid categoryId, int pageNumber, int pageSize, out int recordCount)
        {
            RelationPredicateBucket filter = new RelationPredicateBucket();
            
            //introduced to filter out groups by languages and applications            
            filter.Relations.Add(SnGroupEntity.Relations.SnGroupCategoryEntityUsingGroupCategoryId, JoinHint.Left);
            //Note: MonoX supports the multi application environment so general filter for all DB access calls should contain the application id filter
            filter.PredicateExpression.Add(SnGroupCategoryFields.ApplicationId == MembershipRepository.GetInstance().GetApplicationId());
            //Note: MonoX in supports the multi language environment so general filter for all DB access calls should contain the language id filter
            filter.PredicateExpression.Add(SnGroupCategoryFields.LanguageId == LocalizationUtility.GetCurrentLanguageId());

            //Filter groups by category
            if (categoryId != Guid.Empty)
            {
                filter.PredicateExpression.Add(SnGroupFields.GroupCategoryId == categoryId);
            }
            if (!String.IsNullOrEmpty(category))
            {
                filter.PredicateExpression.Add(SnGroupCategoryFields.Slug == category);
            }

            IPrefetchPath2 prefetch = new PrefetchPath2((int)EntityType.SnGroupEntity);
            prefetch.Add(SnGroupEntity.PrefetchPathSnGroupCategory);
            //Fetch a record from the members table only for the current user so his status could be read
            Guid uid = SecurityUtility.GetUserId();
            if (!Guid.Empty.Equals(uid))
            {
                PredicateExpression memberFilter = new PredicateExpression(SnGroupMemberFields.UserId == uid);
                prefetch.Add(SnGroupEntity.PrefetchPathSnGroupMembers, 1, memberFilter);
            }
            
            #region Popular groups sorter
            const string memberCountField = "MemberCountField";
            const string memberCountTableName = "MemberCountTable";

            EntityFields2 memberFields = new EntityFields2(2);
            memberFields.DefineField(SnGroupMemberFields.GroupId, 0);
            memberFields.DefineField(SnGroupMemberFields.Id, 1, memberCountField, AggregateFunction.Count);
            DerivedTableDefinition memberCountTable = new DerivedTableDefinition(memberFields, memberCountTableName, null, new GroupByCollection(memberFields[0]));

            IDynamicRelation memberCountRelation = new DynamicRelation(memberCountTable, JoinHint.Right, MonoSoftware.MonoX.DAL.EntityType.SnGroupEntity, String.Empty, SnGroupMemberFields.GroupId.SetObjectAlias(memberCountTable.Alias) == SnGroupFields.Id);
            filter.Relations.Add(memberCountRelation);

            ISortExpression sorter = new SortExpression(new SortClause(new EntityField2(memberCountField, null).SetObjectAlias(memberCountTableName), null, SortOperator.Descending)); 
            #endregion

            EntityCollection<SnGroupEntity> groups = new EntityCollection<SnGroupEntity>();
            //Fetch the group collection
            FetchEntityCollection(groups, filter, 0, sorter, prefetch, pageNumber, pageSize);
            //Fetch the group total count used by paging
            recordCount = GetDbCount(groups, filter);
            //Transfer group entities to the DTO
            List<SnGroupDTO> toReturn = groups.Select(group => new SnGroupDTO(group)).ToList<SnGroupDTO>();
            return toReturn;
        }
        
        #endregion
        



    }

}
