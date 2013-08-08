<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <style>
          .center {text-align: center; margin-left: auto; margin-right: auto; margin-bottom: auto; margin-top: auto;}
        </style>
        <title>Error 404</title>
        <div class="hero-unit center">
        <h1>Page Not Found <small><font face="Tahoma" color="red">Error 404</font></small></h1>
        <br />
        <p>The page you requested could not be found, either contact your webmaster or try again. Use your browsers <b>Back</b> button to navigate to the page you have prevously come from</p>
        <p><b>Or you could just press this neat little button:</b></p>
        <a id="A1" href="~/Members/Forum.aspx" runat="server" class="btn btn-large btn-info"><i class="icon-home"></i> Take Me Home</a>
        </div>
    </form>
</body>
</html>
