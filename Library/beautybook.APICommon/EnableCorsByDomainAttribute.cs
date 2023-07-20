//-----------------------------------------------------------------------
// <copyright file="EnableCorsByDomainAttribute.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.APICommon
{  
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Cors;
    using System.Web.Http.Cors;
    using BeautyBook.Common;

    /// <summary>
    /// Custom attribute to enable CORS from a list of domains, specified in appSettings-key.
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    /// <seealso cref="System.Web.Http.Cors.ICorsPolicyProvider"/>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class EnableCorsByDomainAttribute : Attribute, ICorsPolicyProvider
    {
        #region Private Fields

        /// <summary>
        /// The policy
        /// </summary>
        private static CorsPolicy policy;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes static members of the <see cref="EnableCorsByDomainAttribute"/> class.
        /// </summary>
        static EnableCorsByDomainAttribute()
        {
            policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true,
                SupportsCredentials = true,
                PreflightMaxAge = 24 * 60 * 60    //// 24 hrs
                //// Max age may vary per browsers. Ref.: http://stackoverflow.com/a/23549398
            };

            ////var originsString = Configurations.CorsDomains;
            ////if (!string.IsNullOrWhiteSpace(originsString))
            ////{
            ////    policy.Origins.Clear();

            ////    foreach (var origin in originsString.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
            ////    {
            ////        policy.Origins.Add(origin);
            ////    }
            ////}
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Gets the <see cref="T:System.Web.Cors.CorsPolicy"/>.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="T:System.Web.Cors.CorsPolicy"/>.</returns>
        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(policy);
        }

        #endregion Public Methods
    }
}