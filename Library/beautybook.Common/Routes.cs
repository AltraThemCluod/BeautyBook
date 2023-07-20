//-----------------------------------------------------------------------
// <copyright file="Routes.cs" company="">
//     Copyright . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Sealed Class Routes
    /// </summary>
    public sealed class Routes
    {
        public const string GETUSERS = "GetUsers";
        public const string GETFRANCHISE = "GetFranchise";
        public const string GETFEATURE = "GetFeature";
        public const string GETFEATUREVERSIONS = "GetFeatureVersions";
        public const string GETFEATUREMERGEDCREDITSMETADATA = "GetFeatureMergedCreditsMetadata";
        public const string GETFEATUREMERGEDMETADATA = "GetFeatureMergedMetadata";
        public const string GETFEATUREEXTERNALIDS = "GetFeatureExternalIds";

        public const string GETFEATUREVERSION = "GetFeatureVersion";
        public const string GETFEATUREVERSIONMERGEDMETADATA = "GetFeatureVersionMergedMetadata";
        public const string GETFEATUREVERSIONEXTERNALIDS = "GetFeatureVersionExternalIds";
        public const string GETFEATUREVERSIONMERGEDCREDITSMETADATA = "GetFeatureVersionMergedCreditsMetadata";
        public const string GETFEATUREVERSIONPLATFORMTITLEIDS = "GetFeatureVersionsPlatformTitleIds";
        public const string GETFEATUREVERSIONAVAILDATA = "GetFeatureVersionAvailData";

        public const string ELASTICSEARCH = "ElasticSearch";

        public const string POSTTITLE = "TitleImport";

        public const string GETAVAILDATA = "GetAvailData";
        public const string GETAVAILPRICETIERDATA = "GetAvailPriceTiers";
        public const string GETAVAILDATAAVAIPRICETIER = "GetAvailDataAvailPriceTier";

        public const string GETREACHASSETLIST = "GetReachAssetList";
        
        public const string GETSEASON = "GetSeason";
        public const string GETSERIES = "GetSeries";
        public const string GETEPISODE = "GetEpisode";
        public const string GETEPISODEVERSION = "GetEpisodeVersion";
        public const string CREATEEPISODEVERSION = "CreateEpisodeVersion";
        public const string FEATURES = "Features";
        public const string EPISODES = "Episodes";
        public const string SERIES = "SeriesList";
        public const string SEASONS = "SeasonsList";
        public const string SEARCHFEATUREVERSIONS = "SearchFeatureVersions";        
        public const string AssetIngestWIPList = "AssetIngestWIPList";
        public const string Franchise = "FranchiseList";
        public const string PlatformTitleId = "PlatformTitleIdsList";
        public const string ExternalTitleIds = "ExternalTitleIds";
        public const string GetChapterNamesList = "GetChapterNamesList";

        public const string EPISODEVERSIONS = "EpisodeVersions";
        public const string GETSEASONEPISODELIST = "GetSeasonEpisodesList";

        public const string GetPlatformTitleId = "GetPlatformTitleId";
        public const string PLATFORMCONTAINERIDS = "PlatformContainerIds";

        public const string CREDITS = "Credits";
        public const string PLATFORMMETADATAFIELDS = "PlatformMetadataFields";

        public const string GETTITLEMETADATALIST = "GetTitleMetadataList";
        public const string GETPLATFORMCONTAINERIDS = "GetPlatformContainerIds";
        public const string GETAVAILPRICETIERLIST = "GetAvailPriceTiersList";
        public const string SEARCHAVAILDATA = "SearchAvailData";

        public const string GETEPISODEVERSIONXML = "GetEpisodeVersionXml";
    }
}
