//-----------------------------------------------------------------------
// <copyright file="BaseModel.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Class base model
    /// </summary>
    public abstract class BaseModel
    {
        private static readonly object LockObject = new object();
        private List<string> fieldsToNull;

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public virtual Nullable<DateTime> CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        /// <value>
        /// The modified on.
        /// </value>
        public virtual Nullable<DateTime> UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the delete on.
        /// </summary>
        /// <value>
        /// The deleted on.
        /// </value>
        public virtual Nullable<DateTime> DeletedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public virtual Nullable<int> CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        public virtual Nullable<int> UpdatedBy { get; set; }


        /// <summary>
        /// Gets or sets the deleted by.
        /// </summary>
        /// <value>
        /// The deleted by.
        /// </value>
        public virtual Nullable<int> DeletedBy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseModel"/> class.
        /// </summary>
        protected BaseModel()
        {
            this.fieldsToNull = new List<string>();
        }

        /// <summary>
        /// Sets the fields to null.
        /// </summary>
        /// <value>
        /// The fields to null.
        /// </value>
        public List<string> FieldsToNull
        {
            set
            {
                if (value != null && value.Any())
                {
                    this.fieldsToNull.AddRange(value.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x = x.Trim()).Distinct(StringComparer.OrdinalIgnoreCase));
                }
            }
        }
        
        /// <summary>
        /// Gets the fields to null.
        /// </summary>
        /// <returns> value of fields to null</returns>
        public object GetFieldsToNull()
        {
            return this.fieldsToNull.Any() ? string.Join(",", this.fieldsToNull.Where(s => !string.IsNullOrWhiteSpace(s)).ToList()) : (object)DBNull.Value;
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>result as a key value pair as property name and its value and property info as value</returns>
        private Dictionary<KeyValuePair<string, object>, PropertyInfo> GetProperties(object model)
        {
            Dictionary<KeyValuePair<string, object>, PropertyInfo> result = new Dictionary<KeyValuePair<string, object>, PropertyInfo>();

            PropertyInfo[] properties = model.GetType().GetProperties();

            foreach (var item in properties)
            {
                if (item.CanRead)
                {
                    result.Add(new KeyValuePair<string, object>(item.Name, item.GetValue(model, null)), item);
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether the specified property information has attributes.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="type">The type.</param>
        /// <returns>true if element has attribute of specified type</returns>
        private bool HasAttributes(PropertyInfo propertyInfo, Type type)
        {
            return Attribute.IsDefined(propertyInfo, type);
        }
    }
}
