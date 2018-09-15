<%@ Page Title="ODS Query" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="ODSQuery.aspx.cs" 
    Inherits="WebApp.SamplePages.ODSQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ODS Query</h1>
    <asp:Label ID="Label1" runat="server" Text="Select an artist"></asp:Label>
    <asp:DropDownList ID="ArtistList" runat="server" 
        DataSourceID="ArtistListODS" 
        DataTextField="Name" 
        DataValueField="ArtistId"
         AppendDataBoundItems="true">
        <asp:ListItem Value="0">select ...</asp:ListItem>
    </asp:DropDownList>
    <asp:LinkButton ID="FetchArtist" runat="server">Fetch</asp:LinkButton>
    <br /><br />
    <!-- Bind("xxx") is both input and output
         Eval("xxx") is output only -->
    <asp:GridView ID="AlbumList" runat="server" 
        AutoGenerateColumns="False" 
        DataSourceID="AlbumListODS">
        <Columns>
            <asp:TemplateField HeaderText="Id" SortExpression="AlbumId">
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# Eval("AlbumId") %>' 
                        ID="Label1"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title" SortExpression="Title">
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# Eval("Title") %>' 
                        ID="Label2"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Artist" SortExpression="ArtistId">
                <ItemTemplate>
                    <asp:DropDownList ID="AlbumArtistList" runat="server" 
                        DataSourceID="ArtistListODS" 
                        DataTextField="Name" 
                        DataValueField="ArtistId"
                        selectedvalue='<%# Eval("ArtistId") %>'>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Year" SortExpression="ReleaseYear">
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# Eval("ReleaseYear") %>' 
                        ID="Label4"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Label" SortExpression="ReleaseLabel">
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# Eval("ReleaseLabel") %>' 
                        ID="Label5"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No data is available 
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Artist_List" 
        TypeName="ChinookSystem.BLL.ArtistController">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="AlbumListODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="Album_GetByArtistId"
        TypeName="ChinookSystem.BLL.AlbumController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ArtistList" 
                PropertyName="SelectedValue" DefaultValue="0" 
                Name="artistid" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
