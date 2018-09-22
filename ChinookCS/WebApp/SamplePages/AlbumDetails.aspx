<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlbumDetails.aspx.cs" Inherits="WebApp.SamplePages.AlbumDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Accessing Data from ListView and GridView</h3>
    <asp:Label ID="Label1" runat="server" 
        Text="Select Album: "></asp:Label>
    &nbsp;&nbsp;
    <asp:DropDownList ID="AlbumIDArg" runat="server" 
        DataSourceID="AlbumIDArgODS" 
        DataTextField="Title" 
        DataValueField="AlbumId"></asp:DropDownList>
    <asp:LinkButton ID="FetchDetails" runat="server" OnClick="FetchDetails_Click">Fetch Details</asp:LinkButton>
    <asp:ObjectDataSource ID="AlbumIDArgODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Album_List" 
        TypeName="ChinookSystem.BLL.AlbumController">
       
    </asp:ObjectDataSource>
     <div class="row">
        <asp:ListView ID="AlbumTracks" runat="server" 
            DataSourceID="AlbumTrackODS"
             OnItemCommand="AlbumTracks_ItemCommand">
            <AlternatingItemTemplate>
                <tr style="background-color: #FFF8DC;">
                    <td>
                        <asp:Label Text='<%# Eval("TrackId") %>' runat="server" ID="TrackIdLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("MediaTypeId") %>' runat="server" ID="MediaTypeIdLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("GenreId") %>' runat="server" ID="GenreIdLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Composer") %>' runat="server" ID="ComposerLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Milliseconds") %>' runat="server" ID="MillisecondsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Bytes") %>' runat="server" ID="BytesLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("UnitPrice") %>' runat="server" ID="UnitPriceLabel" /></td>
                                       <td>
                        <asp:Button ID="SelectButton" runat="server" Text="Pick"
                            CommandName="Select" 
                            CommandArgument='<%# Eval("TrackId") %>' />

                    </td>
                </tr>
            </AlternatingItemTemplate>
          
            <EmptyDataTemplate>
                <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
          
            <ItemTemplate>
                <tr style="background-color: #DCDCDC; color: #000000;">
                    <td>
                        <asp:Label Text='<%# Eval("TrackId") %>' runat="server" ID="TrackIdLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                   
                    <td>
                        <asp:Label Text='<%# Eval("MediaTypeId") %>' runat="server" ID="MediaTypeIdLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("GenreId") %>' runat="server" ID="GenreIdLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Composer") %>' runat="server" ID="ComposerLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Milliseconds") %>' runat="server" ID="MillisecondsLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Bytes") %>' runat="server" ID="BytesLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("UnitPrice") %>' runat="server" ID="UnitPriceLabel" /></td>
                    <td>
                        <asp:Button ID="SelectButton" runat="server" Text="Pick"
                            CommandName="Select" 
                            CommandArgument='<%# Eval("TrackId") %>' />

                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                    <th runat="server">Id</th>
                                    <th runat="server">Name</th>
                                    <th runat="server">Media</th>
                                    <th runat="server">Genre</th>
                                    <th runat="server">Composer</th>
                                    <th runat="server">Milliseconds</th>
                                    <th runat="server">Bytes</th>
                                    <th runat="server">UnitPrice</th>
                                    <th runat="server"></th>
                                </tr>
                                <tr runat="server" id="itemPlaceholder"></tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                            <asp:DataPager runat="server" ID="DataPager1">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
           
        </asp:ListView>
        <asp:ObjectDataSource ID="AlbumTrackODS" runat="server" 
            OldValuesParameterFormatString="original_{0}" 
            SelectMethod="Track_GetByAlbumID" 
            TypeName="ChinookSystem.BLL.TrackController">
            <SelectParameters>
                <asp:ControlParameter ControlID="AlbumIDArg" 
                    PropertyName="Text" 
                    DefaultValue="0" 
                    Name="albumid" 
                    Type="Int32"></asp:ControlParameter>
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <div class="row">
        <asp:Label ID="Label3" runat="server" Text="Total time and size"></asp:Label>&nbsp;&nbsp;
        <asp:LinkButton ID="Totals" runat="server" OnClick="Totals_Click" >Totals</asp:LinkButton>&nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Text="Time: "></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="TracksTime" runat="server" ></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="Label7" runat="server" Text="Size: "></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="TracksSize" runat="server" ></asp:Label>
    </div>
     <div class="row">
        <asp:Label ID="Label2" runat="server" Text="You picked track id: "></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="Label5" runat="server" Text="Command Arg: "></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="CommandArgID" runat="server" ></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="Label6" runat="server" Text="Track ID : "></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="ColumnID" runat="server" ></asp:Label>
    </div>
    <div class="row">
        <asp:GridView ID="TrackOfAlbums" runat="server"
            DataSourceID="AlbumTrackODS" AutoGenerateColumns="False" 
            AllowPaging="True" OnSelectedIndexChanged="TrackOfAlbums_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("TrackId") %>' runat="server" ID="TrackIdLabel" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Media">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("MediaTypeId") %>' runat="server" ID="MediaTypeIdLabel" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Genre">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("GenreId") %>' runat="server" ID="GenreIdLabel" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Composer">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Composer") %>' runat="server" ID="ComposerLabel" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Size">
                    <ItemTemplate>
                    <asp:Label Text='<%# Eval("Bytes") %>' runat="server" ID="BytesLabel" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Time (msec)">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Milliseconds") %>' runat="server" ID="MillisecondsLabel" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("UnitPrice") %>' runat="server" ID="UnitPriceLabel" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField SelectText="View" ShowSelectButton="True" ButtonType="Button"></asp:CommandField>
            </Columns>
        </asp:GridView>
        <div class="row">
        <asp:Label ID="Label8" runat="server" Text="Total time and size"></asp:Label>&nbsp;&nbsp;
        <asp:LinkButton ID="Sum" runat="server" OnClick="Sum_Click" >Totals</asp:LinkButton>&nbsp;&nbsp;
        <asp:Label ID="Label9" runat="server" Text="Time: "></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="SumTime" runat="server" ></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="Label11" runat="server" Text="Size: "></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="SumSize" runat="server" ></asp:Label>
    </div>
     <div class="row">
        <asp:Label ID="Label13" runat="server" Text="You picked track id: "></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="Label14" runat="server" Text="Gridview Index: "></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="GridViewIndex" runat="server" ></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="Label16" runat="server" Text="Track ID: "></asp:Label>&nbsp;&nbsp;
        <asp:Label ID="SumTrackId" runat="server" ></asp:Label>
    </div>
    </div>
</asp:Content>
