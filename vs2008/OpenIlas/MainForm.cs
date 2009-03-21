using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using SqlSmartTest;

namespace openilas
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : Form
	{
		private MainMenu mainMenu1;

		int count=0;
		private MenuItem menuItem5;
		private MenuItem menuItemAcs;
		private MenuItem menuItemHoriz;
		private MenuItem menuItemVert;
		private MenuItem menuItemAI;
		private MenuItem menuItemFile;
		private MenuItem menuItemNew;
		private MenuItem menuItemClose;
		private MenuItem menuItemWindow;
		private MenuItem menuItemMax;
        private MenuItem menuItemMin;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem menuItem3;
        private IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// 
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemNew = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemClose = new System.Windows.Forms.MenuItem();
            this.menuItemWindow = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItemAI = new System.Windows.Forms.MenuItem();
            this.menuItemAcs = new System.Windows.Forms.MenuItem();
            this.menuItemHoriz = new System.Windows.Forms.MenuItem();
            this.menuItemVert = new System.Windows.Forms.MenuItem();
            this.menuItemMax = new System.Windows.Forms.MenuItem();
            this.menuItemMin = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItemWindow,
            this.menuItem5,
            this.menuItem2});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemNew,
            this.menuItem1,
            this.menuItemClose});
            this.menuItemFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItemFile.Text = "&File";
            // 
            // menuItemNew
            // 
            this.menuItemNew.Index = 0;
            this.menuItemNew.Text = "&New";
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.MergeOrder = 4;
            this.menuItem1.Text = "Close All";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItemClose
            // 
            this.menuItemClose.Index = 2;
            this.menuItemClose.MergeOrder = 5;
            this.menuItemClose.Text = "Cl&ose";
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            // 
            // menuItemWindow
            // 
            this.menuItemWindow.Index = 1;
            this.menuItemWindow.MdiList = true;
            this.menuItemWindow.Text = "&Window";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAI,
            this.menuItemAcs,
            this.menuItemHoriz,
            this.menuItemVert,
            this.menuItemMax,
            this.menuItemMin});
            this.menuItem5.Text = "&Layout";
            // 
            // menuItemAI
            // 
            this.menuItemAI.Index = 0;
            this.menuItemAI.Text = "Arrange&Icons";
            this.menuItemAI.Click += new System.EventHandler(this.menuItemAI_Click);
            // 
            // menuItemAcs
            // 
            this.menuItemAcs.Index = 1;
            this.menuItemAcs.Text = "&Cascade";
            this.menuItemAcs.Click += new System.EventHandler(this.menuItemCas_Click);
            // 
            // menuItemHoriz
            // 
            this.menuItemHoriz.Index = 2;
            this.menuItemHoriz.Text = "Arrange &Horizontal";
            this.menuItemHoriz.Click += new System.EventHandler(this.menuItemHoriz_Click);
            // 
            // menuItemVert
            // 
            this.menuItemVert.Index = 3;
            this.menuItemVert.Text = "Arrange &Vertical";
            this.menuItemVert.Click += new System.EventHandler(this.menuItemVert_Click);
            // 
            // menuItemMax
            // 
            this.menuItemMax.Index = 4;
            this.menuItemMax.Text = "Ma&ximize all";
            this.menuItemMax.Click += new System.EventHandler(this.menuItemMax_Click);
            // 
            // menuItemMin
            // 
            this.menuItemMin.Index = 5;
            this.menuItemMin.Text = "Mi&nimize all";
            this.menuItemMin.Click += new System.EventHandler(this.menuItemMin_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3});
            this.menuItem2.Text = "baseinfo";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.Text = "”√ªß";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(528, 401);
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MDI Sample";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void menuItemNew_Click(object sender, System.EventArgs e)
		{
			
				
			
		}

		private void menuItemClose_Click(object sender, System.EventArgs e)
		{
			//Gets the currently active MDI child window.
			Form a = this.ActiveMdiChild;
			//Close the MDI child window
            if (a != null)
            {
                a.Close();
            }
		}		

		private void menuItemAI_Click(object sender, System.EventArgs e)
		{
			//Arrange MDI child icons within the client region of the MDI parent form.
			this.LayoutMdi(MdiLayout.ArrangeIcons);
		}

		private void menuItemCas_Click(object sender, System.EventArgs e)
		{
			//Cascade all child forms.
			this.LayoutMdi(MdiLayout.Cascade);
		}

		private void menuItemHoriz_Click(object sender, System.EventArgs e)
		{
			//Tile all child forms horizontally.
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void menuItemVert_Click(object sender, System.EventArgs e)
		{			
			//Tile all child forms vertically.
			this.LayoutMdi(MdiLayout.TileVertical);
		}

		private void menuItemMax_Click(object sender, System.EventArgs e)
		{
			//Gets forms that represent the MDI child forms 
			//that are parented to this form in an array
			Form [] charr= this.MdiChildren;

			//for each child form set the window state to Maximized
			foreach (Form chform in charr)
				chform.WindowState=FormWindowState.Maximized;
		}

		private void menuItemMin_Click(object sender, System.EventArgs e)
		{
			//Gets forms that represent the MDI child forms 
			//that are parented to this form in an array
			Form [] charr= this.MdiChildren;

			//for each child form set the window state to Minimized
			foreach (Form chform in charr)
				chform.WindowState=FormWindowState.Minimized;
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			//Gets forms that represent the MDI child forms 
			//that are parented to this form in an array
			Form [] charr= this.MdiChildren;

			//for each child form set the window state to Minimized
			foreach (Form chform in charr)
				chform.Close();
		}

        private void Form1_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            this.menuItemWindow.MdiList = true;
            this.Text = "openilas";
            CompanyApp app = new CompanyApp();
            ReaderList list = null;
            list = new ReaderList();
            list.MdiParent = this;
            list.Show();
            
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            ReaderList list = null;
            list = new ReaderList();
            list.MdiParent = this;
            list.Show();

        }

        private void menuItem4_Click(object sender, EventArgs e)
        {


        }

			
	}
}
