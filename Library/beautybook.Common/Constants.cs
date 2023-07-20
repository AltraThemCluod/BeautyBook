//-----------------------------------------------------------------------
// <copyright file="TitleConstants.cs" company="Premiere Digital Services">
//     Copyright Premiere Digital Services. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace BeautyBook.Common
{
    /// <summary>
    /// class TitleConstants
    /// </summary>
    public static class Constants
    {
        #region Object Table Names & Messages

        public const string FRANCHISETABLENAME = "Franchises";
        public const string INVALIDFRANCHISECREATED = "Cannot create franchise - Elastic indexing failed.";
        public const string INVALIDFRANCHISEUPDATED = "Cannot update franchise - Elastic indexing failed.";
        public const string EXTERNALTITLEIDSTABLENAME = "ExternalTitleIds";
        public const string EPISODEVERSIONTABLENAME = "EpisodeVersions";
        public const string CREDITTABLENAME = "Credits";
        public const string AVAILDATATABLENAME = "AvailData";
        public const string PLATFORMCONTAINERIDSTABLENAME = "PlatformContainerIds";
        public const string PLATFORMMETADATAFIELDS = "PlatformMetadataFields";

        public const string INVALIDPROVIDER = "Invalid Provider";
        public const string INVALIDFRANCHISEPROVIDERMESSAGE = "Cannot update franchise - Invalid Provider";
        public const string INVALIDCOUNTRYOFORIGIN = "Invalid Country Of Origin";
        public const string INVALIDORIGINALSPOKENLANGUAGE = "Invalid Original Spoken Language";
        public const string INVALIDLANGUAGECHAPTER = "Cannot create/update Chapter - Invalid Language";
        public const string INVALIDTERRITORYCHAPTER = "Cannot create/update Chapter - Invalid Territory";
        public const string INVALIDSTATUSMESSAGE = "Cannot Update Order: Trying to set an invalid Status";
        public const string INVALIDTASKSTATUSMESSAGE = "Cannot Update task: Trying to set an invalid status";
        public const string INVALIDORDERITEMREQUIREMENTSSTATUSMESSAGE = "Cannot Update Order Item Requirement: Trying to set an invalid Status";
        public const string INVALIDORDERITEMREQUIREMENTRE = "Cannot Update Order Item Requirement: Trying to set an invalid RE Conformance Asset Type";
        public const string INVALIDORDERITEMREQUIREMENTASPECTRATIO = "Cannot Update Order Item Requirement: Trying to set an invalid aspect ratio";
        public const string INVALIDORDERITEMREQUIREMENTRESOLUTION = "Cannot Update Order Item Requirement: Trying to set an invalid resolution";
        public const string INVALIDAVAILPRICETIERPLATFORM = "Cannot Create Avail Price Tier- Invalid Platform";
        public const string INVALIDAVAILDATAPLATFORM = "Cannot Create Avail data- Invalid Platform";
        public const string INVALIDAVAILDATATERRITORY = "Cannot Create Avail data- Invalid Territory";
        public const string INVALIDPATCHAVAILDATAPLATFORM = "Cannot update Avail data - Invalid Platform";
        public const string INVALIDPATCHAVAILDATATERRITORY = "Cannot update Avail data - Invalid Territory";
        public const string INVALIDPLATFORMTITLEIDMESSAGE = "Cannot Create platform title id - Invalid Platform.";
        public const string INVALIDTERRITORYPLATFORMTITLEIDMESSAGE = "Cannot Create platform title id - Invalid Territory.";

        public const string INVALIDFRANCHISEPARAMETERS = "Invalid Request Parameters - cannot update Id, Provider, pdsFranchiseId, CreatedAt, UpdatedAt and DeletedAt.";
        public const string INVALIDSERIESPARAMETERS = "Invalid Request Parameters - cannot update Id, PdsTitleId, Provider, FranchiseId, CreatedAt, UpdatedAt and DeletedAt.";
        public const string INVALIDPLATFORMTITLEIDSPLATFORM = "Cannot update platform title ID - Invalid Platform";
        public const string INVALIDPLATFORMTITLEIDSTERRITORY = "Cannot update platform title ID - Invalid territory";
        public const string INVALIDEPISODEVERSIONPARAMETER = "Invalid Request Parameters - cannot update pdsTitleId, Provider, CreatedAt, UpdatedAt, DeletedAt and episodeId.";
        public const string INVALIDPLATFORMCONTAINERIDSPARAMETER = "Invalid Request Parameters - cannot update SeasonId, CreatedAt, UpdatedAt and DeletedAt.";

        public const string INVALIDSEASONPARAMETER = "Invalid Request Parameters - cannot update pdsTitleId, SeriesId, CreatedAt, UpdatedAt, And DeletedAt.";
        public const string INVALIDAVAILTYPE = "Cannot update Avail Data - Invalid Avail type";
        public const string INVALIDRELEASETYPE = "Cannot update Avail Data - Invalid Release type";
        public const string INVALIDVODTYPE = "Cannot update Avail Data - Invalid VOD type";
        public const string INVALIDSEASONEISODESPARAMETER = "Invalid Request Parameters - cannot update SeasonId, EpisodeId.";
        public const string INVALIDPATCHAVAILPRICETIERPLATFORM = "Cannot update Avail Price Tier - Invalid Platform";

        public const string INVALIDTERRITORYPLATFORMCONTAINERID = "Cannot create platform title id - Invalid Territory";
        public const string INVALIDPLATFORMPLATFORMCONTAINERID = "Cannot create platform title id - Invalid Platform";

        public const string TVSERIES = "tv series";
        public const string TVSEASON = "tv season";
        public const string TVEPISODE = "tv episode";
        #endregion
    }
}
