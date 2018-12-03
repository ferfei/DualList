using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NG.IS.EOPF.DualList
{
    
    [ToolboxData("<{0}:DualListBox runat=server></{0}:DualListBox>")]
    public class DualListBox : CompositeControl
    {
        Label lblAvailableListox, lblSelectedListBox;
        ListBox AvailableListBox, SelectedListBox;
        Button btnUnassign, btnAssign, btnAssignAll, btnUnAssignAll;
        public delegate void SortDelegate(ref ListBox listbox );

        SortDelegate CustomSort = null;

        [Bindable(true)]
        [DefaultValue("Available")]
        [Themeable(false)]
        public object AvailableList
        {
            get
            {
                EnsureChildControls();
                return AvailableListBox.Items;
            }

            set
            {
                EnsureChildControls();
                AvailableListBox.DataSource = value;
                
            }
        }

        [Bindable(true)]
        [DefaultValue("Current")]
        [Themeable(false)]
        public object SelectedList
        {
            get
            {
                EnsureChildControls();
                return SelectedListBox.Items;
            }

            set
            {
                EnsureChildControls();
                SelectedListBox.DataSource = value;                
            }
        } 

        [Bindable(true)]
        [DefaultValue("Current")]
        [Themeable(false)]
        public String SelectedDataTextField
        {
            get
            {
                EnsureChildControls();
                return SelectedListBox.DataTextField;
            }

            set
            {
                EnsureChildControls();
                SelectedListBox.DataTextField = value;
                SelectedListBox.DataBind();
            }
        }

        [Bindable(true)]
        [DefaultValue("Current")]
        [Themeable(false)]
        public String AvailableDataTextField
        {
            get
            {
                EnsureChildControls();
                return AvailableListBox.DataTextField;
            }

            set
            {
                EnsureChildControls();
                AvailableListBox.DataTextField = value;
                AvailableListBox.DataBind();                
            }
        }

        public void InitializeAvailableList(object datasource, string dataTextField,string dataValueField= "",SortDelegate customSort=null)
        {
            AvailableListBox.DataSource = datasource;
            AvailableListBox.DataTextField = dataTextField;
            if (!string.IsNullOrEmpty(dataValueField))
            {
                AvailableListBox.DataValueField = dataValueField;
            }
            if (customSort != null)
            {
                this.CustomSort = customSort;
            }

            AvailableListBox.DataBind();  
                
        }

        public void Sort (SortDelegate sort)
        {
            CustomSort = sort;
        }
        public void InitializeSelectedList(object datasource, string dataTextField,string dataValueField="")
        {
            SelectedListBox.DataSource = datasource;
            SelectedListBox.DataTextField = dataTextField;
            if (!string.IsNullOrEmpty(dataValueField))
            {
                SelectedListBox.DataValueField = dataValueField;
            }
            SelectedListBox.DataBind();
            
            IEnumerable selectedList = AvailableListBox.Items.Cast<ListItem>().Intersect(SelectedListBox.Items.Cast<ListItem>()).ToList();
            foreach (ListItem list in selectedList)
            {
                AvailableListBox.Items.Remove(list);
            }

            SortListBox(SelectedListBox);
        }

        [Bindable(true)]
        [DefaultValue("Current")]
        [Themeable(false)]
        public string SelectedListLabel
        {
            get
            {
                EnsureChildControls();
                return lblSelectedListBox.Text;
            }
            set
            {
                EnsureChildControls();
                lblSelectedListBox.Text = value;
                if (String.IsNullOrEmpty(SelectedListTooltip))
                {
                    SelectedListTooltip = value;
                }
            }
        }


        [Bindable(true)]
        [DefaultValue("Available")]
        [Themeable(false)]
        public string AvailableListLabel
        {

            get
            {
                EnsureChildControls();
                return lblAvailableListox.Text;
            }
            set
            {
                EnsureChildControls();
                lblAvailableListox.Text = value;
                if (String.IsNullOrEmpty(AvailableListTooltip))
                {
                    AvailableListTooltip = value;
                }
            }
        }

        [Bindable(true)]
        [DefaultValue("Available")]
        [Themeable(false)]
        public string AvailableListTooltip
        {

            get
            {
                EnsureChildControls();
                return AvailableListBox.ToolTip;
            }
            set
            {
                EnsureChildControls();
                AvailableListBox.ToolTip = value;
            }
        }

        [Bindable(true)]
        [DefaultValue("Current")]
        [Themeable(false)]
        public string SelectedListTooltip
        {

            get
            {
                EnsureChildControls();
                return SelectedListBox.ToolTip;
            }
            set
            {
                EnsureChildControls();
                SelectedListBox.ToolTip = value;
            }
        }

        [Bindable(true)]
        [DefaultValue("Assign All")]
        [Themeable(false)]
        public string AssignAllLabel
        {
            get
            {
                EnsureChildControls();
                return btnAssignAll.Text;
            }
            set
            {
                EnsureChildControls();
                btnAssignAll.Text = value;
            }
        }

        [DefaultValue("Assign")]
        [Bindable(true)]
        [Themeable(false)]
        public string AssignLabel
        {
            get
            {
                EnsureChildControls();
                return btnAssign.Text;
            }
            set
            {
                EnsureChildControls();
                btnAssign.Text = value;
            }
        }

        [DefaultValue("UnAssign")]
        [Bindable(true)]
        [Themeable(false)]
        public string UnAssignLabel
        {
            get
            {
                EnsureChildControls();
                return btnUnassign.Text;
            }
            set
            {
                EnsureChildControls();
                btnUnassign.Text = value;
            }
        }

        [DefaultValue("UnAssign All")]
        [Bindable(true)]
        [Themeable(false)]
        public string UnAssignAllLabel
        {
            get
            {
                EnsureChildControls();
                return btnUnAssignAll.Text;
            }
            set
            {
                EnsureChildControls();
                btnUnAssignAll.Text = value;
            }
        }

        [DefaultValue(true)]
        [Bindable(true)]
        [Themeable(false)]
        public bool ShowAssignAll
        {
            get
            {
                EnsureChildControls();
                return btnAssignAll.Visible;
            }
            set
            {
                EnsureChildControls();
                btnAssignAll.Visible = value;
            }
        }



        [DefaultValue(true)]
        [Bindable(true)]
        [Themeable(false)]
        public bool ShowUnAssignAll
        {
            get
            {
                EnsureChildControls();
                return btnUnAssignAll.Visible;
            }
            set
            {
                EnsureChildControls();
                btnUnAssignAll.Visible = value;
            }
        }


        [DefaultValue(true)]
        [Bindable(true)]
        [Themeable(false)]
        public bool ShowAssign
        {
            get
            {
                EnsureChildControls();
                return btnAssign.Visible;
            }
            set
            {
                EnsureChildControls();
                btnAssign.Visible = value;
            }
        }


        [DefaultValue(true)]
        [Bindable(true)]
        [Themeable(false)]
        public bool ShowUnAssign
        {
            get
            {
                EnsureChildControls();
                return btnUnassign.Visible;
            }
            set
            {
                EnsureChildControls();
                btnUnassign.Visible = value;
            }
        }


        [DefaultValue(true)]
        [Bindable(true)]
        [Themeable(false)]
        public bool EnableAssignAll
        {
            get
            {
                EnsureChildControls();
                return btnAssignAll.Enabled;
            }
            set
            {
                EnsureChildControls();
                btnAssignAll.Enabled = value;
            }
        }



        [DefaultValue(true)]
        [Bindable(true)]
        [Themeable(false)]
        public bool EnableUnAssignAll
        {
            get
            {
                EnsureChildControls();
                return btnUnAssignAll.Enabled;
            }
            set
            {
                EnsureChildControls();
                btnUnAssignAll.Enabled = value;
            }
        }


        [DefaultValue(true)]
        [Bindable(true)]
        [Themeable(false)]
        public bool EnableAssign
        {
            get
            {
                EnsureChildControls();
                return btnAssign.Enabled;
            }
            set
            {
                EnsureChildControls();
                btnAssign.Enabled = value;
            }
        }


        [DefaultValue(true)]
        [Bindable(true)]
        [Themeable(false)]
        public bool EnableUnAssign
        {
            get
            {
                EnsureChildControls();
                return btnUnassign.Enabled;
            }
            set
            {
                EnsureChildControls();
                btnUnassign.Enabled = value;
            }
        }    

        [DefaultValue(true)]
        [Bindable(true)]
     
        public void ClearSelectedList()
        {
                EnsureChildControls();
                SelectedListBox.Items.Clear();
        }

        public void ClearAvailableList()
        {
            EnsureChildControls();
            AvailableListBox.Items.Clear();
        }

        public void Clear()
        {
            SelectedListBox.Items.Clear();
            AvailableListBox.Items.Clear();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Controls.Clear();
            lblAvailableListox = new Label();
            lblSelectedListBox = new Label();
            AvailableListBox = new ListBox();
            AvailableListBox.ID = "Available";
            AvailableListBox.Height = 300;
            AvailableListBox.Width = 220;
            AvailableListBox.SelectionMode = ListSelectionMode.Multiple;
            SelectedListBox = new ListBox();
            SelectedListBox.ID = "Selected";
            SelectedListBox.Height = 300;
            SelectedListBox.Width = 220;
            SelectedListBox.SelectionMode = ListSelectionMode.Multiple;
            btnUnassign = new Button();
            btnAssign = new Button();
            btnUnAssignAll = new Button();
            btnAssignAll = new Button();
            AssignAllLabel = "Assign All";
            AssignLabel = "Assign";
            UnAssignAllLabel = "UnAssign All";
            UnAssignLabel = "UnAssign";
            AvailableListLabel = "Available";
            SelectedListLabel = "Current";
            btnUnassign.Click += new EventHandler(btnUnAssign_Click);
            btnAssign.Click += new EventHandler(btnAssign_Click);
            btnUnAssignAll.Click += new EventHandler(btnUnAssignAll_Click);
            btnAssignAll.Click += new EventHandler(btnAssignAll_Click);


            Controls.Add(lblAvailableListox);
            Controls.Add(lblSelectedListBox);
            Controls.Add(AvailableListBox);
            Controls.Add(SelectedListBox);

            Controls.Add(btnUnassign);
            Controls.Add(btnAssign);
            Controls.Add(btnAssignAll);
            Controls.Add(btnUnAssignAll);
        }

        private void btnAssign_Click(object source, EventArgs e)
        {
            ListItem lstItem = AvailableListBox.SelectedItem;

            while (lstItem != null)
            {
                AvailableListBox.Items.Remove(lstItem);
                SelectedListBox.Items.Add(lstItem);
                lstItem.Selected = false;
                lstItem = AvailableListBox.SelectedItem;
            }
            SelectedList = SelectedListBox.Items;
            SortListBox(SelectedListBox);
        }       

        private void btnUnAssign_Click(object source, EventArgs e)
        {
            ListItem lstItem = SelectedListBox.SelectedItem;

            while (lstItem != null)
            {
                SelectedListBox.Items.Remove(lstItem);
                AvailableListBox.Items.Add(lstItem);
                lstItem.Selected = false;
                lstItem = SelectedListBox.SelectedItem;
            }

            SortListBox(AvailableListBox);
        }

        private void btnUnAssignAll_Click(object source, EventArgs e)
        {
            MoveAllItems(SelectedListBox, AvailableListBox);
        }

        private void btnAssignAll_Click(object source, EventArgs e)
        {
            MoveAllItems(AvailableListBox, SelectedListBox);
        }

        private void MoveAllItems(ListBox moveFrom, ListBox moveTo)
        {
            List<ListItem> OriginalListItems = moveFrom.Items.Cast<ListItem>().ToList();

            foreach (ListItem item in OriginalListItems)
            {
                moveTo.Items.Add(item);
                moveFrom.Items.Remove(item);
            }

            SortListBox(moveTo);
        }

        public void SortListBoxOnLoad()
        {
            SortListBox(AvailableListBox);
            SortListBox(SelectedListBox);
        }
        private void SortListBox(ListBox list)
        {
            if (CustomSort != null)
            {              
                CustomSort(ref list);
            }
            else
            {
                List<ListItem> sortedList = list.Items.Cast<ListItem>().ToList();
                sortedList = sortedList.OrderBy(listitem => listitem.Value).ToList();
                list.Items.Clear();
                list.Items.AddRange(sortedList.ToArray());
            }
        }

      
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string css = "<link href=\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(),
            "NG.IS.EOPF.DualList.resources.css.main.css") + "\" type=\"text/css\" rel=\"stylesheet\" />";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "cssFile", css, false);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "formcol");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "width: 230px;");

            //outer div
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Style, "padding: 5px; text-align: center;");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "formrow");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "formlabel");
            writer.RenderBeginTag(HtmlTextWriterTag.Label);

            
            writer.Write(AvailableListLabel);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "formrow");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, AvailableListBox.ClientID);
            AvailableListBox.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "formcol");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "width: 150px;");

            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "formrow");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "height: 342px; padding: 5px; text-align: center;");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "margin: 90px 0;");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (ShowAssignAll)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ButtonStyleMedium");

                btnAssignAll.RenderControl(writer);
                writer.Write("<br/><br/>");
            }
            if (ShowAssign)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ButtonStyleMedium");
                btnAssign.RenderControl(writer);
                writer.Write("<br/><br/>");
            }

            if (ShowUnAssign)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ButtonStyleMedium");
                btnUnassign.RenderControl(writer);
                writer.Write("<br/><br/>");
            }
            if (ShowUnAssignAll)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "ButtonStyleMedium");
                btnUnAssignAll.RenderControl(writer);
            }
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "formcol");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "width: 230px;");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Style, "padding: 5px; text-align: center;");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "formrow");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "formlabel");
            writer.RenderBeginTag(HtmlTextWriterTag.Label);

            writer.Write(SelectedListLabel);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "formrow");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, SelectedListBox.ClientID);
            SelectedListBox.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
        }
    }
}
