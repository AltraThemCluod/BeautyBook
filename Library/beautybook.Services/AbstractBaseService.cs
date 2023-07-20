//-----------------------------------------------------------------------
// <copyright file="AbstractBaseService.cs" company="Premiere Digital Services">
//     Copyright Premiere Digital Services. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Services
{    
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Common;
    using Common.Paging;
    using Amazon;
    using Amazon.S3;
    using Amazon.S3.Transfer;
    using Amazon.S3.Model;
    using System.IO;
    using BeautyBook.Data;

    /// <summary>
    /// Class Base Business Access Layer
    /// </summary>
    public abstract class AbstractBaseService
    {
        /// <summary>
        /// Executes the dynamic query.
        /// </summary>
        /// <typeparam name="A">The abstract class.</typeparam>
        /// <typeparam name="I">The base model class.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>
        /// returns the paged list result.
        /// </returns>
        public virtual Task<SuccessResult<PagedList<A>>> ExecuteDynamicQuery<A, I>(string query, PageParam param)
            where A : BaseModel
            where I : BaseModel
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes the dynamic query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The parameter.</param>
        /// <returns>
        /// returns the paged list result.
        /// </returns>
        public virtual Task<SuccessResult<PagedList<dynamic>>> ExecuteDynamicQuery(string query, PageParam param)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes the dynamic query Elastic.
        /// </summary>
        /// <typeparam name="A">The abstract class.</typeparam>
        /// <typeparam name="I">The base model class.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// returns the paged list result.
        /// </returns>
        public virtual Task<SuccessResult<PagedList<A>>> ExecuteDynamicQueryElastic<A, I>(string type)
            where A : BaseModel
            where I : BaseModel
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes the dynamic query Elastic.
        /// </summary>
        /// <typeparam name="A">The abstract class.</typeparam>
        /// <typeparam name="I">The base model class.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="sortDescriptors">The sort descriptors.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// returns the paged list result.
        /// </returns>
        public virtual Task<SuccessResult<PagedList<A>>> ExecuteDynamicQuery<A, I>(PageParam param, string type)
            where A : BaseModel
            where I : BaseModel
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes the dynamic query Elastic.
        /// </summary>
        /// <typeparam name="I">The base model class.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="aggregation">The aggregation.</param>
        /// <param name="sortDescriptors">The sort descriptors.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// returns the paged list result.
        /// </returns>
        public virtual Task<SuccessResult<PagedList<dynamic>>> ExecuteDynamicQuery<I>(Dictionary<string, string> sortDescriptors, PageParam param, string type)
            where I : BaseModel
        {
            throw new NotImplementedException();
        }

        public virtual void S3FileUpload(string Filepath, string awsKey, byte[] imageBytes = null, bool isByteArray = false)
        {
            try
            {
                using (IAmazonS3 client = new AmazonS3Client(Configurations.S3AccessKeyID, Configurations.S3SecretKey, RegionEndpoint.APSouth1))
                {

                    if (!isByteArray)
                    {
                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            Key = awsKey,
                            FilePath = Filepath,
                            BucketName = Configurations.BucketName
                        };

                        var fileTransferUtility = new TransferUtility(client);
                        fileTransferUtility.Upload(uploadRequest);
                    }
                    else
                    {
                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            Key = awsKey,
                            InputStream = new MemoryStream(imageBytes),
                            BucketName = Configurations.BucketName
                        };

                        var fileTransferUtility = new TransferUtility(client);
                        fileTransferUtility.Upload(uploadRequest);
                    }

                  
                }
            }
            catch (AmazonS3Exception ex)
            {

            }
        }

        public virtual string GeneratePreSignedURL(string awsKey)
        {
            string urlString = "";
            try
            {
                using (IAmazonS3 client = new AmazonS3Client(Configurations.S3AccessKeyID, Configurations.S3SecretKey, RegionEndpoint.APSouth1))
                {
                    GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                    {
                        BucketName = Configurations.BucketName,
                        Key = awsKey,
                        Expires = DateTime.Now.AddSeconds(2000)
                    };

                    urlString = client.GetPreSignedURL(request1);
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            return urlString;
        }

        public virtual bool DeleteS3File(string awsKey)
        {
            try
            {
                using (IAmazonS3 client = new AmazonS3Client(Configurations.S3AccessKeyID, Configurations.S3SecretKey, RegionEndpoint.APSouth1))
                {
                    Amazon.S3.Model.DeleteObjectRequest deleteObjectRequest = new Amazon.S3.Model.DeleteObjectRequest
                    {
                        BucketName = Configurations.BucketName,
                        Key = awsKey
                    };                    
                    client.DeleteObject(deleteObjectRequest);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
