<%@ Page Title="" Language="C#" MasterPageFile="~/Rental.Master" AutoEventWireup="true" CodeBehind="Flats.aspx.cs" Inherits="RentalCMS.Flats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainAreaOfPage" runat="server">






    <section class="choices">
        <fieldset>
            <legend>Фильтры</legend>
            <p id="_errorText" runat="server" class="error2 hide">
                <asp:Label ID="_lbError" runat="server" Text=""></asp:Label></p>
            <div class="left">
                <span class="mask">
                    <asp:TextBox runat="server" ID="_tbSearchText" name="_tbSearchText"></asp:TextBox>
                </span>
            </div>

            <br class="clearLeft" />
            <div class="lsCheck">
                <div class="row">
                    <asp:CheckBox ID="_cbOriginalName" name="_cbOriginalName" runat="server"></asp:CheckBox>
                    <label for="_cbOriginalName" title="">
                         Адресс 
                    </label>
                </div>
            </div>
            <div class="nrmRow">
                <div class="areaGroup areaGroup_fix1">
                    <div class="row>
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

    <script type="text/javascript">
        $(function () {

            $(".datePicker").datepicker({ appendText: '(dd.mm.yyyy)', dateFormat: 'dd.mm.yy' }).val();

     
        });
</script>














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
                    <asp:Label runat="server" ID="lbData"> <%# Eval("DATA") %> </asp:Label>
                </td>


                <td>
                    <asp:Label runat="server" ID="lblRoomCount"> <%# Eval("ROOM_COUNT") %> </asp:Label>
                </td>


                <td>
                    <asp:Label runat="server" ID="lbAddress"> <%# Eval("ADDRESS") %> </asp:Label>
                </td>

            </tr>
        </ItemTemplate>
    </asp:ListView>


</asp:Content>
