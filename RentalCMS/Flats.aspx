<%@ Page Title="" Language="C#" MasterPageFile="~/Rental.Master" AutoEventWireup="true" CodeBehind="Flats.aspx.cs" Inherits="RentalCMS.Flats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainAreaOfPage" runat="server">

    <asp:ListView ID="_lwInfoListEdit" runat="server" OnSorting="_lwInfoList_Sorting" OnItemCommand="_lwInfoList_ItemCommand">
        <LayoutTemplate>
            <table class="grid">
                <thead>
                    <tr>
                        <th>
                            <asp:LinkButton ID="lkId" runat="server" CommandName="Sort" CommandArgument="0">
                                Номер
                                <span id="sort0" runat="server" class="sort">&nbsp;</span>
                            </asp:LinkButton>
                        </th>

                        <th>
                            <asp:LinkButton ID="lkData" runat="server" CommandName="Sort" CommandArgument="1">
                                Дата
                                <span id="sort1" runat="server" class="sort">&nbsp;</span>
                            </asp:LinkButton>
                        </th>

                        <th>
                            <asp:LinkButton ID="lkRoomCount" runat="server" CommandName="Sort" CommandArgument="2">
                                Комтан
                                <span id="sort2" runat="server" class="sort">&nbsp;</span>
                            </asp:LinkButton>
                        </th>

                        <th>
                            <asp:LinkButton ID="lkAddress" runat="server" CommandName="Sort" CommandArgument="4">
                                Адресс
                                <span id="sort4" runat="server" class="sort">&nbsp;</span>
                            </asp:LinkButton>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
             
                <td runat="server" id="trNumber">
                    <asp:Label runat="server" ID="lbId">  <%# Eval("ID") %> </asp:Label>
                </td>

                <td>
                    <asp:Label runat="server" ID="lbData"><%#Eval("DATA")%></asp:Label>
                </td>

                
                <td>
                    <asp:Label runat="server" ID="lblRoomCount"><%#Eval("ROOM_COUNT")%></asp:Label>
                </td>

                
                <td>
                    <asp:Label runat="server" ID="lbAddress"><%#Eval("ADDRESS")%></asp:Label>
                </td>

            </tr>
        </ItemTemplate>
    </asp:ListView>


</asp:Content>
