using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site : System.Web.UI.MasterPage
{
    public Label PageTitle
    {
        get { return lblPageTitle; }
        set { lblPageTitle = value; }
    }

    public Label ErrorMessage
    {
        get { return lblErrorMessage; }
        set { lblErrorMessage = value; }
    }
}
