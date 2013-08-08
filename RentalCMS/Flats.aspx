<%@ Page Title="" Language="C#" MasterPageFile="~/Rental.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Flats.aspx.cs" Inherits="RentalCMS.Flats" %>

<%@ Import Namespace="System.ComponentModel" %>
<%@ Register Assembly="SmartControls" Namespace="SmartControls" TagPrefix="Smart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainAreaOfPage" runat="server">






    <section class="choices">
        <fieldset>
            <legend>Фильтры</legend>
            <p id="_errorText" runat="server" class="error2 hide">
                <asp:Label ID="_lbError" runat="server" Text=""></asp:Label>
            </p>
            <div class="left">
                <span class="mask">
                    <asp:TextBox runat="server" ID="_tbSearchText" name="_tbSearchText"></asp:TextBox>
                </span>
            </div>

            <br class="clearLeft" />
            <div class="lsCheck">
                <div class="row">

                    <asp:RadioButton ID="_cbAddress" runat="server" Checked="true" GroupName="filter" />



                    <label for="_cbAddress" title="">
                        Адрес 
                    </label>


                    <asp:RadioButton ID="_cbRoomCount" runat="server" Checked="false" GroupName="filter" />


                    <label for="_cbRoomCount" title="">
                        Комнат 
                    </label>



                    <asp:RadioButton ID="rbFloor" runat="server" Checked="false" GroupName="filter" />


                    <label title="">
                        Этаж 
                    </label>


                    <asp:RadioButton ID="rbPrice" runat="server" Checked="false" GroupName="filter" />


                    <label title="">
                        Цена 
                    </label>


                    <asp:RadioButton ID="rbFurniture" runat="server" Checked="false" GroupName="filter" />


                    <label title="">
                        Мебель 
                    </label>


                    <asp:RadioButton ID="rbDistinct" runat="server" Checked="false" GroupName="filter" />


                    <label title="">
                        Район 
                    </label>

                </div>
            </div>
            <div class="nrmRow">
                <div class="areaGroup areaGroup_fix1">
                    <div class="row">
                        <strong class="calendar_info">Временной интервал
                        </strong>
                    </div>
                    <div class="row calendar">
                        <label title="Select Start Date" for="_tbStartDateText" class="above">
                            Начало
                        </label>
                        <span class="mask">
                            <asp:TextBox runat="server" ID="_tbStartDateText" name="_tbStartDateText" CssClass="datePicker"></asp:TextBox>
                        </span>
                    </div>
                    <div class="row calendar">
                        <label title="Select End Date" for="_tbEndDateText" class="above">
                            Конец
                        </label>
                        <span class="mask">
                            <asp:TextBox runat="server" ID="_tbEndDateText" name="_tbEndDateText" CssClass="datePicker"></asp:TextBox>
                        </span>
                    </div>
                </div>
                <br class="clearLeft" />
            </div>
            <div class="panel">
                <asp:LinkButton ID="_btFilterData" runat="server" CssClass="button " OnClick="_btFilterData_Click">
                    Применить
                </asp:LinkButton>
                <asp:LinkButton ID="_lbClearData" runat="server" CssClass="button " OnClick="_btClearData_Click">
                    Сбросить
                </asp:LinkButton><br class="clearLeft" />
            </div>
            <br class="clearLeft" />
        </fieldset>
    </section>

















    <div style="overflow: auto; overflow-y: hidden;">

        <asp:ListView ID="_lwInfoListEdit" runat="server" OnSorting="_lwInfoList_Sorting" OnItemCommand="_lwInfoList_ItemCommand">
            <LayoutTemplate>
                <table class="grid">
                    <thead>
                        <tr>
                            <th>
                                <asp:LinkButton ID="lkId" runat="server" CommandName="Sort" CommandArgument="1">
                                    №
                                <span id="sort1" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>

                            <th>
                                <asp:LinkButton ID="lkData" runat="server" CommandName="Sort" CommandArgument="2">
                                    Дата
                                <span id="sort2" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>

                            <th>
                                <asp:LinkButton ID="lkRoomCount" runat="server" CommandName="Sort" CommandArgument="4">
                                    Комнат
                                <span id="sort4" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>

                            <th>
                                <asp:LinkButton ID="lkAddress" runat="server" CommandName="Sort" CommandArgument="8">
                                    Адрес
                                <span id="sort8" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>

                            <th>
                                <asp:LinkButton ID="lbDistinct" runat="server" CommandName="Sort" CommandArgument="256">
                                    Район
                                <span id="sort256" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>

                            <th>
                                <asp:LinkButton ID="lbFloor" runat="server" CommandName="Sort" CommandArgument="32">
                                    Этаж
                                <span id="sort32" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>

                            <th>
                                <asp:LinkButton ID="lbPrice" runat="server" CommandName="Sort" CommandArgument="64">
                                    Цена
                                <span id="sort64" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>


                            <th>
                                <asp:LinkButton ID="lbFurniture" runat="server" CommandName="Sort" CommandArgument="128">
                                    Мебель
                                <span id="sort128" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>






                            <th class="icon">&nbsp;</th>
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
                        <asp:HiddenField ID="hfID" Value='<%# Eval("ID") %>' runat="server" />
                        <asp:Label runat="server" ID="lbId">  <%# Eval("ID") %> </asp:Label>
                    </td>

                    <td>
                        <asp:Label runat="server" ID="lbData"> <%# ((DateTime)Eval("DATA")).ToShortDateString() %> </asp:Label>
                    </td>


                    <td>
                        <asp:Label runat="server" ID="lblRoomCount"> <%# Eval("ROOM_COUNT") %> </asp:Label>
                    </td>


                    <td>
                        <asp:Label runat="server" ID="lbAddress"> <%# Eval("ADDRESS") %> </asp:Label>
                    </td>

                    <td>
                        <asp:Label runat="server" ID="lbDistinct"> <%# Eval("REGION") %> </asp:Label>
                    </td>


                    <td>
                        <asp:Label runat="server" ID="lbFloor"> <%# Eval("FLOOR") %> </asp:Label>
                    </td>

                    <td>
                        <asp:Label runat="server" ID="lbPrice"> <%# Eval("PRICE") %> </asp:Label>
                    </td>

                    <td>
                        <asp:Label runat="server" ID="lbFurniture"> <%# Eval("FURNITURE") %> </asp:Label>
                    </td>



                    <td class="icon">
                        <asp:LinkButton ID="lbEdit" runat="server" CommandName="Action" CommandArgument="Edit" title='Посмотреть'>
                         <image src="/images/icon_view.png" width="21" height="20" title='Просмотреть' alt=""/>
                        </asp:LinkButton>
                    </td>

                </tr>
            </ItemTemplate>
        </asp:ListView>

    </div>

    <section class="footer-tbl" id="_navPag" runat="server">
        <div class="itemsPg">
            <div class="dropDown" id="pageItems_f">

                <asp:DropDownList ID="pageItems" runat="server" AutoPostBack="True" OnSelectedIndexChanged="pageItems_SelectedIndexChanged">
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                    <asp:ListItem Text="50" Value="50" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="rowscounters">
                <asp:Label ID="lbSelNumPerPage" runat="server"></asp:Label>
            </div>
        </div>
        <nav class="pager">
            <Smart:SmartPagingLink ID="_plInfoGrid" runat="server" OnPaging="_plInfoGrid_Paging"
                CssActivePage="selected"
                ImageFirstPage="/images/icon_pg_left_1.png"
                ImageBackPage="/images/icon_pg_left.png"
                ImageNextPage="/images/icon_pg_right.png"
                ImageLastPage="/images/icon_pg_right_2.png" />
        </nav>
        <br class="clearLeft" />
    </section>


    <%-- <script type="text/javascript">
        $.fn.adaptTableWidth('grid', { 'grid': '670px' }, { 0: '30px', 1: '65px', 2: '65px', 3: '100px', 4: '80px', 5: '21px', 6: '21px', 7: '21px', 8: '21px', 9: '21px' });


        $(".datePicker").datepicker({ appendText: '(dd.mm.yyyy)', dateFormat: 'dd.mm.yy' }).val();


    </script>--%>
</asp:Content>
