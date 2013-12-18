<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnketKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.AnketKontrol" %>
<div class="AnketKontrol">
    <table>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:Label ID="lblSoru" runat="server">
                            </asp:Label>
                            <br />
                            <br />
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" DataSourceID="EntityDataSource1"
                                                 DataTextField="Cevap" DataValueField="Id" AutoPostBack="True">
                            </asp:RadioButtonList>
                            <br />
                            <span style="font-size: 19px; font-family: MyriadProCondensed;">
                                <asp:LinkButton ID="LinkButtonOyla" runat="server" ForeColor="#074140" Font-Underline="False"
                                                OnClick="LinkButtonOyla_Click">Oyla</asp:LinkButton>
                            </span>
                            <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=EnrollKurumsalEntities"
                                                  DefaultContainerName="EnrollKurumsalEntities" EntitySetName="AnketCevaplari"
                                                  EnableFlattening="False">
                            </asp:EntityDataSource>
                          
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                            <asp:HiddenField ID="hdnToplam" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="Panel4" runat="server" Visible="False">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                                          DataSourceID="EntityDataSource1" GridLines="None" OnDataBound="GridView1_DataBound"
                                          ShowHeader="False" Style="font-family: Verdana; font-size: x-small" Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%#                                        Bind("Cevap") %>' Width="140px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOran" runat="server" CssClass="style1" Font-Bold="True" Text='<%#Bind("OySayisi") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</div>