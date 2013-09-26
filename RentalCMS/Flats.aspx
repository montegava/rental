<%@ Page Title="" Language="C#" MasterPageFile="~/Rental.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Flats.aspx.cs" Inherits="RentalCMS.Flats" %>

<%@ Import Namespace="System.ComponentModel" %>
<%@ Register Assembly="PageControls" Namespace="SmartControls" TagPrefix="Smart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainAreaOfPage" runat="server">

    <section class="choices">
        <fieldset>
            <legend>Фильтры</legend>

            <div class="row">
                <asp:Literal>Кол-во комнат</asp:Literal>
                <asp:DropDownList runat="server" ID="ddlRoomCount">
                    <asp:ListItem Selected="True" Text="-- не важно --" Value="0"></asp:ListItem>
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text=">5" Value=">5"></asp:ListItem>
                </asp:DropDownList>
                <asp:Literal>Адрес:</asp:Literal>
                <asp:TextBox runat="server" ID="tbAddress" name="_tbSearchText"></asp:TextBox>
            </div>

            <div class="row">
                <asp:Literal>Район</asp:Literal>
                <asp:DropDownList runat="server" ID="ddlRegion">
                    <asp:ListItem Selected="True" Text="-- не важно --" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Коминтерновский" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Ж/Д" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Левобережный" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Советский" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Ленинский" Value="5"></asp:ListItem>
                </asp:DropDownList>
                <asp:Literal>Этаж</asp:Literal>
                <asp:DropDownList runat="server" ID="ddlFloor">
                    <asp:ListItem Selected="True" Text="-- не важно --" Value="0"></asp:ListItem>
                    <asp:ListItem Text="5<" Value="5"></asp:ListItem>
                    <asp:ListItem Text="5-8" Value="5-8"></asp:ListItem>
                    <asp:ListItem Text="9-12" Value="9-12"></asp:ListItem>
                    <asp:ListItem Text=">13" Value="13"></asp:ListItem>
                </asp:DropDownList>
                <asp:Literal>Мебель</asp:Literal>
                <asp:DropDownList runat="server" ID="ddlFurniture">
                    <asp:ListItem Selected="True" Text="-- не важно --" Value="0"></asp:ListItem>
                    <asp:ListItem Text="есть" Value="1"></asp:ListItem>
                    <asp:ListItem Text="нет" Value="2"></asp:ListItem>
                    <asp:ListItem Text="частично" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="row">
                <asp:Literal>Цена</asp:Literal>
                <asp:TextBox runat="server" ID="tbPrice"></asp:TextBox>
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
                <asp:LinkButton ID="_btFilterData" runat="server" CssClass="button " OnClick="ApplyFilters">
                    Применить
                </asp:LinkButton>
                <asp:LinkButton ID="_lbClearData" runat="server" CssClass="button " OnClick="ClearFilters">
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
                                <asp:LinkButton ID="lkId" runat="server" CommandName="Sort" CommandArgument='1'>
                                    <span id="sort1" runat="server" class="sort">&nbsp;</span>
                                    #
                                </asp:LinkButton>
                            </th>

                            <th>
                                <asp:LinkButton ID="lkData" runat="server" CommandName="Sort" CommandArgument='2'>
                                    Дата
                                <span id="sort2" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>

                            <th>
                                <asp:LinkButton ID="lkRoomCount" runat="server" CommandName="Sort" CommandArgument='3'>
                                    Комнат
                                <span id="sort3" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>

                            <th>
                                <asp:LinkButton ID="lkAddress" runat="server" CommandName="Sort" CommandArgument='4'>
                                    Адрес
                                <span id="sort4" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>

                            <th>
                                <asp:LinkButton ID="lbRegion" runat="server" CommandName="Sort" CommandArgument='25'>
                                    Район
                                <span id="sort25" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>

                            <th>
                                <asp:LinkButton ID="lbFloor" runat="server" CommandName="Sort" CommandArgument='5'>
                                    Этаж
                                <span id="sort5" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>

                            <th>
                                <asp:LinkButton ID="lbPrice" runat="server" CommandName="Sort" CommandArgument='12'>
                                    Цена
                                <span id="sort12" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>


                            <th>
                                <asp:LinkButton ID="lbFurniture" runat="server" CommandName="Sort" CommandArgument='8'>
                                    Мебель
                                <span id="sort8" runat="server" class="sort">&nbsp;</span>
                                </asp:LinkButton>
                            </th>






                            <th>Фото</th>
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


    <script type="text/javascript">
        $.fn.adaptTableWidth('grid', { 'grid': '670px' }, { 0: '26px', 1: '64px', 2: '60px', 3: '180px', 4: '90px', 5: '60px', 6: '60px', 7: '60px', 8: '21px' });


        $(".datePicker").datepicker({ appendText: '(dd.mm.yyyy)', dateFormat: 'dd.mm.yy' }).val();


    </script>
</asp:Content>
