<%@ Page Title="" Language="C#" MasterPageFile="~/Rental.Master" AutoEventWireup="true"
    CodeBehind="AddFlat.aspx.cs" Inherits="RentalCMS.AddFlat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainAreaOfPage" runat="server">
    <div class="row">
        <asp:Literal runat="server" Text="Ваше имя"></asp:Literal>
        <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
    </div>
    <div class="row">
        <asp:Literal ID="Literal1" runat="server" Text="Электронная почта"></asp:Literal>
        <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
    </div>
    <div class="row">
        <asp:Literal ID="Literal2" runat="server" Text="Номер телефона"></asp:Literal>
        <asp:TextBox ID="tbPhone" runat="server"></asp:TextBox>
    </div>
    <div class="row">
        <asp:Literal ID="Literal3" runat="server" Text="Район"></asp:Literal>
        <asp:DropDownList ID="ddlRegion" runat="server">
            <asp:ListItem>-- неизвестно -- </asp:ListItem>
            <asp:ListItem>Коминтерновский </asp:ListItem>
            <asp:ListItem>Центральный </asp:ListItem>
            <asp:ListItem>Ж/Д </asp:ListItem>
            <asp:ListItem>Левобережный </asp:ListItem>
            <asp:ListItem>Советский </asp:ListItem>
            <asp:ListItem>Ленинский </asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="row">
        <asp:Literal ID="Literal5" runat="server" Text="Категория"></asp:Literal>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>-- неизвестно -- </asp:ListItem>
            <asp:ListItem>Квартира</asp:ListItem>
            <asp:ListItem>Комната</asp:ListItem>
            <asp:ListItem>Дом</asp:ListItem>
            <asp:ListItem>Помещение, офис</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="row">
        <asp:Literal ID="Literal4" runat="server" Text="Тип"></asp:Literal>
        <asp:DropDownList ID="ddlType" runat="server">
            <asp:ListItem>-- неизвестно -- </asp:ListItem>
            <asp:ListItem>Сдам </asp:ListItem>
            <asp:ListItem>Сниму </asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="row">
        <asp:Literal ID="Literal6" runat="server" Text="Кол-во комнат"></asp:Literal>
        <asp:DropDownList ID="DropDownList2" runat="server">
            <asp:ListItem>-- неизвестно --</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="row">
        <asp:Literal ID="Literal7" runat="server" Text="Этаж"></asp:Literal>
        <asp:DropDownList ID="DropDownList3" runat="server">
            <asp:ListItem>-- неизвестно --</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>13</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="row">
        <asp:Literal ID="Literal8" runat="server" Text="Вид дома"></asp:Literal>
        <asp:DropDownList ID="DropDownList4" runat="server">
            <asp:ListItem>-- неизвестно --</asp:ListItem>
            <asp:ListItem>кирпичный</asp:ListItem>
            <asp:ListItem>панельный</asp:ListItem>
            <asp:ListItem>коттедж</asp:ListItem>
            <asp:ListItem>часть дома</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="row">
        <asp:Literal ID="Literal9" runat="server" Text="Срок аренды"></asp:Literal>
        <asp:DropDownList ID="DropDownList5" runat="server">
            <asp:ListItem>на длительный срок</asp:ListItem>
            <asp:ListItem>на сутки</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="row">
        <asp:Literal ID="Literal10" runat="server" Text="Площадь"></asp:Literal>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
    <div class="row">
        <asp:Literal ID="Literal11" runat="server" Text="Адрес  "></asp:Literal>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    </div>
    <div class="row">
        <asp:Literal ID="Literal12" runat="server" Text="Описание  "></asp:Literal>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
    </div>
    <asp:FileUpload runat="server" />
</asp:Content>
