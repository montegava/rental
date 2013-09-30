<%@ Page Title="" Language="C#" MasterPageFile="~/Rental.Master" AutoEventWireup="true"
    CodeBehind="AddFlat.aspx.cs" Inherits="RentalCMS.AddFlat" %>

    <%@ Register Namespace="RentalCMS.Controls" TagPrefix="Rental" Assembly="RentalCMS"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainAreaOfPage" runat="server">
    
    <div class="row">
        <div class="span-row">Ваше имя</div>
        <asp:TextBox ID="tbName" CssClass="form-input-text" runat="server"></asp:TextBox> 

    </div>
    
    <div class="row">
       <div class="span-row">Электронная почта</div>
        <asp:TextBox ID="tbEmail"  CssClass="form-input-text" runat="server"></asp:TextBox>
    </div>

    <div class="row">
        <div class="span-row">Номер телефона</div>
        <asp:TextBox ID="tbPhone" CssClass="form-input-text" runat="server"></asp:TextBox>
    </div>

    <div class="row">
        <div class="span-row">Район</div>
        <Rental:EnumDropDownList ID="ddlRegion" runat="server" Type="DAL.Region, DAL" />
    </div>
    
    <div class="row">
       <div class="span-row">Категория</div>
       <Rental:EnumDropDownList ID="ddlCategory" runat="server" Type="DAL.Category, DAL" />
    </div>

    <div class="row">
        <div class="span-row">Тип</div>
         <Rental:EnumDropDownList ID="ddlType" runat="server" Type="DAL.Type, DAL" />
    </div>

    <div class="row">
        <div class="span-row">Кол-во комнат</div>
        <Rental:RangeDropDownList ID="ddlRoomCount" runat="server" From="1" To="9"></Rental:RangeDropDownList>
    </div>

    <div class="row">
         <div class="span-row">Этаж</div>
         <Rental:RangeDropDownList ID="ddlFloor" runat="server" From="1" To="13"></Rental:RangeDropDownList>
    </div>

    <div class="row">
        <div class="span-row">Вид дома</div>
        <Rental:EnumDropDownList ID="ddlHouseType" runat="server" Type="DAL.HouseType, DAL" />
    </div>

    <div class="row">
        <div class="span-row">Срок аренды</div>
        <Rental:EnumDropDownList ID="ddlRentType" runat="server" Type="DAL.RentType, DAL" />
    </div>

    <div class="row">
        <div class="span-row">Площадь</div>
        <asp:TextBox ID="tbSquare" runat="server"></asp:TextBox>
    </div>

    <div class="row">
        <div class="span-row">Адрес</div>
        <asp:TextBox ID="tbAdres" runat="server"></asp:TextBox>
    </div>

    <div class="row">
        <div class="span-row">Описание</div>
        <asp:TextBox ID="tbDescription" runat="server"  Rows="4"></asp:TextBox>
    </div>

    
    <div class="row">
        <div class="span-row">Арендная плата</div>
        <asp:TextBox ID="tbPrice" runat="server"></asp:TextBox>
    </div>

    <div class="row">
        <div class="span-row">Оплата</div>
        <Rental:EnumDropDownList ID="ddlPayment" runat="server" Type="DAL.Payment, DAL" />
    </div>
     

   <div class="row">

        <asp:FileUpload ID="fuPhoto" runat="server" class="multi" accept="gif|jpg|jpeg" maxlength="5" />
        <asp:Label ID="myLabel" runat="server" ForeColor="#CC0000" />
        <asp:Label ID="lblMsg" runat="server" ForeColor="#CC0000" /> 
        <div class="MultiFile-list" id="MultiFile1_wrap_list"></div>
   </div>

    
    <div class="row">
        <div class="panel">
            <asp:LinkButton ID="btnClose" runat="server" CssClass="button" OnClick="CancelClick" CausesValidation="false">
                    Закрыть
            </asp:LinkButton>

            <asp:LinkButton ID="Upload"  Text="" runat="server" CssClass="button" onclick="Upload_Click">     Сохранить
            </asp:LinkButton>

        </div>
    </div>


</asp:Content>
