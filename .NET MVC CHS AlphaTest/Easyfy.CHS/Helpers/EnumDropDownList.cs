using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Easyfy.CHS.Helpers
{
  public static class EnumDropDownList
  {
    private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
    {
      Type realModelType = modelMetadata.ModelType;

      Type underlyingType = Nullable.GetUnderlyingType(realModelType);
      if (underlyingType != null)
      {
        realModelType = underlyingType;
      }
      return realModelType;
    }
    private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };

    //public static HtmlString EnumDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> modelExpression, string firstElement)
    //{
    //  var typeOfProperty = modelExpression.ReturnType;
    //  if (!typeOfProperty.IsEnum)
    //    throw new ArgumentException(string.Format("Type {0} is not an enum", typeOfProperty));

    //  var enumValues = new SelectList(Enum.GetValues(typeOfProperty));
    //  return htmlHelper.DropDownListFor(modelExpression, enumValues, firstElement);
    //}

    public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string firstElement = null, object htmlAttribute = null)
    {
      ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
      Type enumType = GetNonNullableModelType(metadata);
      IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>().AsEnumerable();

      TypeConverter converter = TypeDescriptor.GetConverter(enumType);

      IEnumerable<SelectListItem> items =
          from value in values
          select new SelectListItem
          {
            Text = converter.ConvertToString(value),
            Value = value.ToString(),
            Selected = value.Equals(metadata.Model)
          };

      if (metadata.IsNullableValueType)
      {
        items = SingleEmptyItem.Concat(items);
      }

      return htmlHelper.DropDownListFor(
          expression,
          items,
          firstElement,
          htmlAttribute
          );
    }


  }
}
