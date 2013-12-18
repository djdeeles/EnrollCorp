<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HaberGrubuUyeligiKontrol.ascx.cs"
            Inherits="EnrollKurumsal.Admin.Kontroller.HaberGrubuUyeligiKontrol" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<link href="../Theme/Styles/AdminKontroller.css" rel="stylesheet" type="text/css" />
<asp:MultiView ID="MultiView2" runat="server">
    <asp:View ID="View2" runat="server">
        <table class="AnaTablo">
            <tr>
                <td style="width: 450px;">
                </td>
                <td style="width: 250px; text-align: right;">
                    <asp:DropDownList ID="DropDownListDurum" runat="server" Width="200px" AutoPostBack="True"
                                      OnSelectedIndexChanged="DropDownListDurum_SelectedIndexChanged">
                        <asp:ListItem Value="Tumu" Text="Tüm Üyeler"></asp:ListItem>
                        <asp:ListItem Value="AktifUyeler" Text="Aktif Üyeler"></asp:ListItem>
                        <asp:ListItem Value="PasifUyeler" Text="Pasif Üyeler"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <table class="AnaTablo">
                                <tr>
                                    <td colspan="4" style="height: 20px; color: White; background: #5D7B9D; padding-left: 5px; width: 700px;">
                                        Üyelik Düzenle
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        E-Posta
                                    </td>
                                    <td style="width: 10px;">
                                        :
                                    </td>
                                    <td style="width: 200px;">
                                        <asp:TextBox ID="TextBoxEPosta" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="width: 390px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxEPosta"
                                                                    ErrorMessage="Gerekli." ForeColor="Red" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Kayıt Tarihi
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td colspan="2">
                                        <%--   <asp:TextBox ID="TextBoxKayitTarihi" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="TextBoxKayitTarihi_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd.MM.yyyy" TargetControlID="TextBoxKayitTarihi">
                                </asp:CalendarExtender>--%>
                                        <telerik:RadDatePicker ID="RadDatePickerKayitTarihi" runat="server" Culture="Turkish (Turkey)">
                                            <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                                      ViewSelectorText="x">
                                            </Calendar>
                                            <DatePopupButton runat="server" ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                            <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        İptal Tarihi
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td colspan="2">
                                        <%--  <asp:TextBox ID="TextBoxIptalTarihi" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="TextBoxIptalTarihi_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd.MM.yyyy" TargetControlID="TextBoxIptalTarihi">
                                </asp:CalendarExtender>--%>
                                        <telerik:RadDatePicker ID="RadDatePickerIptalTarihi" runat="server" Culture="Turkish (Turkey)">
                                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                            </Calendar>
                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                            <DateInput DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Durum
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="CheckBoxDurum" runat="server" Text="Aktif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        <asp:ImageButton ID="ImageButtonKaydet" runat="server" ImageUrl="../Theme/Images/kaydet.png"
                                                         OnClick="ImageButtonKaydet_Click" ValidationGroup="g1" />
                                        <asp:ImageButton ID="ImageButtonIptal" runat="server" ImageUrl="../Theme/Images/iptal.png"
                                                         OnClick="ImageButtonIptal_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        <uc1:MesajKontrol ID="MesajKontrol1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        <asp:HiddenField ID="HiddenFieldId" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <uc1:MesajKontrol ID="MesajKontrol2" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="GridViewHaberGrubuUyeligi" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                  CellPadding="4" ForeColor="#333333" GridLines="None" DataSourceID="EntityDataSourceHaberGrubuUyeligi"
                                  Width="700px" OnRowCommand="GridViewHaberGrubuUyeligi_RowCommand">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="75" HeaderText="İşlemler" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="ImageButtonGuncelle" runat="server" ImageUrl="../Theme/Images/icon/edit.png"
                                                                 CommandArgument='<%#                                        Bind("Id") %>' CommandName="Guncelle" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImageButtonSİl" runat="server" ImageUrl="../Theme/Images/icon/cop.png"
                                                                 CommandArgument='<%#Bind("Id") %>' CommandName="Sil" OnClientClick="return confirm('Silmek istediğinizden emin misiniz?');" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="75px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EPosta" HeaderText="E-Posta" SortExpression="EPosta">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="KayitTarihi" HeaderText="Kayıt Tarihi" SortExpression="KayitTarihi">
                                <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IptalTarihi" HeaderText="İptal Tarihi" SortExpression="IptalTarihi">
                                <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="Durum" HeaderText="Durum">
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                            </asp:CheckBoxField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <asp:EntityDataSource ID="EntityDataSourceHaberGrubuUyeligi" runat="server" ConnectionString="name=EnrollKurumsalEntities"
                                          DefaultContainerName="EnrollKurumsalEntities" EnableFlattening="False" EntitySetName="HaberGrubuUyeligi"
                                          EntityTypeFilter="HaberGrubuUyeligi">
                    </asp:EntityDataSource>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="View3" runat="server">
        <table class="AnaTablo">
            <tr>
                <td style="font-size: 16px; text-align: center; width: 775px;">
                    <p>
                        Bu bölümü görüntülemek için yetkiniz bulunmamaktadır.<br />
                        Lütfen site yönetici ile iletişime geçiniz.
                    </p>
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>