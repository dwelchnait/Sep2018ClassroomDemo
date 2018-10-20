<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NestedRepeater.aspx.cs" Inherits="WebApp.SamplePages.NestedRepeater" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Nested Repeater</h1>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <asp:Repeater ID="DTORepeater" runat="server"
        DataSourceID="DTORepeaterODS"
        ItemType="Chinook.Data.DTOs.AnAlbum">
        <HeaderTemplate>
            <h3>Album Song List</h3>
        </HeaderTemplate>
        <ItemTemplate>
            <h4><%# Item.title %> (<%# Item.artist %>)</h4><br />
            <asp:Repeater ID="POCORepeater" runat="server"
                 DataSource="<%# Item.songs %>"
                 ItemType="Chinook.Data.POCOs.Song">
                <ItemTemplate>
                    <%# Item.songname %> : <%# Item.length %> (M:S) <br />
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
    <asp:ObjectDataSource ID="DTORepeaterODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Album_GetAlbumAndSongs" 
        TypeName="ChinookSystem.BLL.AlbumController"
         OnSelected="CheckForException">

    </asp:ObjectDataSource>
</asp:Content>
