<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EmployeeXML._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Employee Data</h2>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true">
    </asp:GridView>

</asp:Content>