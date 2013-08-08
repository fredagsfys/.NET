<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>ASP.NET WebForms</h2>
    <blockquote>
        <p>Subject: Datavetenskap</p>
        <p>Level: Grundnivå</p>
        <p>Universitypoints: 7,5</p>
        <p>Coursecode: 1DV406</p>
    </blockquote>
    <blockquote>
        <p><b>Welcome to "The Forum"</b></p>
        <small>Before anything please register <a runat="server" href="~/Account/Register.aspx" title="Register">here</a>.</small>
    </blockquote>
</asp:Content>

