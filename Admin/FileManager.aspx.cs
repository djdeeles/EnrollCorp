using System;
using System.Web.UI;

namespace EnrollKurumsal.Admin
{
    public partial class FileManager : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HiddenFieldReturnField.Value = Request.QueryString["ReturnField"];
            //string[] viewDocuments = new string[] { "~/FileManager" };
            //string[] uploadDocuments = new string[] { "~/FileManager" };
            //string[] deleteDocuments = new string[] { "~/FileManager" };
            //string[] searchDocuments = new string[] { "*.*" };

            //if (!IsPostBack)
            //{
            //    RadFileExplorer1.Configuration.ViewPaths = viewDocuments;
            //    RadFileExplorer1.Configuration.UploadPaths = uploadDocuments;
            //    RadFileExplorer1.Configuration.DeletePaths = deleteDocuments;
            //    RadFileExplorer1.Configuration.SearchPatterns = searchDocuments;
            //    RadFileExplorer1.ToolBar.Items.RemoveAt(5);
            //}

            //RadFileExplorer1.Configuration.SearchPatterns = new string[] { "*.*" };
            ////RadFileExplorer1.Configuration.ContentProviderTypeName = typeof(PendikBelediyesi.Admin.FileManager).AssemblyQualifiedName;
            //RadFileExplorer1.Configuration.ViewPaths = new string[] { "~/FileManager" };
            //RadFileExplorer1.Configuration.UploadPaths = new string[] { "~/FileManager" };
            //RadFileExplorer1.Configuration.DeletePaths = new string[] { "~/FileManager" };
        }
    }
}