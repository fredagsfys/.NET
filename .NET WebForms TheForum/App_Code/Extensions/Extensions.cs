using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Reflection;

/// <summary>
/// Extension class that got two methods to add Error Messages from the .aspx pages. In other words it creates
/// a message if the validationsummary doesnt catch it and throws it back to validationsummary and its validationgroup.
/// </summary>

[DataObject(false)]
public static class Extensions
{
    #region Methods

    public static void AddErrorMessage(this Page page, string message, string validationGroup = null)
    {
        var validator = new CustomValidator
        {
            IsValid = false,
            ErrorMessage = message,
            ValidationGroup = validationGroup
        };

        page.Validators.Add(validator);
    }

    public static void AddErrorMessage(this Page page, IDataErrorInfo obj, string validationGroup = null)
    {
        obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => !String.IsNullOrWhiteSpace(obj[p.Name]))
            .Select(p => p.Name)
            .ToList()
            .ForEach(propertyName => AddErrorMessage(page, obj[propertyName], validationGroup));
    }

    #endregion
}