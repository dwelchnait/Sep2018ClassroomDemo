<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SimpleRepeater.aspx.cs" Inherits="WebApp.SamplePages.SimpleRepeater" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> Simple Repeater using POCO class</h1>
    <blockquote>
        This form will display data created by a Linq query.
        The data is a flat dataset described in the POCO folder of
        Chinook.Data.POCOs. The data will be display in a
        Repeater control.
    </blockquote>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <asp:Repeater ID="PlaylistSummaryList" runat="server"
        DataSourceID="PlaylistSummaryListODS"
        ItemType="Chinook.Data.POCOs.PlaylistSummary">
        <HeaderTemplate>
            <h4>Playlist Summary</h4>
            <table class="table">
                <tr>
                    <th>Name</th>
                    <th>Tracks</th>
                    <th>Cost</th>
                    <th>Storage</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
            <td><%# Item.name %></td> 
            <td><%# Item.trackcount %> </td> 
            <td><%# Item.cost %> </td> 
            <td><%# Item.storage %> </td> 

            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
            &copy; Chinook reserves all rights to this data.
        </FooterTemplate>
    </asp:Repeater>
    <asp:ObjectDataSource ID="PlaylistSummaryListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Playlist_GetPlaylistSummary" 
        TypeName="ChinookSystem.BLL.PlaylistController"
         OnSelected="CheckForException"></asp:ObjectDataSource>

</asp:Content>
