//-----------------------------------------------------------------------
// <copyright file="AbstractBaseController.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.APICommon
{    
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Routing;
    using BeautyBook.Common;
    using BeautyBook.Common.Paging;

    /// <summary>
    /// Class base API controller
    /// </summary>
    [RoutePrefix("api/[controller]")]
    [WebApiActionFilter]
    public abstract class AbstractBaseController : ApiController
    {
        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractBaseController"/> class.
        /// </summary>
        protected AbstractBaseController()
        {
            string name = this.GetType().Name;

            var regex = new Regex(@"^*([vV][\d]+)Controller$", RegexOptions.IgnoreCase);
            Match match = regex.Match(name);

            this.Version = match.Groups[1].Value;
        }

        #endregion Protected Constructors

        #region Protected Properties

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        protected string Version { get; private set; }

        #endregion Protected Properties

        #region Protected Methods

        /// <summary>
        /// Creates a <see cref="CustomNegotiatedContentResult{T}"/> with the specified values.
        /// </summary>
        /// <typeparam name="T">The type of content in the entity body.</typeparam>
        /// <param name="statusCode">The HTTP status code for the response message.</param>
        /// <param name="value">The content value to negotiate and format in the entity body.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>A <see cref="CustomNegotiatedContentResult{T}"/> with the specified values.</returns>
        protected internal CustomNegotiatedContentResult<T> Content<T>(HttpStatusCode statusCode, T value, string routeName, object routeValues)
        {
            string location = string.Empty;

            if (statusCode == HttpStatusCode.Created)
            {
                location = this.GetLink(routeName, routeValues);
            }

            this.SetJsonIgnoreCondition();
            return new CustomNegotiatedContentResult<T>(statusCode, value, location, this);
        }

        /// <summary>
        /// Contents the specified status code.
        /// </summary>
        /// <typeparam name="T">The type of content in the entity body.</typeparam>
        /// <param name="statusCode">The status code.</param>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="CustomNegotiatedContentResult{T}"/> with the specified values.</returns>
        protected internal new CustomNegotiatedContentResult<T> Content<T>(HttpStatusCode statusCode, T value)
        {
            ////this.SetJsonIgnoreCondition();
            return this.Content<T>(statusCode, value, string.Empty, null);
        }        

        /// <summary>
        /// Gets the error result.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <returns>The http result.</returns>
        protected IHttpActionResult GetErrorResult(HttpStatusCode code, string message)
        {
            ErrorResult result = new ErrorResult();
            result.Error.Message = message;
            return this.Content<ErrorResult>((HttpStatusCode)code, result);
        }

        /// <summary>
        /// Gets the error list result.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="messages">The messages.</param>
        /// <returns>The error list result</returns>
        protected IHttpActionResult GetErrorListResult(HttpStatusCode code, IEnumerable<string> messages)
        {
            return this.GetErrorResult(code, string.Join(";", messages));
        }

        /// <summary>
        /// Gets the group by result.
        /// </summary>
        /// <param name="aggregates">The aggregates.</param>
        /// <returns>
        /// GroupByResult Object
        /// </returns>
        protected GroupByResult<dynamic> GetGroupByResult(IEnumerable<dynamic> aggregates)
        {
            GroupByResult<dynamic> obj = new GroupByResult<dynamic>();
            obj.Aggregates = aggregates;

            return obj;
        }

        /// <summary>
        /// Gets the link.
        /// </summary>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The link</returns>
        protected string GetLink(string routeName, object routeValues)
        {
            if (!(string.IsNullOrWhiteSpace(routeName) || routeValues == null))
            {
                string url = this.Url.Link(this.GetVersionedRouteName(routeName), routeValues);
                if (!this.Request.IsLocal())
                {
                    if (this.Request.Headers.Count() > 0)
                    {
                        string protocol = Utility.GetHeader(this.Request, "X-Forwarded-Proto");
                        if (!string.IsNullOrWhiteSpace(protocol))
                        {
                            url = url.Replace("http:", protocol + ":");
                        }
                    }
                }
                else
                {
                    url = url.Replace("http:", "https:");
                }

                return WebUtility.UrlDecode(url);
            }
            else if (string.IsNullOrWhiteSpace(routeName) && routeValues != null)
            {
                return WebUtility.UrlDecode(routeValues.ToString());
            }

            return default(string);
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <typeparam name="T">The Base Model</typeparam>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="obj">The object.</param>
        /// <returns>The http result.</returns>
        protected IHttpActionResult GetResult<T>(string routeName, IDictionary<string, object> routeValues, PagedList<T> obj) where T : BaseModel
        {
            if (obj == null || obj.Values == null || obj.Values.Count == 0)
            {
                return this.NotFound();
            }

            return this.GetSearchResult<T>(routeName, routeValues, obj);
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <typeparam name="T">The Base Model</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>The http result.</returns>
        protected IHttpActionResult GetResult<T>(ICollection<T> obj) where T : BaseModel
        {
            if (obj == null || obj.Count == 0)
            {
                return this.NotFound();
            }

            this.SetHateoasLinks<T>(obj);

            return this.Ok<ICollection<T>>(obj);
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <typeparam name="T">The Base Model</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>The http result.</returns>
        protected IHttpActionResult GetResult<T>(T obj) where T : BaseModel
        {
            if (obj == null)
            {
                return this.NotFound();
            }

            this.SetHateoasLinks<T>(obj);

            return this.Ok<T>(obj);
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <typeparam name="T">The base model.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="errorBody">if set to <c>true</c> [error body].</param>
        /// <returns>
        /// The http result.
        /// </returns>
        protected IHttpActionResult GetResult<T>(SuccessResult<PagedList<T>> result, string routeName, object routeValues, bool errorBody = false) where T : BaseModel
        {
            if (result == null)
            {
                return this.StatusCode(HttpStatusCode.InternalServerError);
                ////return this.Content<T>(HttpStatusCode.InternalServerError, default(T));
            }
            else if (!result.IsValid || !result.IsSuccessStatusCode)
            {
                if (errorBody && !string.IsNullOrWhiteSpace(result.Message))
                {
                    return this.GetErrorResult((HttpStatusCode)result.Code, result.Message);
                }

                return this.StatusCode((HttpStatusCode)result.Code);
            }
            else
            {
                PagedResult<T> pagedResult = this.GetPagedResult<T>(routeName, routeValues, result.Item);

                return this.Content<PagedResult<T>>((HttpStatusCode)result.Code, pagedResult);
            }
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <typeparam name="T">The class.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="errorBody">if set to <c>true</c> [error body].</param>
        /// <returns>
        /// The http result.
        /// </returns>
        protected IHttpActionResult GetResult<T>(SuccessResult<IEnumerable<T>> result, bool errorBody = false) where T : BaseModel
        {
            if (result == null)
            {
                return this.StatusCode(HttpStatusCode.InternalServerError);
                ////return this.Content<T>(HttpStatusCode.InternalServerError, default(T));
            }
            else if (!result.IsValid || !result.IsSuccessStatusCode)
            {
                if (errorBody && !string.IsNullOrWhiteSpace(result.Message))
                {
                    return this.GetErrorResult((HttpStatusCode)result.Code, result.Message);
                }

                return this.StatusCode((HttpStatusCode)result.Code);
            }
            else
            {
                this.SetHateoasLinks<T>(result.Item);

                return this.Content<IEnumerable<T>>((HttpStatusCode)result.Code, result.Item);
            }
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <typeparam name="T">The class.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="errorBody">if set to <c>true</c> [error body].</param>
        /// <returns>
        /// The http result.
        /// </returns>
        protected IHttpActionResult GetResult<T>(SuccessResult<T> result, string routeName, object routeValues, bool errorBody = false) where T : BaseModel
        {
            if (result == null)
            {
                return this.StatusCode(HttpStatusCode.InternalServerError);
                ////return this.Content<T>(HttpStatusCode.InternalServerError, default(T));
            }
            else if (!result.IsValid)
            {
                if (errorBody && !string.IsNullOrWhiteSpace(result.Message))
                {
                    return this.GetErrorResult((HttpStatusCode)result.Code, result.Message);
                }

                return this.StatusCode((HttpStatusCode)result.Code);
            }
            else
            {
                this.SetHateoasLinks<T>(result.Item);
                return this.Content<T>((HttpStatusCode)result.Code, result.Item, routeName, routeValues);
            }
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <typeparam name="T">The base model.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="link">The link.</param>
        /// <param name="errorBody">if set to <c>true</c> [error body].</param>
        /// <returns>
        /// The http result.
        /// </returns>
        protected IHttpActionResult GetResult<T>(SuccessResult<T> result, string link, bool errorBody = false) where T : BaseModel
        {
            string routeName = string.Empty;
            object routeValues = null;

            if (result.IsValid && result.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(link))
            {
                ////Link itemLink = result.Item.GetLinks().FirstOrDefault(l => l.Name.Equals(link));
                ////if (itemLink != null)
                ////{
                ////    routeName = itemLink.RouteName;
                ////    routeValues = itemLink.RouteValues;
                ////}
            }

            return this.GetResult<T>(result, routeName, routeValues, errorBody);
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <typeparam name="T">The base model.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="errorBody">if set to <c>true</c> [error body].</param>
        /// <returns>
        /// The http result.
        /// </returns>
        protected IHttpActionResult GetResult<T>(SuccessResult<T> result, bool errorBody = false) where T : BaseModel
        {
            return this.GetResult<T>(result, string.Empty, errorBody);
        }

        /// <summary>
        /// Gets the route values.
        /// </summary>
        /// <returns>The route values from query string.</returns>
        protected IDictionary<string, object> GetRouteValues()
        {
            IHttpRouteData routeData = this.Request.GetRouteData();
            IDictionary<string, object> routeValues = routeData != null ? routeData.Values : new Dictionary<string, object>();

            IEnumerable<KeyValuePair<string, string>> queryNameValuePairs = this.Request.GetQueryNameValuePairs();

            foreach (var value in queryNameValuePairs)
            {
                routeValues[value.Key] = value.Value;
            }

            return routeValues;
        }

        /// <summary>
        /// Gets the search result.
        /// </summary>
        /// <typeparam name="T">The Base Model</typeparam>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="obj">The object.</param>
        /// <returns>The http result.</returns>
        protected IHttpActionResult GetSearchResult<T>(string routeName, IDictionary<string, object> routeValues, PagedList<T> obj) where T : BaseModel
        {
            PagedResult<T> result = this.GetPagedResult<T>(routeName, routeValues, obj);

            return this.Ok<PagedResult<T>>(result);
        }
        
        /// <summary>
        /// Prepares the group by property.
        /// </summary>
        /// <typeparam name="T">Any class object</typeparam>
        /// <param name="groupBy">The group by.</param>
        /// <param name="obj">The object.</param>
        /// <param name="tableName">The Name of Table.</param>
        /// <returns>List of Property list, by which we can create dynamic query</returns>
        protected string PrepareGroupByProp<T>(string groupBy, T obj, string tableName)
        {
            var propList = new Dictionary<Tuple<string, string>, string>();
            var properties = obj.GetType().GetProperties();
            bool isDeletedExists = false;
            if (obj != null)
            {
                foreach (var p in properties)
                {
                    string name = p.Name;

                    PropertyInfo prop = obj.GetType().GetProperty(name);
                    if (prop != null && !p.PropertyType.FullName.ToLower().Contains("date"))
                    {
                        object vall = prop.GetValue(obj, null);
                        if (vall != null)
                        {
                            propList.Add(Tuple.Create(name, vall.ToString()), p.PropertyType.Name);
                        }
                    }

                    if (name.ToLower().Contains("deletedat"))
                    {
                        isDeletedExists = true;
                    }
                }
            }

            string whereClause = string.Empty;
            if (isDeletedExists)
            {
                whereClause = " where DeletedAt is null ";
            }
            else
            {
                whereClause = " where 1 = 1 ";
            }

            string que = "select @columns, Count(Id) as Total from " + tableName + " @whereClause group by @columns";
            que = que.Replace("@columns", groupBy.TrimEnd(','));

            if (propList.Count > 0)
            {
                foreach (var prop in propList)
                {
                    Tuple<string, string> tempProp = prop.Key;
                    if (prop.Value.ToString().ToLower() == "string")
                    {
                        whereClause += " and " + tempProp.Item1 + " like '%" + tempProp.Item2 + "%'";
                    }
                    else
                    {
                        if (tempProp.Item2.ToString().ToLower() == "true" || tempProp.Item2.ToString().ToLower() == "false")
                        {
                            whereClause += " and " + tempProp.Item1 + " = " + (tempProp.Item2.ToString().ToLower() == "true" ? 1 : 0);
                        }
                        else
                        {
                            whereClause += " and " + tempProp.Item1 + " = " + tempProp.Item2;
                        }
                    }
                }
            }

            que = que.Replace("@whereClause", whereClause);

            return que;
        }

        /// <summary>
        /// Sets the HATEOAS links.
        /// </summary>
        /// <typeparam name="T">The Base Model</typeparam>
        /// <param name="result">The result.</param>
        protected void SetHateoasLinks<T>(object result) where T : BaseModel
        {
            IEnumerable enumerable = result as IEnumerable;
            if (enumerable != null)
            {
                foreach (var item in enumerable)
                {
                    this.GenerateHateoasLinks<T>(item);
                }
            }
            else
            {
                this.GenerateHateoasLinks<T>(result);
            }
        }

        /// <summary>
        /// Sets the page links.
        /// </summary>
        /// <typeparam name="T">The Base Model</typeparam>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="result">The result.</param>
        /// <param name="pageParam">The page parameter.</param>
        protected void SetPageLinks<T>(string routeName, object routeValues, PagedResult<T> result, PageParam pageParam) where T : class
        {
            result._links.Clear();

            if (pageParam.IsPageProvided)
            {
                this.PaginationLinks<T>(routeName, routeValues, result, pageParam);
            }
            else
            {
                this.OffsetLinks<T>(routeName, routeValues, result, pageParam);
            }
        }

        /// <summary>
        /// Sets the authentication cookie.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="isRememberMe">if set to <c>true</c> [is remember me].</param>
        ////protected void SetAuthCookie(string token, bool isRememberMe)
        ////{
        ////    HttpCookie cookie = new HttpCookie(Constant.AUTHTOKEN, token.Split(';')[0]) { HttpOnly = true, Domain = Configurations.CookieDomain, Secure = Configurations.IsSecureCookie, Path = "/" };
        ////    if (isRememberMe)
        ////    {
        ////        cookie.Expires = DateTime.UtcNow.AddDays(Configurations.CookieExpirationDays);
        ////    }

        ////    ////HttpCookie cookieDatetime = new HttpCookie(Constant.AUTHXAUTHTOKENTIME, (DateTime.UtcNow.AddSeconds(60) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds.ToString()) { HttpOnly = true, Domain = BaseConfigurations.CookieDomain, Secure = BaseConfigurations.IsSecureCookie, Path = "/", Expires = DateTime.UtcNow.AddSeconds(60) };
        ////    HttpCookie cookieDatetime = new HttpCookie(Constant.TOKENTIME, (DateTime.UtcNow.AddSeconds(Convert.ToInt32(token.Split(';')[1])) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds.ToString()) { HttpOnly = true, Domain = Configurations.CookieDomain, Secure = Configurations.IsSecureCookie, Path = "/", Expires = DateTime.UtcNow.AddSeconds(Convert.ToInt32(token.Split(';')[1])) };
        ////    HttpContext.Current.Response.SetCookie(cookie);
        ////    HttpContext.Current.Response.SetCookie(cookieDatetime);
        ////}

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <typeparam name="T">The base model.</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>
        /// The http result.
        /// </returns>
        protected IHttpActionResult GetDynamicResult<T>(SuccessResult<PagedList<T>> result, string routeName, object routeValues) where T : class
        {
            if (result == null)
            {
                return this.StatusCode(HttpStatusCode.InternalServerError);
                ////return this.Content<T>(HttpStatusCode.InternalServerError, default(T));
            }
            else if (!result.IsValid || !result.IsSuccessStatusCode)
            {
                return this.StatusCode((HttpStatusCode)result.Code);
                ////return this.Content<T>((HttpStatusCode)result.Code, default(T));
            }
            else
            {
                PagedResult<T> pagedResult = new PagedResult<T>(result.Item);

                if (!(string.IsNullOrWhiteSpace(routeName) || routeValues == null))
                {
                    this.SetPageLinks<T>(routeName, routeValues, pagedResult, result.Item);
                }

                return this.Content<PagedResult<T>>((HttpStatusCode)result.Code, pagedResult);
            }
        }

        /// <summary>
        /// Gets the dynamic result.
        /// </summary>
        /// <typeparam name="T">Type T</typeparam>
        /// <param name="result">The result.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="errorBody">if set to <c>true</c> [error body].</param>
        /// <returns>
        /// returns dynamic result
        /// </returns>
        protected IHttpActionResult GetDynamicResult<T>(SuccessResult<T> result, string routeName, object routeValues, bool errorBody = false) where T : class
        {
            if (result == null)
            {
                return this.StatusCode(HttpStatusCode.InternalServerError);
                ////return this.Content<T>(HttpStatusCode.InternalServerError, default(T));
            }
            else if (!result.IsValid || !result.IsSuccessStatusCode)
            {
                if (errorBody && !string.IsNullOrWhiteSpace(result.Message))
                {
                    return this.GetErrorResult((HttpStatusCode)result.Code, result.Message);
                }

                return this.StatusCode((HttpStatusCode)result.Code);
            }
            else
            {
                return this.Content<T>((HttpStatusCode)result.Code, result.Item, routeName, routeValues);
            }
        }

        /// <summary>
        /// Generates the HATEOAS links.
        /// </summary>
        /// <typeparam name="T">The Base Model</typeparam>
        /// <param name="obj">The object.</param>
        private void GenerateHateoasLinks<T>(object obj) where T : BaseModel
        {
            IDictionary<string, string> links = new Dictionary<string, string>();

            BaseModel model = obj as T;
            ////this.SetJsonIgnoreCondition();

            ////foreach (var link in model.GetLinks())
            ////{
            ////    if (link.IgnoreFlags != null && link.IgnoreFlags.Count() > 0)
            ////    {
            ////        if (JsonIgnoreConditionalResolver.CheckLinkToGenerate(link.IgnoreFlags))
            ////        {
            ////            links.Add(link.Name, this.GetLink(link.RouteName, link.RouteValues));
            ////        }
            ////    }
            ////    else
            ////    {
            ////        links.Add(link.Name, this.GetLink(link.RouteName, link.RouteValues));
            ////    }
            ////}

            ////model.SetHateoas(links);
        }

        /// <summary>
        /// Gets the paged result.
        /// </summary>
        /// <typeparam name="T">The Base Model</typeparam>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// The paged result.
        /// </returns>
        private PagedResult<T> GetPagedResult<T>(string routeName, object routeValues, PagedList<T> obj) where T : BaseModel
        {
            PagedResult<T> result = new PagedResult<T>(obj);

            if (!(result.Items == null || result.Items.Count == 0))
            {
                this.SetHateoasLinks<T>(result.Items);
            }

            if (!(string.IsNullOrWhiteSpace(routeName) || routeValues == null))
            {
                this.SetPageLinks<T>(routeName, routeValues, result, obj);
            }

            return result;
        }

        /// <summary>
        /// Gets the name of the versioned route.
        /// </summary>
        /// <param name="routeName">Name of the route.</param>
        /// <returns>The versioned route</returns>
        private string GetVersionedRouteName(string routeName)
        {
            return this.Version + routeName;
        }

        /// <summary>
        /// Sets the offset links.
        /// </summary>
        /// <typeparam name="T">The class.</typeparam>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="result">The result.</param>
        /// <param name="pageParam">The page parameter.</param>
        private void OffsetLinks<T>(string routeName, object routeValues, PagedResult<T> result, PageParam pageParam) where T : class
        {
            IDictionary<string, object> routeDictionary = routeValues as IDictionary<string, object>;

            routeDictionary.Remove("page");
            routeDictionary.Remove("pageSize");

            routeDictionary["limit"] = pageParam.Limit;

            if (result.TotalResults > 0)
            {
                ////routeValues["offset"] = offset;
                ////string current = this.GetLink(routeName, routeValues);
                ////result._links.Add("current", current);

                if (pageParam.Offset > 0)
                {
                    routeDictionary["offset"] = pageParam.Offset - pageParam.Limit;
                    string previous = this.GetLink(routeName, routeDictionary);
                    result._links.Add("previous", previous);

                    ////routeValues["offset"] = 0;
                    ////string first = this.GetLink(routeName, routeValues);
                    ////result._links.Add("first", first);
                }

                if (pageParam.Offset + pageParam.Limit < result.TotalResults)
                {
                    routeDictionary["offset"] = pageParam.Offset + pageParam.Limit;
                    string next = this.GetLink(routeName, routeDictionary);
                    result._links.Add("next", next);

                    //////routeValues["offset"] = result.TotalResults;
                    //////string last = this.GetLink(routeName, routeValues);
                    //////result._links.Add("last", last);
                }
            }
        }

        /// <summary>
        /// Sets the pagination links.
        /// </summary>
        /// <typeparam name="T">The class.</typeparam>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="result">The result.</param>
        /// <param name="pageParam">The page parameter.</param>
        private void PaginationLinks<T>(string routeName, object routeValues, PagedResult<T> result, PageParam pageParam) where T : class
        {
            IDictionary<string, object> routeDictionary = routeValues as IDictionary<string, object>;

            routeDictionary.Remove("limit");

            routeDictionary["pageSize"] = pageParam.PageSize;

            if (result.TotalResults > 0)
            {
                if (pageParam.Page > 1)
                {
                    routeDictionary["page"] = pageParam.Page - 1;
                    string previous = this.GetLink(routeName, routeDictionary);
                    result._links.Add("previous", previous);
                }

                if ((pageParam.Page * pageParam.PageSize) < result.TotalResults)
                {
                    routeDictionary["page"] = pageParam.Page + 1;
                    string next = this.GetLink(routeName, routeDictionary);
                    result._links.Add("next", next);

                    //////routeValues["offset"] = result.TotalResults;
                    //////string last = this.GetLink(routeName, routeValues);
                    //////result._links.Add("last", last);
                }
            }
        }

        /// <summary>
        /// Sets the JSON ignore condition.
        /// </summary>
        private void SetJsonIgnoreCondition()
        {
            ////if (Request.Properties.Any(p => p.Key.Equals(Constant.AUTHVALIDATEDUSERHEADERKEY, StringComparison.InvariantCulture)))
            ////{
            ////    JsonIgnoreConditionalResolver.ParseAuthMetaData(Convert.ToString(Request.Properties[Constant.AUTHVALIDATEDUSERHEADERKEY]));
            ////}
        }

        #endregion Private Methods
    }    
}