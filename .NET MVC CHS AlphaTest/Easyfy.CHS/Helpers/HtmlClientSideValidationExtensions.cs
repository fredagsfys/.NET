using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Easyfy.CHS.Helpers
{
  public static class HtmlClientSideValidationExtensions
  {
    public static IDisposable BeginAjaxContentValidation(this HtmlHelper html, string formId)
    {
      MvcForm mvcForm = null;

      if (html.ViewContext.FormContext == null)
      {
        html.EnableClientValidation();
        mvcForm = new MvcForm(html.ViewContext);
        html.ViewContext.FormContext.FormId = formId;
      }

      return new AjaxContentValidation(html.ViewContext, mvcForm);
    }

    private class AjaxContentValidation : IDisposable
    {
      private readonly MvcForm _mvcForm;
      private readonly ViewContext _viewContext;

      public AjaxContentValidation(ViewContext viewContext, MvcForm mvcForm)
      {
        _viewContext = viewContext;
        _mvcForm = mvcForm;
      }

      public void Dispose()
      {
        if (_mvcForm != null)
        {
          _viewContext.OutputClientValidation();
          _viewContext.FormContext = null;
        }
      }
    }
  }
}